using PerformanceEvaluationPlatform.Application.Interfaces.Roles;
using PerformanceEvaluationPlatform.Application.Model.Roles;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Application.Packages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Role
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<ServiceResponse<ListItemsDto<RoleListItemDto>>> GetListItems(RoleListFilterDto filter)
        {
            ListItemsDto<RoleListItemDto> items = await _rolesRepository.GetList(filter);
            return ServiceResponse<ListItemsDto<RoleListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<RoleDetailsDto>> GetDetails(int id)
        {
            var details = await _rolesRepository.GetDetails(id);
            return details == null
                ? ServiceResponse<RoleDetailsDto>.NotFound()
                : ServiceResponse<RoleDetailsDto>.Success(details);
        }

        public async Task<ServiceResponse> Update(int id, UpdateRoleDto model)
        {
            if (model is null)
            {
                return ServiceResponse.BadRequest();
            }

            var entity = await _rolesRepository.Get(id);
            if (entity == null)
            {
                return ServiceResponse.NotFound();
            }

            var isTittleNotUnique = await _rolesRepository.IsTittleNotUnique(model.Title, id);

            if (isTittleNotUnique)
            {
                return ServiceResponse.Conflict("Role with the same title is already exists");
            }

            entity.Title = model.Title;
            entity.IsPrimary = model.IsPrimary;


            await _rolesRepository.SaveChanges();
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<int>> Create(CreateRoleDto model)
        {
            if (model is null)
            {
                return ServiceResponse<int>.BadRequest();
            }

            var isTittleNotUnique = await _rolesRepository.IsTittleNotUnique(model.Title);

            if (isTittleNotUnique)
            {
                return ServiceResponse<int>.Conflict("Role with the same title is already exists");
            }

            var role = new Domain.Roles.Role
            {
                Title = model.Title,
                IsPrimary = model.IsPrimary
            };

            await _rolesRepository.Create(role);
            await _rolesRepository.SaveChanges();

            return ServiceResponse<int>.Success(role.Id, 201);
        }
    }
}
