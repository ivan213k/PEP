using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormsData      
{
    public interface IFormDataRepository: IBaseRepository
    {
        Task<IList<FormDataListItemDto>> GetList(FormDataListFilterDto filter);
        Task<IList<FormDataStateListItemDto>> GetStatesList();
        Task<FormDataDetailsDto> GetDetails(int id);
    }
}
