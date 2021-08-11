using PerformanceEvaluationPlatform.Application.Model.Fields;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Application.Services.Field
{
    public interface IFieldService
    {
        Task<ServiceResponse<IList<FieldListItemDto>>> GetListItems(FieldListFilterDto filter);
        Task<ServiceResponse<IList<FieldTypeListItemDto>>> GetTypeListItems();
        Task<ServiceResponse<FieldDetailsDto>> GetDetails(int id);

        Task<ServiceResponse> Update(int id, EditFieldDto model);
        Task<ServiceResponse<int>> Create(CreateFieldDto model);
        Task<ServiceResponse<int>> Copy(int id);
        Task<ServiceResponse> Delete(int id);
    }
}
