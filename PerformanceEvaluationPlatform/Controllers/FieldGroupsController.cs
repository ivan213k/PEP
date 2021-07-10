using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FieldGroups.ViewModels;
using PerformanceEvaluationPlatform.Models.FieldGroups.RequestModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldGroupsController : ControllerBase
    {
        [Route("fields/groups")]
        public IActionResult Get([FromQuery]FieldGroupsListFilterRequestModel filter)
        {
            var items = GetFieldGroupsListItemViewModel();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        private IEnumerable<FieldGroupsListItemViewModel> GetFilteredItems (IEnumerable<FieldGroupsListItemViewModel> items, 
            FieldGroupsListFilterRequestModel filter)
        {
            InitFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Title.Contains(filter.Search));
            }

            if(filter.CountFrom != null)
            {
                items = items
                    .Where(t => t.FieldCount >= filter.CountFrom);
            }
            
            if(filter.CountTo != null)
            {
                items = items
                    .Where(t => t.FieldCount <= filter.CountTo);
            }

            if (filter.IsNotEmptyOnly)
            {
                items = items
                    .Where(t => t.FieldCount > 0);
            }

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        private void InitFilter (FieldGroupsListFilterRequestModel filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }

            if (filter.Take == null)
            {
                filter.Take = 20;
            }
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
