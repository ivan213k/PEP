using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Application.Interfaces.Roles;
using PerformanceEvaluationPlatform.Application.Model.Roles;
using PerformanceEvaluationPlatform.Domain.Roles;
using PerformanceEvaluationPlatform.Persistence.DatabaseContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Persistence.Repositories.Roles
{
    public class RolesRepository : BaseRepository, IRolesRepository
    {

        public RolesRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<RoleListItemDto>> GetList(RoleListFilterDto filter)
        {
            var parameters = new
            {
                Skip = filter.Skip,
                Take = filter.Take,
                Search = filter.Search,
                IsPrimary = filter.IsPrimary,
                UsersCountFrom = filter.UsersCountFrom,
                UsersCountTo = filter.UsersCountTo,
                TitleSortOrder = filter.TitleSortOrder,
                IsPrimarySortOrder = filter.IsPrimarySortOrder
            };

            return ExecuteSp<RoleListItemDto>("[dbo].[spGetRoleListItems]", parameters);
        }

        public async Task<RoleDetailsDto> GetDetails(int id)
        {
            var role = await DbContext.Set<Role>()
                .Include(r => r.UserRoleMaps)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (role == null)
            {
                return null;
            }

            var details = new RoleDetailsDto
            {
                Id = role.Id,
                Title = role.Title,
                IsPrimary = role.IsPrimary,
                UsersCount = role.UserRoleMaps.Count()
            };

            return details;
        }

        public Task<Role> Get(int id)
        {
            return Get<Role>(id);
        }

        public async Task<bool> IsTittleNotUnique(string title, int? id = null)
        {
            if (id == null)
            {
                return await DbContext.Set<Role>().AnyAsync(t => title.Equals(t.Title));
            }
            else
            {
                return await DbContext.Set<Role>().Where(t => t.Id != id).AnyAsync(t => title.Equals(t.Title));
            }
        }

        public Task Create(Role role)
        {
            return Create<Role>(role);
        }
    }
}
