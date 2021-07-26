using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates
{
    public interface IFormTemplatesRepository
    {
        Task<IList<FormTemplateListItemDto>> GetList(FormTemplateListFilterOrderDto filter);
        Task<IList<FormTemplateStatusListItemDto>> GetStatusListAsync();
        Task<FormTemplateDetailsDto> GetDetailsAsync(int id);
        Task<FormTemplate> Get(int id);
    }
}
