using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using PerformanceEvaluationPlatform.Domain.FormTemplates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.FormTemplates
{
    public interface IFormTemplatesRepository: IBaseRepository
    {
        Task<IList<FormTemplateListItemDto>> GetList(FormTemplateListFilterOrderDto filter);
        Task<IList<FormTemplateStatusListItemDto>> GetStatusList();
        Task<FormTemplateDetailsDto> GetDetails(int id);
        Task<FormTemplate> Get(int id);
        Task<FormTemplateStatus> GetStatus(int id);
        Task Create(FormTemplate formTemplate);
        Task<bool> ExistByName(string name);
        Task<bool> ExistDraftFormTemplate(string name);
        Task<IList<FormTemplate>> GetActiveFormTemplate(string name);
        Task<int> MaxVersion(string name);
    }
}
