using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Deeplinks;

namespace PerformanceEvaluationPlatform.Application.Services.Deeplinks
{
    public interface IDeeplinksService 
    {
        Task<ServiceResponse<ListItemsDto<DeeplinkListItemDto>>> GetList(DeeplinkListFilterDto filter);
        Task<ServiceResponse<IList<DeeplinkStateListItemDto>>> GetStatesList();
        Task<ServiceResponse<DeeplinkDetailsDto>> GetDetails(int id);
        Task<ServiceResponse> Update(int id, UpdateDeeplinkDto model);
        Task<ServiceResponse<int>> Create(CreateDeeplinkDto model);
    }
}
