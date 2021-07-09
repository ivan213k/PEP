using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldsController : ControllerBase
    {

        [Route("fields")]
        public IActionResult Get([FromQuery] FieldListFilterRequestModel filter)
        {
            var items = GetFieldListItemViewModels();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        [Route("fields/types")]
        public IActionResult GetTypes()
        {
            var items = new List<FieldTypeListItemViewModel>
            {
                new FieldTypeListItemViewModel
                {
                    Id = 1,
                    Name = "Divider"
                },
                new FieldTypeListItemViewModel
                {
                    Id = 2,
                    Name = "Dropdown with comment"
                }
            };

            return Ok(items);
        }

        private IEnumerable<FieldListItemViewModel> GetFilteredItems(IEnumerable<FieldListItemViewModel> items,
            FieldListFilterRequestModel filter)
        {
            InitFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Name.Contains(filter.Search));
            }

            if (filter.AssesmentGroupIds != null)
            {
                items = items
                    .Where(t => filter.AssesmentGroupIds.Contains(t.AssesmentGroupId));
            }

            if (filter.TypeIds != null)
            {
                items = items
                    .Where(t => filter.TypeIds.Contains(t.TypeId));
            }

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        private void InitFilter(FieldListFilterRequestModel filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }

            if (filter.Take == null)
            {
                filter.Take = 5;
            }
        }
        private static IEnumerable<FieldListItemViewModel> GetFieldListItemViewModels()
        {
            var items = new List<FieldListItemViewModel>
            {
                new FieldListItemViewModel
                {
                    Name = "Communication skills",
                    Type = "Dropdown with comment",
                    TypeId = 2,
                    AssesmentGroupName = "A-F Group",
                    AssesmentGroupId = 2,
                    IsRequired = true 
                },
                new FieldListItemViewModel
                {
                    Name = "Written communication",
                    Type = "Dropdown with comment",
                    TypeId = 2,
                    AssesmentGroupName = "A-F Group",
                    AssesmentGroupId = 2,
                    IsRequired = true
                },
                new FieldListItemViewModel
                {
                    Name = "Full-bleed divider",
                    Type = "Divider",
                    TypeId = 1,
                    AssesmentGroupName = "NoAssesment",
                    AssesmentGroupId = 1,
                    IsRequired = false
                }
            };
            return items;
        }
    }
}
