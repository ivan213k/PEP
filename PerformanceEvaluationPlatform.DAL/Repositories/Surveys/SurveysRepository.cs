using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Surveys
{
    public class SurveysRepository : BaseRepository, ISurveysRepository
    {
        public SurveysRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {

        }

        public async Task<IList<SurveyListItemDto>> GetList(SurveyListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                StateIds = filter.StateIds,
                AssigneeIds = filter.AssigneeIds,
                SupervisorIds = filter.SupervisorIds,
                AppointmentDateFrom = filter.AppointmentDateFrom,
                AppointmentDateTo = filter.AppointmentDateTo,
                FormNameSortOrder = filter.FormNameSortOrder,
                AssigneeSortOrder = filter.AssigneeSortOrder,
                Skip = filter.Skip,
                Take = filter.Take,
            };
            return await ExecuteSp<SurveyListItemDto>("[dbo].spGetSurveyListItems", parameters);
        }

        public async Task<IList<SurveyStateListItemDto>> GetStatesList()
        {
            return await DbContext.Set<SurveyState>()
               .Select(t => new SurveyStateListItemDto
               {
                   Id = t.Id,
                   Name = t.Name
               })
               .ToListAsync();
        }

        public async Task<SurveyDetailsDto> GetDetails(int id)
        {
            var survey = await DbContext.Set<Survey>()
                .Include(r => r.SurveyState)
                .Include(r => r.RecomendedLevel)
                .Include(r => r.FormTemplate)
                .Include(r => r.Asignee)
                .Include(r => r.Supervisor)
                .Include(r => r.DeepLinks).ThenInclude(t => t.User)
                .Include(r => r.FormData).ThenInclude(f => f.FormDataState)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (survey is null)
            {
                return null;
            }

            var details = new SurveyDetailsDto
            {
                AppointmentDate = survey.AppointmentDate,
                State = survey.SurveyState.Name,
                StateId = survey.StateId,
                RecommendedLevel = survey.RecomendedLevel.Name,
                RecommendedLevelId = survey.RecommendedLevelId,
                Summary = survey.Summary,
                Assignee = $"{survey.Asignee.FirstName} {survey.Asignee.LastName}",
                AssigneeId = survey.AssigneeId,
                Supervisor = $"{survey.Supervisor.FirstName} {survey.Supervisor.LastName}",
                SupervisorId = survey.SupervisorId,
                FormName = survey.FormTemplate.Name,
                FormId = survey.FormTemplateId,
                AssignedUsers = survey.DeepLinks.Select(d => new SurveyAssigneeDto
                {
                    Id = d.UserId,
                    Name = $"{d.User.FirstName} {d.User.LastName}"
                }).ToList(),
                FormData = survey.FormData.Select(fd => new SurveyFormDataDto
                {
                    AssignedUserId = fd.UserId,
                    StateId = fd.FormDataStateId
                }).ToList()
            };
            return details;
        }

        public Task<Survey> Get(int id)
        {
            return Get<Survey>(id);
        }

        public Task<SurveyState> GetState(int id)
        {
            return Get<SurveyState>(id);
        }

        public Task<Level> GetLevel(int id)
        {
            return Get<Level>(id);
        }

        public Task Create(Survey survey)
        {
            return Create<Survey>(survey);
        }
    }
}
