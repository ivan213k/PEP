using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates
{
    public interface IFormTemplatesRepository: IBaseRepository
    {
        Task<IList<FormTemplateListItemDto>> GetList(FormTemplateListFilterOrderDto filter);
        Task<IList<FormTemplateStatusListItemDto>> GetStatusListAsync();
        Task<FormTemplateDetailsDto> GetDetailsAsync(int id);
        Task<FormTemplate> Get(int id);
        Task<FormTemplateStatus> GetStatus(int id);
        Task Create(FormTemplate formTemplate);
        Task<bool> ExistByName(string name);
        Task<bool> ExistDraftFormTemplate(string name);
        Task<IList<FormTemplate>> GetActiveFormTemplate(string name);
        Task<int> MaxVersion(string name);
    }
}
