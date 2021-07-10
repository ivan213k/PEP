using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Projects.RequestModels;
using PerformanceEvaluationPlatform.Models.Projects.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        [HttpGet("projects")]
        public IActionResult Get([FromQuery] ProjectListFilterRequestModel filter)
        {
            var items = GetProjectListItemViewModels();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        private IEnumerable<ProjectListItemViewModel> GetFilteredItems(IEnumerable<ProjectListItemViewModel> items,
            ProjectListFilterRequestModel filter)
        {
            InitFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Title.Contains(filter.Search));
            }

            if (filter.CoordinatorIds != null)
            {
                items = items
                    .Where(t => filter.CoordinatorIds.Contains(t.CoordinatorId));
            }

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        private void InitFilter(ProjectListFilterRequestModel filter)
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
        private IEnumerable<ProjectListItemViewModel> GetProjectListItemViewModels()
        {
            var items = new List<ProjectListItemViewModel>
            {
                new ProjectListItemViewModel
                {
                    Title = "Project 1",
                    StartDate = new DateTime(2021,7,10),
                    Coordinator = "H1",
                    CoordinatorId = 1
                },
                new ProjectListItemViewModel
                {
                    Title = "Project 2",
                    StartDate = new DateTime(2021,7,1),
                    Coordinator = "H2",
                    CoordinatorId = 2
                },
                new ProjectListItemViewModel
                {
                    Title = "Project 3",
                    StartDate = new DateTime(2021,7,5),
                    Coordinator = "H3",
                    CoordinatorId = 3
                },
            };
            return items;
        }
    }
}
