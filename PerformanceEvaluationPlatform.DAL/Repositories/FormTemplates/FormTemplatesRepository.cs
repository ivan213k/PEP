using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates
{
    public class FormTemplatesRepository : BaseRepository, IFormTemplatesRepository
    {
        public FormTemplatesRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) : base(databaseOptions, dbContext)
        {
        }

        public Task<FormTemplate> Get(int id)
        {
            return DbContext.Set<FormTemplate>().Include(t=>t.FormTemplateFieldMaps).SingleOrDefaultAsync(t=>t.Id == id);
        }

        public async Task<FormTemplateDetailsDto> GetDetailsAsync(int id)
        {
            var formTemplate = await DbContext.Set<FormTemplate>()
                .Include(t => t.FormTemplateStatus)
                .Include(t => t.FormTemplateFieldMaps)
                .ThenInclude(t=>t.Field)
                .ThenInclude(t=>t.FieldType)
                .SingleOrDefaultAsync(t => t.Id == id);

            if (formTemplate is null) return null;

            var formTemplateDto = new FormTemplateDetailsDto
            {
                Id = formTemplate.Id,
                Name = formTemplate.Name,
                Version = formTemplate.Version,
                CreatedAt = formTemplate.CreatedAt,
                StatusId = formTemplate.StatusId,
                Status = formTemplate.FormTemplateStatus.Name,
                Fields = formTemplate.FormTemplateFieldMaps?
                .Select(t => new FormTemplateFieldDto
                {
                    Id = t.FieldId,
                    Name = t.Field.Name,
                    Order = t.Order,
                    FieldTypeId = t.Field.FieldTypeId,
                    FieldTypeName = t.Field.FieldType.Name
                })
                .OrderBy(t => t.Order)
                .ToList()
            };

            return formTemplateDto;

        }

        public Task<IList<FormTemplateListItemDto>> GetList(FormTemplateListFilterOrderDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                StatusIds = filter.StatusIds,
                NameSortOrder = filter.NameSortOrder,
                Skip = filter.Skip,
                Take = filter.Take
            };
            return ExecuteSp<FormTemplateListItemDto>("[dbo].[spGetFormTemplatesListItems]", parameters);
        }

        public async Task<IList<FormTemplateStatusListItemDto>> GetStatusListAsync()
        {
            return await DbContext.Set<FormTemplateStatus>()
                .Select(s => new FormTemplateStatusListItemDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }

        public Task<FormTemplateStatus> GetStatus(int id)
        {
            return Get<FormTemplateStatus>(id); 
        }

        public Task Create(FormTemplate formTemplate)
        {
            return Create<FormTemplate>(formTemplate); 
        }

        public Task<bool> ExistByName(string name)
        {
            return DbContext.Set<FormTemplate>().AnyAsync(t => t.Name == name);
        }
    }
}
