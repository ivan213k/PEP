using PerformanceEvaluationPlatform.Application.Model.Surveys;
using PerformanceEvaluationPlatform.Domain.Surveys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Surveys
{
    public interface ISurveysRepository : IBaseRepository
    {
        Task<IList<SurveyListItemDto>> GetList(SurveyListFilterDto filter);
        Task<IList<SurveyStateListItemDto>> GetStatesList();
        Task<SurveyDetailsDto> GetDetails(int id);
        Task<Survey> Get(int id);
        Task<Survey> GetSurveyWithDeeplinksAndFormData(int id);
        Task<SurveyState> GetState(int id);
        Task<Level> GetLevel(int id);
        Task Create(Survey survey);
        Task<bool> ExistsByFormTemplate(int formTemplateId);
    }
}
