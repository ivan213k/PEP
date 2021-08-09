using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Fields
{
    public interface IFieldsRepository : IBaseRepository
    {
        Task<IList<FieldListItemDto>> GetList(FieldListFilterDto filter);
        Task<IList<FieldTypeListItemDto>> GetTypesList();
        Task<IList<FieldAssesmentGroupListItemDto>> GetFieldAssesmentGroupList();
        Task<FieldDetailsDto> GetDetails(int id);
        Task<Field> Get(int id);
        Task<FieldType> GetType(int id);
        Task<FieldAssesmentGroup> GetAssesmentGroup(int id);
        Task Create(Field field);
        Task<List<Field>> GetListByIds(IEnumerable<int> fieldIds);
        void Delete(Field field);
        Task<bool> GetAnyReferenceToFormTemplate(int id);
        Task<FieldData> GetFieldData(int id);
    }
}
