using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Application.Interfaces.Surveys;
using PerformanceEvaluationPlatform.Application.Interfaces.Users;
using PerformanceEvaluationPlatform.Application.Interfaces.Users.Auth0;
using PerformanceEvaluationPlatform.Application.Model.Users;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = PerformanceEvaluationPlatform.Domain.Users.User;

namespace PerformanceEvaluationPlatform.Application.Services.Users
{
    public class UserService : IUserService
    {
        private const string connection = "Username-Password-Authentication";
        private const int ActiveState = 1;
        private const int SuspendState = 2;
        private readonly IUserRepository _userRepo;
        //private readonly IRoleRepository _roleRepo;
        //private readonly ITeamRepository _teamRepo;
        private readonly ISurveysRepository _surveysRepository;
        private readonly IAuth0ClientFactory _auth0Factory;
        private readonly Auth0Configur _config;

        public UserService(IUserRepository userRepo, ISurveysRepository surveysRepository, IAuth0ClientFactory auth0Factory, IOptions<Auth0Configur> config)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
            _auth0Factory = auth0Factory ?? throw new ArgumentNullException(nameof(auth0Factory));
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            //_roleRepo = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            // _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        }



        public async Task<ServiceResponse<UserDetailDto>> GetDetail(int id)
        {
            var userDetail = await _userRepo.GetDetail(id);
            return userDetail == null
                ? ServiceResponse<UserDetailDto>.NotFound()
                : ServiceResponse<UserDetailDto>.Success(userDetail);
        }

