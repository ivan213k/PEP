using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Model.FormsData;
using PerformanceEvaluationPlatform.Domain.FormsData;

namespace PerformanceEvaluationPlatform.Application.Interfaces.FormsData
{
    public interface IFormDataRepository: IBaseRepository
    {
        Task<IList<FormDataListItemDto>> GetList(FormDataListFilterDto filter);
        Task<IList<FormDataStateListItemDto>> GetStatesList();
        Task<FormDataDetailsDto> GetDetails(int id);
        Task<FormData> Get(int id);
    }
}
