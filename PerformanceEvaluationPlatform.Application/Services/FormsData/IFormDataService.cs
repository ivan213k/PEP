using PerformanceEvaluationPlatform.Application.Model.FormsData;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Application.Services.FormsData
{
    public interface IFormDataService
    {
        Task<ServiceResponse<IList<FormDataListItemDto>>> GetListItems(FormDataListFilterDto filter);
        Task<ServiceResponse<IList<FormDataStateListItemDto>>> GetStateListItems();
        Task<ServiceResponse<FormDataDetailsDto>> GetDetails(int id);
        Task<ServiceResponse> Update(int id, IList<UpdateFieldDataDto> model);
    }
}