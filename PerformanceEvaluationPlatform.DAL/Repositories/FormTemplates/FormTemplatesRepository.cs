using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates
{
    public class FormTemplatesRepository : BaseRepository, IFormTemplatesRepository
    {
        public FormTemplatesRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) : base(databaseOptions, dbContext)
        {
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
    }
}
