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
        private static List<UserViewModel> users;
        public UsersController()
        {
            users = new List<UserViewModel>()
            {
                new UserViewModel(){Email = "userExample@gor.com",FirstName ="Artur",LastName = "Grugon",Level = "Junior",
                LevelId =1,PreviousPEDate = new DateTime(2021,05,03),State = "Active",StateId=1,TeamName = "Sharks",
                Role = "Dev", RoleId = 1
                },
                    new UserViewModel(){Email = "GodKiller@gmail.com",FirstName ="Kiril",LastName = "Krigan",Level = "Senior",
                LevelId =3,PreviousPEDate = new DateTime(2021,04,10),State = "Active",StateId=1,TeamName = "Gnomes",
                Role = "Architector", RoleId = 2
                },
                        new UserViewModel(){Email = "bestmanager@ukr.net",FirstName ="Kristina",LastName = "Lavruk",Level = "Junior",
                LevelId =1,PreviousPEDate = new DateTime(2021,03,07),State = "Active",StateId=1,TeamName = "Sharks",
                Role = "Dev", RoleId = 1}
            };
        }

        //GET api/users
        [HttpGet]
        public IActionResult GetUsers([FromQuery] UserSortingRequestModel userSorting)
        {
            var items =SortingUsers(userSorting,users); 
            return Ok(items);
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
