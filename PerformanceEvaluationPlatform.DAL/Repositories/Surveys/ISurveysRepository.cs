using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Surveys
{
    public interface ISurveysRepository
    {
        Task<IList<SurveyListItemDto>> GetList(SurveyListFilterDto filter);
        Task<IList<SurveyStateListItemDto>> GetStatesList();
        Task<SurveyDetailsDto> GetDetails(int id);
    }
}
