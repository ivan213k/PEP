using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Model.Fields;
using PerformanceEvaluationPlatform.Domain.Fields;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Fields
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
        Task<IList<Field>> GetListByIds(IEnumerable<int> fieldIds);
        void Delete(Field field);
        Task<bool> GetAnyReferenceToFormTemplate(int id);
        Task<FieldData> GetFieldData(int id);
    }
}
