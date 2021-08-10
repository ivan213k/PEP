using PerformanceEvaluationPlatform.Application.Model.Examples;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Application.Services.Example
{
    public interface IExamplesService
    {
        Task<ServiceResponse<IList<ExampleListItemDto>>> GetListItems(ExampleListFilterDto filter);
        Task<ServiceResponse<IList<ExampleTypeListItemDto>>> GetTypeListItems();
        Task<ServiceResponse<IList<ExampleStateListItemDto>>> GetStateListItems();
        Task<ServiceResponse<ExampleDetailsDto>> GetDetails(int id);

        Task<ServiceResponse> Update(int id, UpdateExampleDto model);
        Task<ServiceResponse<int>> Create(CreateExampleDto model);
    }
}
