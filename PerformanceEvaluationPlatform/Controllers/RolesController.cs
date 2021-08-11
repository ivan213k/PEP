using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Roles;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;

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
        public async Task<IActionResult> Create([FromBody] CreateRoleRequestModel requestModel)
        {
            if (requestModel is null)
            {
                return BadRequest();
            }

            var isTittleNotUnique = await _rolesRepository.IsTittleNotUnique(requestModel.Title);

            if (isTittleNotUnique)
            {
                return Conflict("Role with the same title is already exists");
            }

            var role = new Role
            {
                Title = requestModel.Title,
                IsPrimary = requestModel.IsPrimary
            };

            await _rolesRepository.Create(role);
            await _rolesRepository.SaveChanges();

            var result = new ObjectResult(new { Id = role.Id }) { StatusCode = 201 };
            return result;
        }

        [Route("roles/{id:int}")]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] EditRoleRequestModel requestModel)
        {
            if (requestModel is null)
            {
                return BadRequest();
            }

            var entity = await _rolesRepository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            var isTittleNotUnique = await _rolesRepository.IsTittleNotUnique(requestModel.Title, id);

            if (isTittleNotUnique)
            {
                return Conflict("Role with the same title is already exists");
            }

            entity.Title = requestModel.Title;
            entity.IsPrimary = requestModel.IsPrimary;

            await _rolesRepository.SaveChanges();
            return Ok();
        }
    }
}
