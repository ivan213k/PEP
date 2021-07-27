using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.User.Dao;
using PerformanceEvaluationPlatform.DAL.Models.User.Dto;
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
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _roleRepository;
        private readonly ITeamsRepository _teamRepository;
        private readonly ISurveysRepository _surveysRepository;
        public UsersController(IUserRepository userRepository, IRolesRepository roleRepository, ITeamsRepository teamRepository, ISurveysRepository surveysRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
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
                Take=userFilter.Take,
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
           
            var user =await  _userRepository.Get(id);
            if (user is null)
            {
                return NotFound();
            }

            var userEmailValidation =await  _userRepository.Get(editedUser.Email);
            if (userEmailValidation != null&& userEmailValidation.Id != id)
            {
                ModelState.AddModelError(editedUser.Email,"User with the same email is already exists");
                return Conflict(ModelState);
            }

            List<int> NotValidUserRoles = new List<int>();
            foreach (var item in editedUser.RoleIds)
            {
                var role = await _roleRepository.Get(item);
                if(role == null)
                {
                    NotValidUserRoles.Add(item);
                }
            }
            if(NotValidUserRoles.Count!=0)
            {
                foreach (var item in NotValidUserRoles)
                {
                    ModelState.AddModelError(item.ToString(), "Role with this Id doesn't exists");
                }
                return BadRequest(ModelState);
            }

            var userTeam = await _teamRepository.Get(editedUser.TeamId);
            if(userTeam is null)
            {
                ModelState.AddModelError(editedUser.TeamId.ToString(), "Team with this Id doesn't exists");
                return BadRequest(ModelState);
            }

            var userTechnicalLevel =await  _surveysRepository.GetLevel(editedUser.TechnicalLevelId);
            if(userTechnicalLevel is null)
            {
                ModelState.AddModelError(editedUser.TechnicalLevelId.ToString(), "Level with this Id doesn't exists");
                return BadRequest(ModelState);
            }
            var userEnglishLevel = await _surveysRepository.GetLevel(editedUser.EnglishLevelId);
            if(userEnglishLevel is null)
            {
                ModelState.AddModelError(editedUser.EnglishLevelId.ToString(), "Level with this Id doesn't exists");
                return BadRequest(ModelState);
            }

             if(editedUser.FirstDayInCompany < editedUser.FirstDayInIndustry)
             {
                ModelState.AddModelError(editedUser.FirstDayInCompany.ToString(), "User couldn't worked In company before he  was in industry");
                return BadRequest(ModelState);
             }

            var yearInCompany = DateTime.Now.Year - editedUser.FirstDayInCompany.Year;
             if (yearInCompany > 60)
             {
                ModelState.AddModelError(editedUser.FirstDayInCompany.ToString(), $"User haven't could work In company for {yearInCompany} years");
                return BadRequest(ModelState);
             }
            var yearInIndustry = DateTime.Now.Year - editedUser.FirstDayInIndustry.Year;
            if (yearInIndustry > 60)
            {
                ModelState.AddModelError(editedUser.FirstDayInCompany.ToString(), $"User haven't could be In Industry for {yearInIndustry} years");
                return BadRequest(ModelState);
            }

            UpdateUser(user,editedUser);
            await _userRepository.Save();
            

            return Ok($"{user.Id} user with this Id was updated success");
        }


        //[HttpPost]
        //public IActionResult CreateUser([FromBody] CreateUserRequestModel createUserRequest)
        //{

        //    //var absoluteUri = string.Concat(HttpContext.Request.Scheme, "://", HttpContext.Request.Host.ToUriComponent());
        //    //string baseUri = string.Concat(absoluteUri, "/users/{id}").Replace("{id}", user.Id.ToString());
        //    //return Created(new Uri(baseUri), $"{user.FirstName} - was created success!!");
        //}

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
            _userRepository.Update(editedUser.RoleIds,user.Id);
        }

    }
}
