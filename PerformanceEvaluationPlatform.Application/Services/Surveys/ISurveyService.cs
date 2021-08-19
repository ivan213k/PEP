using PerformanceEvaluationPlatform.Application.Model.Surveys;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Surveys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Surveys
{
    public interface ISurveyService
    {
        Task<ServiceResponse<IList<SurveyListItemDto>>> GetListItems(SurveyListFilterDto filter);
        Task<ServiceResponse<IList<SurveyStateListItemDto>>> GetStateListItems();
        Task<ServiceResponse<SurveyDetailsDto>> GetDetails(int id);

        Task<ServiceResponse> Update(int id, UpdateSurveyDto model);
        Task<ServiceResponse<int>> Create(CreateSurveyDto model);

        Task<ServiceResponse> ChangeStateToReady(int id);
        Task<ServiceResponse> ChangeStateToSent(int id);
        Task<ServiceResponse> ChangeStateToReadyForReview(int id);
        Task<ServiceResponse> ChangeStateToArchived(int id);
    }
}
