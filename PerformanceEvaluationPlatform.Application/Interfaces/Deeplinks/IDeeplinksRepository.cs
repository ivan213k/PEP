using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Domain.Deeplinks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Deeplinks
{
    public interface IDeeplinksRepository : IBaseRepository
    {
        Task<ListItemsDto<DeeplinkListItemDto>> GetList(DeeplinkListFilterDto filter);

        Task<IList<DeeplinkStateListItemDto>> GetStatesList();
        Task<DeeplinkDetailsDto> GetDetails(int id);
        Task<Deeplink> Get(int id);
        Task<DeeplinkState> GetState(int id);
        Task Create(Deeplink deeplink);
    }
}
