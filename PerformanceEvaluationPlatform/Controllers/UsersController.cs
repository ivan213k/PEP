using Microsoft.AspNetCore.Mvc;
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
        

        [HttpGet]
        public IActionResult Get([FromQuery] UserSortingRequestModel userSorting,[FromQuery] UserFilterRequestModel userFilter)
        {
           var items=  SortingUsers(userSorting,users);
            items = FilterUsers(userFilter, items);
            var resultItems = MapToUserViewModel(items);
            return Ok(resultItems.Skip((userFilter.Skip - 1) * userFilter.Take).Take(userFilter.Take));
        }

        private IEnumerable<UserViewModel> MapToUserViewModel(IEnumerable<User> items)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(var item in items)
            {
                users.Add(new UserViewModel()
                {
                    Id = item.Id,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    LevelName = item.LevelName,
                    LevelId = item.LevelId,
                    PreviousPEDate = item.PreviousPEDate,
                    NextPEDate = item.NextPEDate,
                    StateName = item.StateName,
                    StateId = item.StateId,
                    TeamName = item.TeamName,
                    RoleName = item.RoleName,
                    RoleId = item.RoleId
                });
            }
            return users;
        }

        private IEnumerable<User> FilterUsers(UserFilterRequestModel userFilter, IEnumerable<User> items)
        {
            if(userFilter.EmailOrName != null)
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

        private IEnumerable<User> SortingUsers(UserSortingRequestModel userSorting,IEnumerable<User> items)
        {
            if(userSorting != null)
            {
                switch (userSorting.UserName)
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

                switch (userSorting.UserNextPE)
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

                switch (userSorting.UserPreviousPE)
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
