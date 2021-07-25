
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks
{
    public interface IDeeplinksRepository
    {
        Task<IList<DeeplinkListItemDto>> GetList(DeeplinkListFilterDto filter);
        
        Task<IList<DeeplinkStateListItemDto>> GetStatesList();
        Task<DeeplinkDetailsDto> GetDetails(int id);
    }
}
