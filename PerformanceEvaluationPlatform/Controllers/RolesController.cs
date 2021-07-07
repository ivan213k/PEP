using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        [Route("roles")]
        public IActionResult Get([FromQuery] RoleListFilterRequestModel filter)
        {
            var items = GetRoleListItemViewModels();
            items = GetFilteredItems(items, filter);

            return Ok(items);
        }

        private IEnumerable<RoleListItemViewModel> GetFilteredItems(IEnumerable<RoleListItemViewModel> items,
            RoleListFilterRequestModel filter)
        {
            InitFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Title.Contains(filter.Search));
            }

            if (filter.IsPrimary != null)
            {
                items = items
                    .Where(t => t.IsPrimary == filter.IsPrimary);
            }

            if (filter.UsersCountFrom != null)
            {
                items = items
                    .Where(t => t.UsersCount >= filter.UsersCountFrom);
            }

            if (filter.UsersCountTo != null)
            {
                items = items
                    .Where(t => t.UsersCount <= filter.UsersCountTo);
            }

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        private void InitFilter(RoleListFilterRequestModel filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }

            if (filter.Take == null)
            {
                filter.Take = 30;
            }
        }

        private static IEnumerable<RoleListItemViewModel> GetRoleListItemViewModels()
        {
            var items = new List<RoleListItemViewModel>
            {
                new RoleListItemViewModel
                {
                    Id = 1,
                    Title = "Backend",
                    IsPrimary = true,
                    UsersCount = 30
                },
                new RoleListItemViewModel
                {
                    Id = 2,
                    Title = "Frontend",
                    IsPrimary = false,
                    UsersCount = 15
                },
                new RoleListItemViewModel
                {
                    Id = 3,
                    Title = "QA",
                    IsPrimary = false,
                    UsersCount = 2
                }
            };
            return items;
        }
    }
}
