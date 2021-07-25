using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Fields
{
    public interface IFieldsRepository : IBaseRepository
    {
        Task<IList<FieldListItemDto>> GetList(FieldListFilterDto filter);
        Task<IList<FieldTypeListItemDto>> GetTypesList();
        Task<FieldDetailsDto> GetDetails(int id);
        Task<Field> Get(int id);
        Task<FieldType> GetType(int id);
        Task<FieldAssesmentGroup> GetAssesmentGroup(int id);
        Task Create(Field field);
    }
}
