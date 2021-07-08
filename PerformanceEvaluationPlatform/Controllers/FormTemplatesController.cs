using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormTemplatesController: ControllerBase
    {
        [Route("formtemplates")]
        public IActionResult Get()
        {
            var items = GetFormTemplatesListItemViewModel();
            return Ok(items);
        }

        private IEnumerable<FormTemplatesListItemViewModel> GetFormTemplatesListItemViewModel()
        {
            var items = new List<FormTemplatesListItemViewModel> { 
                new FormTemplatesListItemViewModel{
                    Name = "Middle Back-End Dev",
                    Version = "12",
                    Status = "Draft",
                    AssesmentGroup = "A-F Marks",
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
                new FormTemplatesListItemViewModel{
                    Name = "Middle Front-End Dev",
                    Version = "1",
                    Status = "Active",
                    AssesmentGroup = "A-F Marks",
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
            };
            return items;
        }
    }
}
