using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Teams.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Teams;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
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
        private readonly ITeamsRepository _teamRepository;

        public TeamsController(ITeamsRepository teamRepository)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        }

        [HttpGet("teams")]
        public async Task<IActionResult> Get([FromQuery]TeamListFilterRequestModel filter)
        {
            var filterDto = new TeamListFilterDto
            {
                Search = filter.Search,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = (int?)filter.OrderByTeamTitle,
                ProjectTitleSortOrder = (int?)filter.OrderByProjectTitle,
                TeamSizeSortOrder = (int?)filter.OrderByTeamSize,
                ProjectIds = filter.ProjectIds
            };

            var itemsDto = await _teamRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new TeamListViewModel 
            { 
                Id = t.Id,
                Title = t.Title,         
                ProjectTitle = t.ProjectTitle,
                ProjectId = t.ProjectId,
                Size = t.Size,
                TeamLead = t.TeamLead
            });

            return Ok(items);
        }

        [HttpGet("teams/{id}")]
        public IActionResult GetTeamDetails(int id)
        {
            var items = GetTeamsListItemViewModels();
            var item = items.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        private IEnumerable<TeamListViewModel> GetOrderedItems(IEnumerable<TeamListViewModel> items, TeamListFilterRequestModel filter)
        {
            if (filter.OrderByTeamTitle != null)
            {
                if (filter.OrderByTeamTitle == SortOrder.Ascending)
                    items = items.OrderBy(t => t.Title);
                else
                    items = items.OrderByDescending(t => t.Title);
            } 
            else
            {
                items = items.OrderBy(t => t.Title);
            }

            if (filter.OrderByProjectTitle != null)
            {
                if (filter.OrderByProjectTitle == SortOrder.Ascending)
                    items = items.OrderBy(t => t.ProjectTitle);
                else
                    items = items.OrderByDescending(t => t.ProjectTitle);
            }

            if (filter.OrderByTeamSize != null)
            {
                if (filter.OrderByTeamSize == SortOrder.Ascending)
                    items = items.OrderBy(t => t.Size);
                else
                    items = items.OrderByDescending(t => t.Size);
            }

            return items;
        }

        [HttpPost("teams")]
        public IActionResult AddNewTeam([FromBody] AddNewTeamRequestModel team)
        {
            var items = GetTeamsListItemViewModels();

            if (team == null)
            {
                return BadRequest();
            }

            bool teamAlreadyExists = items.Any(t => t.Title == team.Title);

            if (teamAlreadyExists)
            {
                ModelState.AddModelError("", "Team with such title already exists.");

                return Conflict(ModelState);
            }

            var newTeam = new TeamListViewModel
            {
                Title = team.Title,
                ProjectId = team.ProjectId,
                Size = team.Size,
                TeamLead = team.TeamLead
            };

            items = items.Append(newTeam);

            return Ok(newTeam);
        }

        private static IEnumerable<TeamListViewModel> GetTeamsListItemViewModels()
        {
            var items = new List<TeamListViewModel>
            {
                new TeamListViewModel
                {
                    Title = "Team1",
                    Id = 101,
                    ProjectTitle = "Project1",
                    ProjectId = 1,
                    Size = 5,
                    TeamLead = "User1"
                },
                new TeamListViewModel
                {
                    Title = "ATeam2",
                    Id = 102,
                    ProjectTitle = "Project2",
                    ProjectId = 2,
                    Size = 10,
                    TeamLead = "User2"
                },
                new TeamListViewModel
                {
                    Title = "CTeam3",
                    Id = 103,
                    ProjectTitle = "Project1",
                    ProjectId = 1,
                    Size = 15,
                    TeamLead = "User3"
                },
                new TeamListViewModel
                {
                    Title = "BTeam4",
                    Id = 104,
                    ProjectTitle = "Project3",
                    ProjectId = 3,
                    Size = 20,
                    TeamLead = "User4"
                }
            };

            return items;
        }
    }
}
