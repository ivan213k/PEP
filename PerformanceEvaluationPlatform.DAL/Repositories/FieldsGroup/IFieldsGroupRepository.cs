using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dto;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dao;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FieldsGroup
{
    public interface IFieldsGroupRepository : IBaseRepository
    {
        Task<IList<FieldGroupListItemDto>> GetList(FieldGroupListFilterDto filter);
        Task<FieldGroup> Get(int id);
    }
}
