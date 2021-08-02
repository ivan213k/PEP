
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks
{
    public interface IDeeplinksRepository : IBaseRepository
    {
        Task<IList<DeeplinkListItemDto>> GetList(DeeplinkListFilterDto filter);
        
        Task<IList<DeeplinkStateListItemDto>> GetStatesList();
        Task<DeeplinkDetailsDto> GetDetails(int id);
        Task<Deeplink> Get(int id);
        Task<DeeplinkState> GetState(int id);
        Task Create(Deeplink deeplink);
    }
}
