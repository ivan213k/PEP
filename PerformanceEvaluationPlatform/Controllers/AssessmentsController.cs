using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class AssessmentsController : ControllerBase
    {

        [Route("assessments/groups")]
        public IActionResult GetAssesmentGroup()
        {
            var items = new List<FieldAssesmentGroupListItemViewModel>
            {
                new FieldAssesmentGroupListItemViewModel
                {
                    Id = 1,
                    Name = "NoAssesment"
                },
                new FieldAssesmentGroupListItemViewModel
                {
                    Id = 2,
                    Name = "A-F Group"
                },
            };

            return Ok(items);
        }

    }
}
