using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Examples
{
    public interface IExamplesRepository
    {
        Task<IList<ExampleListItemDto>> GetList(ExampleListFilterDto filter);
        Task<IList<ExampleTypeListItemDto>> GetTypesList();
        Task<IList<ExampleStateListItemDto>> GetStatesList();
        Task<ExampleDetailsDto> GetDetails(int id);
    }
}
