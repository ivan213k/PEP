using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Models.Survey.RequestModels;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class SurveysController : ControllerBase
    {
        [Route("surveys")]
        [HttpGet]
        public IActionResult Get([FromQuery] SurveyListFilterRequestModel filter)
        {
            var surveys = GetSurveyListItemViewModels();
            surveys = GetFilteredItems(surveys, filter);
            return Ok(surveys);
        }

        [Route("surveys/states")]
        [HttpGet]
        public IActionResult GetStates()
        {
            var items = new List<SurveyStateListItemViewModel>
            {
                new SurveyStateListItemViewModel
                {
                    Id = 1,
                    Name = "Active"
                },
                new SurveyStateListItemViewModel
                {
                    Id = 2,
                    Name = "Blocked"
                }
            };

            return Ok(items);
        }

        private IEnumerable<SurveyListItemViewModel> GetSurveyListItemViewModels() 
        {
            var items = new List<SurveyListItemViewModel> 
            {
                new SurveyListItemViewModel
                {
                    AppointmentDate = new DateTime(2021,7,10),
                    Assignee = "Test User",
                    AssigneeId = 1,
                    Supervisor = "Admin User",
                    SupervisorId = 1,
                    FormName = "Manual QA",
                    FormId = 1,
                    State = "Active",
                    StateId = 1
                },
                new SurveyListItemViewModel
                {
                    AppointmentDate = new DateTime(2021,7,11),
                    Assignee = "Test User 1",
                    AssigneeId = 2,
                    Supervisor = "Admin User 1",
                    SupervisorId = 2,
                    FormName = ".NET",
                    FormId = 2,
                    State = "Blocked",
                    StateId = 2
                },
                new SurveyListItemViewModel
                {
                    AppointmentDate = new DateTime(2021,7,12),
                    Assignee = "Test User 2",
                    AssigneeId = 3,
                    Supervisor = "Admin User 2",
                    SupervisorId = 3,
                    FormName = "JS",
                    FormId = 3,
                    State = "Active",
                    StateId = 1
                },
            };
            return items;
        }

        private IEnumerable<SurveyListItemViewModel> GetFilteredItems(IEnumerable<SurveyListItemViewModel> items,
            SurveyListFilterRequestModel filter) 
        {
            InitFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.FormName.Contains(filter.Search) || t.Assignee.Contains(filter.Search));
            }

            if (filter.AssigneeIds != null)
            {
                items = items
                    .Where(t => filter.AssigneeIds.Contains(t.AssigneeId));
            }

            if (filter.SupervisorIds != null)
            {
                items = items
                    .Where(t => filter.SupervisorIds.Contains(t.SupervisorId));
            }

            if (filter.AppointmentDateFrom != null) 
            {
                items = items.
                    Where(t => t.AppointmentDate >= filter.AppointmentDateFrom.Value);
            }

            if (filter.AppointmentDateTo != null)
            {
                items = items.
                    Where(t => t.AppointmentDate <= filter.AppointmentDateTo.Value);
            }

            if (filter.StateIds != null)
            {
                items = items.
                    Where(t => filter.StateIds.Contains(t.StateId));
            }

            items = GetSortedItems(items, filter);

            items = items
               .Skip(filter.Skip.Value)
               .Take(filter.Take.Value);

            return items;
        }

        private IEnumerable<SurveyListItemViewModel> GetSortedItems(IEnumerable<SurveyListItemViewModel> surveys, SurveyListFilterRequestModel filter)
        {
            if (filter.FormNameSortOrder != SortOrder.Undefined)
            {
                if (filter.FormNameSortOrder == SortOrder.Ascending)
                    surveys = surveys.OrderBy(r => r.FormName);
                else
                    surveys = surveys.OrderByDescending(r => r.FormName);
            }
            if (filter.AssigneeNameSortOrder != SortOrder.Undefined)
            {
                if (filter.AssigneeNameSortOrder == SortOrder.Ascending)
                    surveys = surveys.OrderBy(r => r.Assignee);
                else
                    surveys = surveys.OrderByDescending(r => r.Assignee);
            }
            return surveys;
        }

        private void InitFilter(SurveyListFilterRequestModel filter)
        {
            if (filter.Skip is null)
            {
                filter.Skip = 0;
            }

            if (filter.Take is null)
            {
                filter.Take = 30;
            }
        }
    }
}
