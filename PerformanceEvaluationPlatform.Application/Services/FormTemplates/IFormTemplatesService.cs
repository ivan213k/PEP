using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Interfaces;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Application.Packages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.FormTemplates
{
    public interface IFormTemplatesService
    {
        Task<ServiceResponse<ListItemsDto<FormTemplateListItemDto>>> GetListItems(FormTemplateListFilterOrderDto filter);
        Task<ServiceResponse<IList<FormTemplateStatusListItemDto>>> GetStatusListItems();
        Task<ServiceResponse<FormTemplateDetailsDto>> GetDetails(int id);
        Task<ServiceResponse<int>> Create(CreateFormTemplateDto model);
        Task<ServiceResponse> ChangeStatusToActive(int id);
        Task<Dictionary<string, ICollection<string>>> ValidateFormTemplate(IFormTemplateRequest request);
    }
}
