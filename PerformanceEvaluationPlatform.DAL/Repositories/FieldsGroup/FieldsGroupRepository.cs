using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dto;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dao;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FieldsGroup
{
    public class FieldsGroupRepository : BaseRepository, IFieldsGroupRepository
    {
        public FieldsGroupRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<FieldGroupListItemDto>> GetList(FieldGroupListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                TitleSetOrder = filter.TitleSetOrder,
                FieldCountSetOrder = filter.FieldCountSetOrder,
                Skip = filter.Skip,
                Take = filter.Take,
                CountFrom = filter.CountFrom,
                CountTo = filter.CountTo,
                IsNotEmptyOnly = filter.IsNotEmptyOnly
            };

            return ExecuteSp<FieldGroupListItemDto>("[dbo].[spGetFieldGroupListItems]", parameters);
        }

        public async Task<FieldGroupDetailsDto> GetDetails(int id)
        {
            var fieldGroup = await DbContext.Set<FieldGroup>()
                .Include(t => t.Fields)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (fieldGroup == null)
            {
                return null;
            }

            var details = new FieldGroupDetailsDto
            {
                Id = fieldGroup.Id,
                Title = fieldGroup.Title,
                FieldsNames = fieldGroup.Fields.Select(t => t.Name).ToList()
            };

            return details;
        }


        public Task<FieldGroup> Get(int id)
        {
            return Get<FieldGroup>(id);
        }
    }
}
