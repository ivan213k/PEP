using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormTemplatesController: ControllerBase
    {
        [HttpGet("formtemplates")]
        public IActionResult Get([FromQuery]FormTemplateListFilterOrderRequestModel filter)
        {
            var items = GetFormTemplatesListItemViewModel();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        [HttpGet("formtemplates/statuses")]
        public IActionResult GetStatuses()
        {
            var items = GetFormTemplatesStatusesListItemViewModel();
            return Ok(items);
        }

        private IEnumerable<FormTemplateListItemViewModel> GetFilteredItems(IEnumerable<FormTemplateListItemViewModel> items, FormTemplateListFilterOrderRequestModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items.Where(i => i.Name.Contains(filter.Search));
            }
            if (filter.StatusIds != null)
            {
                items = items
                    .Where(i => filter.StatusIds.Contains(i.StatusId));
            }
            if (filter.AssesmentGroupIds != null)
            {
                items = items
                    .Where(i => filter.AssesmentGroupIds.Contains(i.AssesmentGroupId));
            }

            items = GetSortedItems(items, filter);

            items = items.Skip(filter.Skip.Value).Take(filter.Take.Value);

            return items;
        }

        private IEnumerable<FormTemplateListItemViewModel> GetSortedItems(IEnumerable<FormTemplateListItemViewModel> items, FormTemplateListFilterOrderRequestModel filter)
        {
            if (filter.NameSortOrder != null)
            {
                if (filter.NameSortOrder == SortOrder.Ascending)
                    items = items.OrderBy(i => i.Name);
                else
                    items = items.OrderByDescending(i => i.Name);
            }

            return items;
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