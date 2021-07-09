using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private static IEnumerable<RoleListItemViewModel> items = GetRoleListItemViewModels();

        [Route("roles")]
        public IActionResult Get([FromQuery] RoleListFilterRequestModel filter)
        {
            items = GetFilteredItems(items, filter);
            items = SortItems(items, filter);

            return Ok(items);
        }

        [HttpPost("roles")]
        public IActionResult Create([FromBody] CreateRoleRequestModel role)
        {
            if (role == null)
            {
                return BadRequest();
            }

            bool roleAlreadyExists = items.Any(t => t.Title.Trim().ToLower() == role.Title.ToLower().Trim()
                || t.Id == role.Id);

            if (roleAlreadyExists)
            {
                ModelState.AddModelError("", "This Role already exists");
                return Conflict(ModelState);
            }

            var newRole = new RoleListItemViewModel
            {
                Id = role.Id,
                Title = role.Title,
                IsPrimary = role.IsPrimary,
                UsersCount = role.UsersCount
            };

            items = items.Append(newRole);

            return Ok(newRole);
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

        private IEnumerable<RoleListItemViewModel> SortItems(IEnumerable<RoleListItemViewModel> items, RoleListFilterRequestModel filter)
        {
            if (filter.TitleSortOrder != SortOrder.Undefined)
            {
                if (filter.TitleSortOrder == SortOrder.Ascending)
                    items = items.OrderBy(t => t.Title);
                else
                    items = items.OrderByDescending(t => t.Title);
            }
            if (filter.IsPrimarySortOrder != SortOrder.Undefined)
            {
                if (filter.IsPrimarySortOrder == SortOrder.Ascending)
                    items = items.OrderBy(t => t.IsPrimary);
                else
                    items = items.OrderByDescending(t => t.IsPrimary);
            }

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
                filter.Take = 10;
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
                    IsPrimary = true,
                    UsersCount = 15
                },
                new RoleListItemViewModel
                {
                    Id = 3,
                    Title = "QA",
                    IsPrimary = true,
                    UsersCount = 2
                },
                new RoleListItemViewModel
                {
                    Id = 4 ,
                    Title = "Team Lead",
                    IsPrimary = false,
                    UsersCount = 1
                }
            };
            return items;
        }
    }
}
