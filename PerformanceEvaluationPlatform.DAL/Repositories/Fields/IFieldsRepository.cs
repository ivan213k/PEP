using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Fields
{
    public interface IFieldsRepository
    {
        Task<IList<FieldListItemDto>> GetList(FieldListFilterDto filter);
        Task<IList<FieldTypeListItemDto>> GetTypesList();
        Task<FieldDetailsDto> GetDetails(int id);
    }
}
