using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.User.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.User;
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
        private static List<User> users = new List<User>()
            {
                new User(){Id = 1,Email = "userExample@gor.com",FirstName ="Artur",LastName = "Grugon",LevelName = "Junior",
                LevelId =1,PreviousPEDate = new DateTime(2020,12,03),StateName = "Active",StateId=1,TeamName = "Sharks",RoleName = "Dev", RoleId = 1,
                 EnglishLevelId=1,EnglishLevelName ="Proficiency",FirstDayInCompany = new DateTime(2020,03,07),ProjectName="PO for Progars",ProjectId=1,NextPEDate = new DateTime(2021,08,21),
                        YearsOfExpirience=6,PreviousPEs = new List<DateTime>(){
                            new DateTime(2020,06,20),
                            new DateTime(2020,12,03)
                } },

                    new User(){Id =2,Email = "KodKiller@gmail.com",FirstName ="Kiril",LastName = "Krigan",LevelName = "Senior",
                LevelId =3,PreviousPEDate = new DateTime(2021,04,10),StateName = "Active",StateId=1,TeamName = "Gnomes",RoleName = "Architector", RoleId = 2,
                EnglishLevelId=1,EnglishLevelName ="Advanced",FirstDayInCompany = new DateTime(2019,03,07),ProjectName="PO for Progars",ProjectId=1,NextPEDate = new DateTime(2022,06,21),
                        YearsOfExpirience=6,PreviousPEs = new List<DateTime>(){
                            new DateTime(2020,06,20),
                            new DateTime(2021,04,10)
                        } },

                        new User(){Id=3,Email = "bestmanager@ukr.net",FirstName ="Kristina",LastName = "Lavruk",LevelName = "Junior",
                LevelId =1,PreviousPEDate = new DateTime(2021,03,07),StateName = "Active",StateId=1,TeamName = "Sharks",RoleName = "Dev", RoleId = 1,
                        EnglishLevelId=1,EnglishLevelName ="Intermidiate",FirstDayInCompany = new DateTime(2016,03,07),ProjectName="PO for Progars",ProjectId=1,NextPEDate = new DateTime(2022,03,07),
                        YearsOfExpirience=6,PreviousPEs = new List<DateTime>(){
                            new DateTime(2017,03,07),
                            new DateTime(2018,03,07),
                            new DateTime(2019,03,07),
                            new DateTime(2020,03,07),
                            new DateTime(2021,03,07)
                        } }
            };
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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


        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = users.FirstOrDefault(s => s.Id == id);

            if (user is null)
            {
                return NotFound();
            }
            return Ok(MapToUserDeailViewModel(user));
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromBody] EditUserRequestModel editedUser)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var user = users.FirstOrDefault(s => s.Id == id);
            if (user is null)
            {
                return NotFound();
            }
            UpdateUser(user, editedUser);
            return Ok($"{user.Id} user with this Id was updated success");
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequestModel createUserRequest)
        {
            if (createUserRequest == null)
            {
                ModelState.AddModelError("", "You didnt create user");
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var validation = users.Any(s => string.Equals(s.Email, createUserRequest.Email, StringComparison.InvariantCultureIgnoreCase));
            if (validation is true)
            {
                ModelState.AddModelError("", "User with the same email is already exists");
                return Conflict(ModelState);
            }

            var user = new User()
            {
                Id = users.Count + 1,
                Email = createUserRequest.Email,
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                NextPEDate = createUserRequest.NextPEDate,
                FirstDayInCompany = createUserRequest.FirstDayInCompany,
                YearsOfExpirience = createUserRequest.YearsOfExpirience,
                EnglishLevelId = createUserRequest.EnglishLevelId,
                LevelId = createUserRequest.LevelId,
                ProjectId = createUserRequest.ProjectId,
                RoleId = createUserRequest.RoleId,
                StateId = createUserRequest.StateId,
                TeamId = createUserRequest.TeamId
            };
            users.Add(user);
            var absoluteUri = string.Concat(HttpContext.Request.Scheme, "://", HttpContext.Request.Host.ToUriComponent());
            string baseUri = string.Concat(absoluteUri, "/users/{id}").Replace("{id}", user.Id.ToString());
            return Created(new Uri(baseUri), $"{user.FirstName} - was created success!!");
        }

        private void UpdateUser(User user, EditUserRequestModel editedUser)
        {
            user.Email = editedUser.Email;
            user.FirstName = editedUser.FirstName;
            user.LastName = editedUser.LastName;
            user.LevelName = editedUser.LevelName;
            user.NextPEDate = editedUser.NextPEDate;
            user.TeamName = editedUser.TeamName;
            user.RoleName = editedUser.RoleName;
            user.EnglishLevelName = editedUser.EnglishLevelName;
            user.FirstDayInCompany = editedUser.FirstDayInCompany;
            user.ProjectName = editedUser.ProjectName;

        }
        private UserDetailViewModel MapToUserDeailViewModel(User user)
        {
            return new UserDetailViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LevelName = user.LevelName,
                PreviousPEDate = user.PreviousPEDate,
                NextPEDate = user.NextPEDate,
                StateName = user.StateName,
                TeamName = user.TeamName,
                RoleName = user.RoleName,
                PreviousPEs = user.PreviousPEs,
                EnglishLevelName = user.EnglishLevelName,
                FirstDayInCompany = user.FirstDayInCompany,
                ProjectName = user.ProjectName,
                YearsInCompany = user.YearsInCompany,
                YearsOfExpirience = user.YearsOfExpirience
            };
        }
       
        private IEnumerable<User> FilterUsers(UserFilterRequestModel userFilter, IEnumerable<User> items)
        {
            if (userFilter.EmailOrName != null)
            {
                items = items.Where(s => s.Email.ToLower().Contains(userFilter.EmailOrName.ToLower()) || $"{s.FirstName} {s.LastName}".ToLower().Contains(userFilter.EmailOrName.ToLower()));
            }

            if (userFilter.NextPEDate != null)
            {
                items = items.Where(s => s.NextPEDate <= userFilter.NextPEDate);
            }

            if (userFilter.PreviousPEDate != null)
            {
                items = items.Where(s => s.PreviousPEDate == userFilter.PreviousPEDate);
            }

            if (userFilter.RoleIds != null)
            {
                items = items.Where(s => userFilter.RoleIds.Contains(s.RoleId));
            }

            if (userFilter.StateIds != null)
            {
                items = items.Where(s => userFilter.StateIds.Contains(s.StateId));
            }

            return items;
        }

        private IEnumerable<User> SortingUsers(UserSortingRequestModel userSorting, IEnumerable<User> items)
        {
            if (userSorting != null)
            {
                switch (userSorting.UserName)
                {
                    case 0:
                        break;
                    case 1:
                        items = items.OrderBy(s => s.FirstName).ToList();
                        break;
                    case 2:
                        items = users.OrderByDescending(s => s.FirstName).ToList();
                        break;
                }

                switch (userSorting.UserNextPE)
                {
                    case 0:
                        break;
                    case 1:
                        items = users.OrderBy(s => s.NextPEDate).ToList();
                        break;
                    case 2:
                        items = users.OrderByDescending(s => s.NextPEDate).ToList();
                        break;
                }

                switch (userSorting.UserPreviousPE)
                {
                    case 0:
                        break;
                    case 1:
                        items = users.OrderBy(s => s.PreviousPEDate).ToList();
                        break;
                    case 2:
                        items = users.OrderByDescending(s => s.PreviousPEDate).ToList();
                        break;
                }
            }
            return items;

        }
    }
}
