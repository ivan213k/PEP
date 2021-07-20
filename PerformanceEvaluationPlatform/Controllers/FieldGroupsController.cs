using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FieldGroups.ViewModels;
using PerformanceEvaluationPlatform.Models.FieldGroups.RequestModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldGroupsController : ControllerBase
    {
        [HttpGet("fields/groups")]
        public IActionResult Get([FromQuery]FieldGroupsListFilterRequestModel filter)
        {
            var items = GetFieldGroupsListItemViewModel();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        private IEnumerable<FieldGroupsListItemViewModel> GetFilteredItems (IEnumerable<FieldGroupsListItemViewModel> items,
            FieldGroupsListFilterRequestModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Title.Contains(filter.Search));
            }

            if (filter.CountFrom != null)
            {
                items = items
                    .Where(t => t.FieldCount >= filter.CountFrom);
            }

            if (filter.CountTo != null)
            {
                items = items
                    .Where(t => t.FieldCount <= filter.CountTo);
            }

            if (filter.IsNotEmptyOnly)
            {
                items = items
                    .Where(t => t.FieldCount > 0);
            };

            if (filter.TitleSetOrder != null)
            {
                if (filter.TitleSetOrder == SortOrder.Ascending)
                {
                    items = items
                        .OrderBy(t => t.Title);
                }
                else if (filter.TitleSetOrder == SortOrder.Descending)
                {
                    items = items
                        .OrderByDescending(t => t.Title);
                }
            }
            else
            {
                items = items
                    .OrderBy(t => t.Title);
            }

            if(filter.FieldCountSetOrder != null)
            {
                if (filter.FieldCountSetOrder == SortOrder.Ascending)
                {
                    items = items
                        .OrderBy(t => t.FieldCount);
                }
                else if (filter.FieldCountSetOrder == SortOrder.Descending)
                {
                    items = items
                        .OrderByDescending(t => t.FieldCount);
                }
            }

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        private IEnumerable<FieldGroupsListItemViewModel> GetFieldGroupsListItemViewModel()
        {
            var items = new List<FieldGroupsListItemViewModel>
            {
                new FieldGroupsListItemViewModel
                {
                    Title = "Soft skills",
                    FieldCount = 5
                },
                new FieldGroupsListItemViewModel
                {
                    Title = "Comunication skills",
                    FieldCount = 6
                },
                new FieldGroupsListItemViewModel
                {
                    Title = "Proficiency with programming languages",
                    FieldCount = 3
                },
                new FieldGroupsListItemViewModel
                {
                    Title = "Writing skills",
                    FieldCount = 0
                }
            };

            return items;
        }
    }
}
