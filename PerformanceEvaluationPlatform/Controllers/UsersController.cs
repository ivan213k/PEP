using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.User.RequestModels;
using PerformanceEvaluationPlatform.Models.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<UserViewModel> users = new List<UserViewModel>()
            {
                new UserViewModel(){Id = 1,Email = "userExample@gor.com",FirstName ="Artur",LastName = "Grugon",Level = "Junior",
                LevelId =1,PreviousPEDate = new DateTime(2021,05,03),State = "Active",StateId=1,TeamName = "Sharks",
                Role = "Dev", RoleId = 1
                },
                    new UserViewModel(){Id =2,Email = "GodKiller@gmail.com",FirstName ="Kiril",LastName = "Krigan",Level = "Senior",
                LevelId =3,PreviousPEDate = new DateTime(2021,04,10),State = "Active",StateId=1,TeamName = "Gnomes",
                Role = "Architector", RoleId = 2
                },
                        new UserViewModel(){Id=3,Email = "bestmanager@ukr.net",FirstName ="Kristina",LastName = "Lavruk",Level = "Junior",
                LevelId =1,PreviousPEDate = new DateTime(2021,03,07),State = "Active",StateId=1,TeamName = "Sharks",
                Role = "Dev", RoleId = 1}
            };
        //GET api/users
        [HttpGet]
        public IActionResult GetUsers([FromQuery] UserSortingRequestModel userSorting, [FromQuery] UserFilterRequestModel userFilter, [FromQuery] PaginationFilterRequestModel userPagination)
        {
            var items = SortingUsers(userSorting, users);
            items = FilterUsers(userFilter, items);
            return Ok(items.Skip((userPagination.CurrentPage - 1) * userPagination.PageSize).Take(userPagination.PageSize));
        }


        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = users.FirstOrDefault(s => s.Id == id);

            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromBody]EditUserRequestModel editedUser)
        {
            var user = users.FirstOrDefault(s => s.Id == id);
            users.Remove(user);
            if(user is null)
            {
                return NotFound();
            }
            user = MappUser(user,editedUser);
            users.Add(user);
            return RedirectToAction("GetUser",new { id = user.Id});
        }

        

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = new List<RolesViewModel>()
            {
            new RolesViewModel(){Id= 1,Name="Dev"},
            new RolesViewModel(){Id= 2,Name="Architector"}
            };
            return Ok(roles);
        }

        [HttpGet("levels")]
        public IActionResult GetLeveles()
        {
            var roles = new List<LevelesViewModel>()
            {
            new LevelesViewModel(){Id= 1,Name="Junior"},
            new LevelesViewModel(){Id= 2,Name="Middle"},
            new LevelesViewModel(){Id= 3,Name="Senior"}
            };
            return Ok(roles);
        }


        private UserViewModel MappUser(UserViewModel user, EditUserRequestModel editedUser)
        {
            // З  використанням бази даних я би поновлював тільки айді елементів інших табилць.
            user.LastName = editedUser.LastName;
            user.FirstName = editedUser.FirstName;
            user.Level = editedUser.Level;
            user.LevelId = editedUser.LevelId;
            user.PreviousPEDate = editedUser.PreviousPEDate;
            user.Role = editedUser.Role;
            user.RoleId = editedUser.RoleId;  
            user.State = editedUser.State;
            user.StateId = editedUser.StateId;
            user.TeamName = editedUser.TeamName;
            return user;
        }

        private IEnumerable<UserViewModel> FilterUsers(UserFilterRequestModel userFilter, IEnumerable<UserViewModel> items)
        {
            if(userFilter.EmailFilter != null)
            {
                items = items.Where(s => s.Email.Contains(userFilter.EmailFilter));
            }

            if (userFilter.FullNameFilter != null)
            {
                items = items.Where(s => $"{s.FirstName} {s.LastName}".Contains(userFilter.FullNameFilter));
            }

            if (userFilter.NextPEDateFilter != null)
            {
                items = items.Where(s => s.NextPEDate == userFilter.NextPEDateFilter);
            }

            if (userFilter.PreviousPEDateFilter != null)
            {
                items = items.Where(s => s.PreviousPEDate == userFilter.PreviousPEDateFilter);
            }

            if (userFilter.RoleIdFilter != null)
            {
                items = items.Where(s => s.RoleId == userFilter.RoleIdFilter);
            }

            if (userFilter.StateIdFilter != null)
            {
                items = items.Where(s => s.StateId == userFilter.StateIdFilter);
            }

            return items;
        }

        private IEnumerable<UserViewModel> SortingUsers(UserSortingRequestModel userSorting,IEnumerable<UserViewModel> items)
        {
            if(userSorting != null)
            {
                switch (userSorting.UserNameSorting)
                {
                    case 0:
                            break;
                    case 1:
                       items = items.OrderBy(s => s.FirstName).ToList();
                            break;
                    case 2:
                        items =users.OrderByDescending(s => s.FirstName).ToList();
                        break;
                }

                switch (userSorting.UserNextPESorting)
                {
                    case 0:
                        break;
                    case 1:
                        items = users.OrderBy(s => s.NextPEDate).ToList();
                        break;
                    case 2:
                       items =  users.OrderByDescending(s => s.NextPEDate).ToList();
                        break;
                }

                switch (userSorting.UserPreviousPESorting)
                {
                    case 0:
                        break;
                    case 1:
                        items = users.OrderBy(s => s.PreviousPEDate).ToList();
                        break;
                    case 2:
                        items = users.OrderByDescending(s => s.PreviousPEDate).ToList() ;
                        break;
                }
            }
            return items;

        }
    }
}
