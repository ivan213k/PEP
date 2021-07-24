using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Roles;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository ?? throw new ArgumentNullException(nameof(rolesRepository));
        }

        [Route("roles")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RoleListFilterRequestModel filter)
        {
            var filterDto = new RoleListFilterDto
            {
                Skip = filter.Skip,
                Take = filter.Take,
                Search = filter.Search,
                IsPrimary = filter.IsPrimary,
                UsersCountFrom =filter.UsersCountFrom,
                UsersCountTo = filter.UsersCountTo,
                TitleSortOrder = (int?)filter.TitleSortOrder,
                IsPrimarySortOrder = (int?)filter.IsPrimarySortOrder
            };

            var itemsDto = await _rolesRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new RoleListItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                IsPrimary = t.IsPrimary,
                UsersCount = t.UsersCount
            });

            return Ok(items);
        }

        [Route("roles/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetRoleDetails([FromRoute] int id)
        {
            var detailsDto = await _rolesRepository.GetDetails(id);
            if (detailsDto == null)
            {
                return NotFound();
            }

            var detailsVm = new RoleDetailsViewModel
            {
                Id = detailsDto.Id,
                Title = detailsDto.Title,
                IsPrimary = detailsDto.IsPrimary,
                UsersCount = detailsDto.UsersCount
            };

            return Ok(detailsVm);
        }

        [Route("roles")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateRoleRequestModel role)
        {
            var items = GetRoleListItemViewModels();

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

        [Route("roles/{id}")]
        [HttpPut]
        public IActionResult EditUser(int id, [FromBody] EditRoleRequestModel role)
        {
            var items = GetRoleListItemViewModels();

            if (role is null)
            {
                return BadRequest();
            }

            var currentRole = items.FirstOrDefault(t => t.Id == role.Id);
            if (currentRole is null)
            {
                return NotFound();
            }

            currentRole.Title = role.Title;
            currentRole.IsPrimary = role.IsPrimary;

            items = items.Where(t => t.Id != currentRole.Id).Append(currentRole);

            return Ok();
        }

        private IEnumerable<RoleListItemViewModel> GetFilteredItems(IEnumerable<RoleListItemViewModel> items,
            RoleListFilterRequestModel filter)
        {
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
            if (filter.TitleSortOrder != null)
            {
                if (filter.TitleSortOrder == SortOrder.Ascending)
                    items = items.OrderBy(t => t.Title);
                else
                    items = items.OrderByDescending(t => t.Title);
            }
            if (filter.IsPrimarySortOrder != null)
            {
                if (filter.IsPrimarySortOrder == SortOrder.Ascending)
                    items = items.OrderBy(t => t.IsPrimary);
                else
                    items = items.OrderByDescending(t => t.IsPrimary);
            }

            return items;
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
