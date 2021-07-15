using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Team.RequestModels;
using PerformanceEvaluationPlatform.Models.Team.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class TeamsController : ControllerBase
    {
        [HttpGet("teams")]
        public IActionResult Get([FromQuery]TeamListFilterRequestModel filter)
        {
            var items = GetTeamsListItemViewModels();
            items = GetFilteredItems(items, filter);

            return Ok(items);
        }

        [HttpGet("teams/{id}")]
        public IActionResult GetTeamDetails(int id)
        {
            var items = GetTeamsListItemViewModels();
            var item = items.FirstOrDefault(t => t.TeamId == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        private IEnumerable<TeamListViewModel> GetFilteredItems(IEnumerable<TeamListViewModel> items, TeamListFilterRequestModel filter)
        {
            InitFilter(filter);

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.TeamTitle.Contains(filter.Search));
            }

            if (filter.ProjectIds != null)
            {
                items = items
                    .Where(t => filter.ProjectIds.Contains(t.ProjectId));
            }

            return items;
        }

        private void InitFilter(TeamListFilterRequestModel filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }

            if (filter.Take == null)
            {
                filter.Take = 30;
            }
        }

        private static IEnumerable<TeamListViewModel> GetTeamsListItemViewModels()
        {
            var items = new List<TeamListViewModel>
            {
                new TeamListViewModel
                {
                    TeamTitle = "Team1",
                    TeamId = 101,
                    ProjectTitle = "Project1",
                    ProjectId = 1,
                    TeamSize = 5,
                    TeamLead = "User1"
                },
                new TeamListViewModel
                {
                    TeamTitle = "Team2",
                    TeamId = 102,
                    ProjectTitle = "Project2",
                    ProjectId = 2,
                    TeamSize = 10,
                    TeamLead = "User2"
                },
                new TeamListViewModel
                {
                    TeamTitle = "Team3",
                    TeamId = 103,
                    ProjectTitle = "Project11",
                    ProjectId = 1,
                    TeamSize = 15,
                    TeamLead = "User3"
                },
                new TeamListViewModel
                {
                    TeamTitle = "Team4",
                    TeamId = 104,
                    ProjectTitle = "Project4",
                    ProjectId = 4,
                    TeamSize = 20,
                    TeamLead = "User4"
                }
            };

            return items;
        }
    }
}
