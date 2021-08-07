using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Roles;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;
using PerformanceEvaluationPlatform.DAL.Repositories.Teams;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
using PerformanceEvaluationPlatform.Models.User.RequestModels;
using PerformanceEvaluationPlatform.Models.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private const int ActiveState = 1;
        private const int SuspendState = 2;
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _roleRepository;
        private readonly ITeamsRepository _teamRepository;
        private readonly ISurveysRepository _surveysRepository;
        private readonly IConfiguration _config;
        public UsersController(IUserRepository userRepository, IRolesRepository roleRepository, ITeamsRepository teamRepository, ISurveysRepository surveysRepository, IConfiguration config)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserSortingRequestModel userSorting, [FromQuery] UserFilterRequestModel userFilter)
        {
            var parameters = new UserFilterDto()
            {
                NextPeDate = userFilter.NextPEDate,
                PreviousPeDate = userFilter.PreviousPEDate,
                RoleIds = userFilter.RoleIds,
                Search = userFilter.EmailOrName,
                Skip = userFilter.Skip,
                StateIds = userFilter.StateIds,
                Take = userFilter.Take,
                UserNameSort = userSorting.UserName,
                UserNextPE = userSorting.UserNextPE,
                UserPreviousPE = userSorting.UserPreviousPE
            };

            var itemsDto = await _userRepository.GetList(parameters);

            var itemViewModel = itemsDto.Select(s => new UserViewModel()
            {
                Id = s.Id,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                LevelName = s.LevelName,
                NextPEDate = s.NextPE,
                StateName = s.StateName,
                PreviousPEDate = s.PreviousPE,
                RoleName = s.RoleName,
                TeamName = s.TeamName
            });

            return Ok(itemViewModel);
        }

        [HttpGet("userstate")]
        public async Task<IActionResult> GetUserStates()
        {
            var userStateDtos = await _userRepository.GetStates();
            var userStatesviewModel = userStateDtos.Select(s => new UserStateViewModel { Id = s.Id, Name = s.Name });
            return Ok(userStatesviewModel);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetDetail(id);

            if (user is null)
            {
                return NotFound();
            }
            var userViewModel = new UserDetailViewModel()
            {
                Email = user.Email,
                EnglishLevelName = user.EnglishLevel,
                FirstDayInCompany = user.FirstDayInCompany,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TechnicalLevelName = user.TechnicalLevel,
                NextPEDate = user.NextPeDate,
                PreviousPEDate = user.PreviousPEDate,
                PreviousPEs = user.PreviousPes,
                ProjectName = user.Project,
                RoleNames = user.Role,
                StateName = user.State,
                TeamName = user.Team,
                YearsInCompany = user.YearsInCompany,
                YearsOfExpirience = user.YearsOfExpirience
            };
            return Ok(userViewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] EditUserRequestModel editedUser)
        {

            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return NotFound();
            }
            var existingUser = await _userRepository.Get(editedUser.Email);
            if(existingUser.Id != id)
            {
                ModelState.AddModelError(editedUser.Email, "User with the same email is already exists");
                return Conflict(ModelState);
            }

            await ValidateUser(editedUser);
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            UpdateUser(user, editedUser);
            await _userRepository.Save();


            return Ok($"{user.Id} user with this Id was updated success");
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel createUserRequest)
        {
            var existingUser = await _userRepository.Get(createUserRequest.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(createUserRequest.Email, "User with the same email is already exists");
                return Conflict(ModelState);
            }

            await ValidateUser(createUserRequest);
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            var userRoleMaps = createUserRequest.RoleIds.Select(s => new UserRoleMap { RoleId = s }).ToList();
            var user = new User()
            {
                Email = createUserRequest.Email,
                EnglishLevelId = createUserRequest.EnglishLevelId,
                FirstDayInCompany = createUserRequest.FirstDayInCompany,
                FirstDayInIndustry = createUserRequest.FirstDayInIndustry,
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                StateId = ActiveState,
                TeamId = createUserRequest.TeamId,
                TechnicalLevelId = createUserRequest.TechnicalLevelId,
                Roles = userRoleMaps
            };
            await _userRepository.Create(user);
            await _userRepository.Save();

            await CreateAuth0User(user);
           
            var absoluteUri = string.Concat(HttpContext.Request.Scheme, "://", HttpContext.Request.Host.ToUriComponent());
            string baseUri = string.Concat(absoluteUri, "/users/{id}").Replace("{id}", user.Id.ToString());
            return Created(new Uri(baseUri), $"{user.FirstName} - was created success!!");
        }

        [HttpPut("{id:int}/activate")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var user = await _userRepository.Get(id);

            if(user is null)
            {
                return NotFound();
            }

            if(user.StateId == ActiveState)
            {
                return Ok("User is already with active state");
            }
            user.StateId = ActiveState;
            await _userRepository.Save();
            return Ok("User successfully change his state, now its active!");
        }
        [HttpPut("{id:int}/suspend")]
        public async Task<IActionResult> SuspendUser(int id)
        {
            var user = await _userRepository.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            if (user.StateId == SuspendState)
            {
                return Ok("User is already with suspend state");
            }
            user.StateId = SuspendState;
            await _userRepository.Save();
            return Ok("User successfully change his state, now its ");
        }


        private async Task CreateAuth0User( User user)
        {
            var authClient = new AuthenticationApiClient(_config["Auth0:Domain"]);
            AccessTokenResponse token = await authClient.GetTokenAsync(new ClientCredentialsTokenRequest()
            {
                ClientId = _config["Auth0:ClientId"],
                ClientSecret = _config["Auth0:ClientSecret"],
                SigningAlgorithm = JwtSignatureAlgorithm.RS256,
                Audience = $"https://{_config["Auth0:Domain"]}/api/v2/"
            });
            //eU63V466lzj7KRvHpIMng

            var client = new ManagementApiClient(token.AccessToken, new Uri($"https://{_config["Auth0:Domain"]}/api/v2"));
            await client.Users.CreateAsync(new Auth0.ManagementApi.Models.UserCreateRequest()
            {
                Email = user.Email,
                Blocked = false,
                EmailVerified = true,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.FirstName,
                UserId = Guid.NewGuid().ToString(),
                Connection = "Username-Password-Authentication",
                Password = Guid.NewGuid().ToString(),
                VerifyEmail = false
            });
        }

        private async Task ValidateUser(IUserRequest userRequest)
        {
         


            List<int> notValidUserRoles = new List<int>();
            foreach (var item in userRequest.RoleIds)
            {
                var role = await _roleRepository.Get(item);
                if (role == null)
                {
                    notValidUserRoles.Add(item);
                }
            }
            if (notValidUserRoles.Any())
            {
                foreach (var item in notValidUserRoles)
                {
                    ModelState.AddModelError(item.ToString(), "Role with this Id doesn't exists");
                }
            }

            var userTeam = await _teamRepository.Get(userRequest.TeamId);
            if (userTeam is null)
            {
                ModelState.AddModelError(userRequest.TeamId.ToString(), "Team with this Id doesn't exists");
            }

            var userTechnicalLevel = await _surveysRepository.GetLevel(userRequest.TechnicalLevelId);
            if (userTechnicalLevel is null)
            {
                ModelState.AddModelError(userRequest.TechnicalLevelId.ToString(), "Level with this Id doesn't exists");
            }
            var userEnglishLevel = await _surveysRepository.GetLevel(userRequest.EnglishLevelId);
            if (userEnglishLevel is null)
            {
                ModelState.AddModelError(userRequest.EnglishLevelId.ToString(), "Level with this Id doesn't exists");
            }

            if (userRequest.FirstDayInCompany < userRequest.FirstDayInIndustry)
            {
                ModelState.AddModelError(userRequest.FirstDayInCompany.ToString(), "User couldn't worked In company before he  was in industry");
            }

            var yearInCompany = DateTime.Now.Year - userRequest.FirstDayInCompany.Year;
            if (yearInCompany > 60)
            {
                ModelState.AddModelError(userRequest.FirstDayInCompany.ToString(), $"User haven't could work In company for {yearInCompany} years");
            }
            var yearInIndustry = DateTime.Now.Year - userRequest.FirstDayInIndustry.Year;
            if (yearInIndustry > 60)
            {
                ModelState.AddModelError(userRequest.FirstDayInCompany.ToString(), $"User haven't could be In Industry for {yearInIndustry} years");
            }
        }
        private void UpdateUser(User user, EditUserRequestModel editedUser)
        {
            user.Email = editedUser.Email;
            user.EnglishLevelId = editedUser.EnglishLevelId;
            user.FirstDayInCompany = editedUser.FirstDayInCompany;
            user.FirstDayInIndustry = editedUser.FirstDayInIndustry;
            user.FirstName = editedUser.FirstName;
            user.LastName = editedUser.LastName;
            user.TeamId = editedUser.TeamId;
            user.TechnicalLevelId = editedUser.TechnicalLevelId;
            _userRepository.Update(editedUser.RoleIds, user.Id);
        }

    }
}
