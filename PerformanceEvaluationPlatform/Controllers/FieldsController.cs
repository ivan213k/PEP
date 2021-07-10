using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private static IEnumerable<FieldListItemViewModel> items = GetFieldListItemViewModels();

        [HttpPost("fields")]
        public IActionResult Create([FromBody] CreateFieldRequestModel field)
        {
            if (field == null)
            {
                return BadRequest();
            }

            bool fieldAlreadyExists = items.Any(t => t.Name.Trim().ToLower() == field.Name.ToLower().Trim());

            if (fieldAlreadyExists)
            {
                ModelState.AddModelError("", "This field already exists");
                return Conflict(ModelState);
            }

            var newField = new FieldListItemViewModel
            {
                //in FieldListItemViewModel we dont use "Id", "FieldGroupName", "FieldGroupId", "Description"
                Name = field.Name,
                Type = field.Type,
                TypeId = field.TypeId,
                AssesmentGroupName = field.AssesmentGroupName,
                AssesmentGroupId = field.AssesmentGroupId,
                IsRequired = field.IsRequired
            };

            items = items.Append(newField);

            return Ok(newField);
        }

        [HttpGet("fields")]
        public IActionResult Get([FromQuery] FieldListFilterRequestModel filter)
        {      
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        [HttpGet("fields/groups")]
        public IActionResult GetFieldGroups()
        {
            var items = new List<FieldGroupListItemViewModel>
            {
                new FieldGroupListItemViewModel
                {
                    Id = 1,
                    Name = "Field group 1"
                },
                new FieldGroupListItemViewModel
                {
                    Id = 2,
                    Name = "Field group 2"
                }
            };

            return Ok(items);
        }
        [HttpGet("fields/types")]
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

        private IEnumerable<FieldListItemViewModel> GetOrderedItems(IEnumerable<FieldListItemViewModel> items,
            FieldListFilterRequestModel filter)
        {
            if (filter.FieldNameSortOrder != null)
            {
                if (filter.FieldNameSortOrder == SortOrder.Ascending)
                    items = items.OrderBy(t => t.Name);
                else if (filter.FieldNameSortOrder == SortOrder.Descending)
                    items = items.OrderByDescending(t => t.Name);
            }
            else items = items.OrderBy(t => t.Name);

            return items;
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

            items = GetOrderedItems(items, filter);

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
