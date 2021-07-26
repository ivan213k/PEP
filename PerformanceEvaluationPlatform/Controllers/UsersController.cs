using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.User.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Roles;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
using PerformanceEvaluationPlatform.Models.User.Domain;
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
        public UsersController(IUserRepository userRepository, IRolesRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository)); ;
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

            var itemsDto = await _userRepository.GetUsers(parameters);

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
            var userStateDtos = await _userRepository.GetUserStates();
            var userStatesviewModel = userStateDtos.Select(s => new UserStateViewModel { Id = s.Id, Name = s.Name });
            return Ok(userStatesviewModel);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);

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
           
            var user = users.FirstOrDefault(s => s.Id == id);
            if (user is null)
            {
                return NotFound();
            }
            var userRoles = 
            return Ok($"{user.Id} user with this Id was updated success");
        }

        //[HttpPost]
        //public IActionResult CreateUser([FromBody] CreateUserRequestModel createUserRequest)
        //{
           
        //    //var absoluteUri = string.Concat(HttpContext.Request.Scheme, "://", HttpContext.Request.Host.ToUriComponent());
        //    //string baseUri = string.Concat(absoluteUri, "/users/{id}").Replace("{id}", user.Id.ToString());
        //    //return Created(new Uri(baseUri), $"{user.FirstName} - was created success!!");
        //}

        
    }
}
