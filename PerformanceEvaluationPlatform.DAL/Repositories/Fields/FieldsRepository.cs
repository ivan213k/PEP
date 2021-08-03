using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Fields
{
    public class FieldsRepository : BaseRepository, IFieldsRepository
    {
        public FieldsRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) 
            : base(databaseOptions, dbContext)
        {
        }

        public async Task<IList<FieldListItemDto>> GetList(FieldListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                AssesmentGroupIds = filter.AssesmentGroupIds,
                TypeIds = filter.TypeIds,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = filter.TitleSortOrder
            };

            return await ExecuteSp<FieldListItemDto>("[dbo].[spGetFieldListItems]", parameters);
        }
        public async Task<IList<FieldTypeListItemDto>> GetTypesList()
        {
            return await DbContext.Set<FieldType>()
                .Select(t => new FieldTypeListItemDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }
        public async Task<FieldDetailsDto> GetDetails(int id)
        {
            var field = await DbContext.Set<Field>()
                .Include(t => t.FieldType)
                .Include(t => t.AssesmentGroup)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (field == null)
            {
                return null;
            }

            var details = new FieldDetailsDto
            {
                Id = field.Id,
                Name = field.Name,
                AssasmentGroupName = field.AssesmentGroup.Name,
                TypeName = field.FieldType.Name,
                IsRequired = field.IsRequired,
                Description = field.Description
            };

            return details;
        }
        public Task<FieldAssesmentGroup> GetAssesmentGroup(int id)
        {
            return Get<FieldAssesmentGroup>(id);
        }

        public Task Create(Field field)
        {
            return Create<Field>(field);
        }

        public Task<FieldType> GetType(int id)
        {
            return Get<FieldType>(id);
        }

        public Task<Field> GetField(int id)
        {
            return Get<Field>(id);
        }
        public async Task<bool> GetAnyReferenceToFormTemplate(int id)
        {
            return await DbContext.Set<FormTemplateFieldMap>().AnyAsync(t => t.FieldId == id);
        }

        public async Task<List<Field>> GetListByIds(IEnumerable<int> fieldIds)
        {
            var fields = await DbContext.Set<Field>().Where(f => fieldIds.Contains(f.Id)).ToListAsync();
            return fields;
        }

        public async Task<IList<FieldAssesmentGroupListItemDto>> GetFieldAssesmentGroupList()
        {
            return await DbContext.Set<FieldAssesmentGroup>()
                 .Select(t => new FieldAssesmentGroupListItemDto
                 {
                     Id = t.Id,
                     Name = t.Name
                 })
                 .ToListAsync();
        }

        public void Delete(Field field)
        {
            Delete<Field>(field);
        }

        public Task<FieldData> GetFieldData(int id)
        {
            return Get<FieldData>(id);
        }
    }
}
