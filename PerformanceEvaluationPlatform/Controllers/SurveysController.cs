using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class SurveysController : ControllerBase
    {
        [Route("surveys")]
        public IActionResult Get()
        {
            var surveys = GetSurveyListItemViewModels();

            return Ok(surveys);
        }

        private IEnumerable<SurveyListItemViewModel> GetSurveyListItemViewModels() 
        {
            var items = new List<SurveyListItemViewModel> 
            {
                new SurveyListItemViewModel
                {
                    AppointmentDate = new DateTime(2021,7,10),
                    Assignee = "Test User",
                    Supervisor = "Admin User",
                    FormName = "Manual QA",
                    FormId = 1,
                    State = "Active",
                    StateId = 1
                },
                new SurveyListItemViewModel
                {
                    AppointmentDate = new DateTime(2021,7,11),
                    Assignee = "Test User 1",
                    Supervisor = "Admin User 1",
                    FormName = ".NET",
                    FormId = 2,
                    State = "Blocked",
                    StateId = 2
                },
                new SurveyListItemViewModel
                {
                    AppointmentDate = new DateTime(2021,7,12),
                    Assignee = "Test User 2",
                    Supervisor = "Admin User 2",
                    FormName = "JS",
                    FormId = 3,
                    State = "Active",
                    StateId = 3
                },
            };
            return items;
        }
    }
}