        public async Task<ServiceResponse<IList<UserListItemDto>>> GetList(UserFilterDto filter)
        {
            IList<UserListItemDto> items = await _userRepo.GetList(filter);
            return ServiceResponse<IList<UserListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<IList<UserStateListItemDto>>> GetStates()
        {
            IList<UserStateListItemDto> userStates = await _userRepo.GetStates();
            return ServiceResponse<IList<UserStateListItemDto>>.Success(userStates);
        }

        public async Task<ServiceResponse<SystemRoleDto>> GetSystemRole(string id)
        {
            var systemRoles = await _userRepo.GetSystemRole(id);
            return ServiceResponse<SystemRoleDto>.Success(systemRoles);
        }

        public async Task<ServiceResponse<IList<SystemRoleDto>>> GetSystemRoles()
        {
            var systemRoles = await _userRepo.GetSystemRoles();
            return ServiceResponse<IList<SystemRoleDto>>.Success(systemRoles);
        }

        public async Task<ServiceResponse> Create(CreateUserDto userToCreate,bool IsDevelop)
        {
            var existingUser = await _userRepo.Get(userToCreate.Email);
            if (existingUser != null)
            {
                return ServiceResponse.Failure<CreateUserDto>(s => s.Email, "User with the same email is already exists");
            }
            var response = await ValidateUser(userToCreate);
            if (response.IsBadRequest)
            {
                return response;
            }

            var userRoleMaps = userToCreate.RoleIds.Select(s => new UserRoleMap { RoleId = s }).ToList();
            var user = new User()
            {
                Email = userToCreate.Email,
                EnglishLevelId = userToCreate.EnglishLevelId,
                FirstDayInCompany = userToCreate.FirstDayInCompany,
                FirstDayInIndustry = userToCreate.FirstDayInIndustry,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                StateId = ActiveState,
                TeamId = userToCreate.TeamId,
                TechnicalLevelId = userToCreate.TechnicalLevelId,
                SystemRoleId = userToCreate.SystemRoleId,
                Roles = userRoleMaps
            };
            await  _userRepo.Create(user);
            await  _userRepo.Save();
            var client = await _auth0Factory.CreateManagementApi();
            await CreateAuth0User(user, client,IsDevelop);

            if (!IsDevelop)
            {
                var authclient = _auth0Factory.CreateAuthenticationApi();
                await SendMessageToChangeEmail(authclient, user);
            }

            return ServiceResponse.Success();

        }
        public async Task<ServiceResponse> Update(int id ,UpdateUserDto updateUser)
        {
            var user = await _userRepo.Get(id);
            if (user is null)
            {
                return ServiceResponse.NotFound();
            }
            var existingUser = await _userRepo.Get(updateUser.Email);
            if (existingUser != null && existingUser.Id != id)
            {
                return ServiceResponse.Failure<UpdateUserDto>(s => s.Email, "User with the same email is already exists");
            }
            var response=await ValidateUser(updateUser);
            if (response.IsBadRequest) 
            {
                return response;
            }

            user.Email = updateUser.Email;
            user.EnglishLevelId = updateUser.EnglishLevelId;
            user.FirstDayInCompany = updateUser.FirstDayInCompany;
            user.FirstDayInIndustry = updateUser.FirstDayInIndustry;
            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.TeamId = updateUser.TeamId;
            user.TechnicalLevelId = updateUser.TechnicalLevelId;
            user.SystemRoleId = updateUser.SystemRoleId;

            await _userRepo.Update(updateUser.RoleIds, user.Id);
            await _userRepo.Save();

            var client = await _auth0Factory.CreateManagementApi();
            await UpdateAuth0User(user, client);
            return ServiceResponse.Success();
        }
        public async Task<ServiceResponse> Suspend(int id)
        {
            var user =await  _userRepo.Get(id);
            if(user is null)
            {
                return ServiceResponse.NotFound("User with this id doenst exists");
            }
            if(user.StateId == SuspendState)
            {
                return ServiceResponse.Success();
            }
            user.StateId = SuspendState;
            await _userRepo.Save();
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> Activate(int id)
        {
            var user = await _userRepo.Get(id);
            if (user is null)
            {
                return ServiceResponse.NotFound("User with this id doenst exists");
            }
            if (user.StateId == ActiveState)
            {
                return ServiceResponse.Success();
            }
            user.StateId = ActiveState;
            await _userRepo.Save();
            return ServiceResponse.Success();
        }



        private async Task<ServiceResponse> ValidateUser(IUserDto userRequest)
        {
            List<int> notValidUserRoles = new List<int>();
            foreach (var item in userRequest.RoleIds)
            {
                var role = await _userRepo.Get(item);
                if (role == null)
                {
                    notValidUserRoles.Add(item);
                }
            }
            if (notValidUserRoles.Any())
            {
                foreach (var item in notValidUserRoles)
                {
                    return ServiceResponse.Failure<IUserDto>(t => item, "State does not exists");
                }
            }

            //var userTeam = await _teamRepo.Get(userRequest.TeamId);
            //if (userTeam is null)
            //{
            //    ServiceResponse.Failure<IUserDto>(t => t.TeamId, "Team doens not exists");
            //}

            var systemRole = await _userRepo.GetSystemRole(userRequest.SystemRoleId);
            if (systemRole is null)
            {
                return ServiceResponse.Failure<IUserDto>(t => t.SystemRoleId, "System role does not exists");
            }

            var userTechnicalLevel = await _surveysRepository.GetLevel(userRequest.TechnicalLevelId);
            if (userTechnicalLevel is null)
            {
                return ServiceResponse.Failure<IUserDto>(t => t.TechnicalLevelId, "Level with this Id doesn't exists");
            }
            var userEnglishLevel = await _surveysRepository.GetLevel(userRequest.EnglishLevelId);
            if (userEnglishLevel is null)
            {
                return ServiceResponse.Failure<IUserDto>(t => t.EnglishLevelId, "Level with this Id doesn't exists");
            }

            if (userRequest.FirstDayInCompany < userRequest.FirstDayInIndustry)
            {
                return  ServiceResponse.Failure<IUserDto>(t => t.FirstDayInCompany, "User couldn't worked In company before he  was in industry");
            }

            var yearInCompany = DateTime.Now.Year - userRequest.FirstDayInCompany.Year;
            if (yearInCompany > 60)
            {
                return ServiceResponse.Failure<IUserDto>(t => t.FirstDayInCompany, $"User haven't could work In company for {yearInCompany} years");
            }
            var yearInIndustry = DateTime.Now.Year - userRequest.FirstDayInIndustry.Year;
            if (yearInIndustry > 60)
            {
                return ServiceResponse.Failure<IUserDto>(t => t.FirstDayInCompany, $"User haven't could be In Industry for {yearInIndustry} years");
            }
            return ServiceResponse.Success();
        }
        private async Task SendMessageToChangeEmail(AuthenticationApiClient client, User user)
        {
            await client.ChangePasswordAsync(new ChangePasswordRequest()
            {
                ClientId = _config.ClientId,
                Connection = connection,
                Email = user.Email
            });
        }
        private async Task CreateAuth0User(User user, ManagementApiClient client,bool IsDevelop)
        {
            await client.Users.CreateAsync(new UserCreateRequest()
            {
                Email = user.Email,
                Blocked = false,
                EmailVerified = true,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.FirstName,
                UserId = user.Id.ToString(),
                Connection = connection,
                Password = IsDevelop ? _config.DefaultPassword : Guid.NewGuid().ToString(),
                VerifyEmail = false
            });
            await client.Roles.AssignUsersAsync(user.SystemRoleId, new AssignUsersRequest()
            {
                Users = new string[] { $"auth0|{user.Id}" }
            });
        }

        private async Task UpdateAuth0User(User user, ManagementApiClient client)
        {
            await client.Users.UpdateAsync($"auth0|{user.Id}", new UserUpdateRequest()
            {
                Email = user.Email,
                NickName = user.FirstName,
                FullName = $"{user.FirstName} {user.LastName}"
            });
        }

      
    }
}
