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
        Task<FieldData> GetFieldData(int id);
        Task<Field> GetField(int id);
        Task<Assesment> GetAssessment(int id);
        Task<FormDataState> GetState(int id);
        Task<FieldData> GetComment(string comment);

    }
}
