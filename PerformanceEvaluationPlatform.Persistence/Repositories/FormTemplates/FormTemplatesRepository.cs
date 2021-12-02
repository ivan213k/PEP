using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Application.Interfaces.FormTemplates;
using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Domain.FormTemplates;
using PerformanceEvaluationPlatform.Persistence.DatabaseContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Persistence.Repositories.FormTemplates
{
    public class FormTemplatesRepository : BaseRepository, IFormTemplatesRepository
    {
        private const int DraftStatusId = 1;
        private const int ActiveStatusId = 2;

        public FormTemplatesRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) : base(databaseOptions, dbContext)
        {
        }

        public Task<FormTemplate> Get(int id)
        {
            return DbContext.Set<FormTemplate>()
                .Include(t=>t.FormTemplateFieldMaps)
                .SingleOrDefaultAsync(t=>t.Id == id);
        }

        public async Task<FormTemplateDetailsDto> GetDetails(int id)
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
                FormTemplateStatusId = formTemplate.StatusId,
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

        public Task<ListItemsDto<FormTemplateListItemDto>> GetList(FormTemplateListFilterOrderDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                StatusIds = filter.StatusIds,
                NameSortOrder = filter.NameSortOrder,
                Skip = filter.Skip,
                Take = filter.Take
            };
            return ExecuteGetListItemsSp<FormTemplateListItemDto>("[dbo].[spGetFormTemplatesListItems]", parameters);
        }

        public async Task<IList<FormTemplateStatusListItemDto>> GetStatusList()
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

        public Task<bool> ExistDraftFormTemplate(string name)
        {
            return DbContext.Set<FormTemplate>()
                .Where(t => t.StatusId == DraftStatusId)
                .AnyAsync(t => t.Name == name);
        }

        public async Task<IList<FormTemplate>> GetActiveFormTemplate(string name)
        {
            return await DbContext.Set<FormTemplate>()
                .Where(t => t.StatusId == ActiveStatusId && t.Name == name)
                .ToListAsync();
        }

        public Task<int> MaxVersion(string name)
        {
            return DbContext.Set<FormTemplate>().Where(t => t.Name == name).MaxAsync(t => t.Version);
        }
    }
}
