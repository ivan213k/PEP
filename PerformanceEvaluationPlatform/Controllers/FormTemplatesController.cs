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

        [Route("formtemplates/statuses")]
        public IActionResult GetStatuses()
        {
            var items = GetFormTemplatesStatusesListItemViewModel();
            return Ok(items);
        }

        [Route("formtemplates/assesmentgroups")]
        public IActionResult GetAssesmentGroups()
        {
            var items = GetFormTemplatesAssesmentGroupsListItemViewModel();
            return Ok(items);
        }

        private IEnumerable<FormTemplateStatusListItemViewModel> GetFormTemplatesStatusesListItemViewModel()
        {
            var items = new List<FormTemplateStatusListItemViewModel>
            {
                new FormTemplateStatusListItemViewModel
                {
                    Id = 1,
                    Name = "Active"
                },
                new FormTemplateStatusListItemViewModel
                {
                    Id = 2,
                    Name = "Draft"
                }
            };
            return items;
        }

        private IEnumerable<FormTemplateAssesmentGroupListItemViewModel> GetFormTemplatesAssesmentGroupsListItemViewModel()
        {
            var items = new List<FormTemplateAssesmentGroupListItemViewModel>
            {
                new FormTemplateAssesmentGroupListItemViewModel
                {
                    Id = 1,
                    Name = "A-F Marks"
                },
                new FormTemplateAssesmentGroupListItemViewModel
                {
                    Id = 2,
                    Name = "5 points"
                }
            };
            return items;
        }

        private IEnumerable<FormTemplateListItemViewModel> GetFormTemplatesListItemViewModel()
        {
            var items = new List<FormTemplateListItemViewModel> { 
                new FormTemplateListItemViewModel{
                    Name = "Middle Back-End Dev",
                    Version = 12,
                    Status = "Draft",
                    StatusId = 2,
                    AssesmentGroup = "A-F Marks",
                    AssesmentGroupId = 1,
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
                new FormTemplateListItemViewModel{
                    Name = "Middle Front-End Dev",
                    Version = 1,
                    Status = "Active",
                    StatusId = 1,
                    AssesmentGroup = "A-F Marks",
                    AssesmentGroupId = 1,
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
                new FormTemplateListItemViewModel{
                    Name = "Junior Front-End Dev",
                    Version = 1,
                    Status = "Active",
                    StatusId = 1,
                    AssesmentGroup = "5 points",
                    AssesmentGroupId = 2,
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
            };
            return items;
        }
    }
}
