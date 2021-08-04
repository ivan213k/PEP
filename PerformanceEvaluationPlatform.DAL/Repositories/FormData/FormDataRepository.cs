using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormsData
{
    public class FormDataRepository : BaseRepository, IFormDataRepository
    {
        public FormDataRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<FormDataListItemDto>> GetList(FormDataListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                StateId = filter.StateId,
                Skip = filter.Skip,
                Take = filter.Take,
                AssigneeSortOrder = filter.AssigneeSortOrder,
                FormNameSortOrder = filter.FormNameSortOrder,
                AssigneeIds = filter.AssigneeIds,
                ReviewersIds = filter.ReviewersIds,
                AppointmentDateFrom = filter.AppointmentDateFrom,
                AppointmentDateTo = filter.AppointmentDateTo
            };

            return ExecuteSp<FormDataListItemDto>("[dbo].[spGetFormDataListItems]", parameters);
        }

        public async Task<IList<FormDataStateListItemDto>> GetStatesList()
        {
            return await DbContext.Set<FormDataState>()
                .Select(t => new FormDataStateListItemDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<FormDataDetailsDto> GetDetails(int id)
        {
            var formData = await DbContext.Set<FormData>()
                .Include(fd => fd.FormDataState)
                .Include(fd => fd.Survey)
                    .ThenInclude(s => s.RecomendedLevel)
                .Include(fd => fd.Survey)
                    .ThenInclude(s => s.FormTemplate)
                    .ThenInclude(s => s.FormTemplateFieldMaps)
                .Include(fd => fd.User)
                    .ThenInclude(u => u.Team)
                    .ThenInclude(u => u.Project)
                .Include(fd => fd.User)
                    .ThenInclude(u => u.TechnicalLevel)
                .Include(fd => fd.User)
                    .ThenInclude(u => u.EnglishLevel)
                .Include(fd => fd.FieldData)
                    .ThenInclude(fd => fd.Assesment)
                .Include(fd => fd.FieldData)
                    .ThenInclude(fd => fd.Field)
                    .ThenInclude(fd => fd.FieldType)
                .SingleOrDefaultAsync(fd => fd.Id == id);

            if (formData is null) return null;

            var details = new FormDataDetailsDto
            {
                FormName = formData.Survey.FormTemplate.Name,
                FormId = formData.Survey.FormTemplateId,
                Assignee = GetFormatedName(formData),
                AssigneeId = formData.User.Id,
                Reviewer = GetFormatedName(formData),
                ReviewerId = formData.User.Id,
                State = formData.FormDataState.Name,
                FormDataStateId = formData.FormDataStateId,
                AppointmentDate = DateTime.Now,
                RecommendedLevel = formData.Survey.RecomendedLevel.Name,
                RecommendedLevelId = formData.Survey.RecommendedLevelId,
                Project = formData.User.Team.Project.Title,
                ProjectId = formData.User.Team.Project.Id,
                Team = formData.User.Team.Title,
                TeamId = formData.User.Team.Id,
                // Period =,
                ExperienceInCompany = Math.Abs((int)(DateTime.Now - formData.User.FirstDayInCompany).TotalDays) / 365,
                EnglishLevel = formData.User.EnglishLevel.Name,
                CurrentPosition = formData.User.TechnicalLevel.Name,

                Answers = formData.FieldData?.Select(fd => new FormDataAnswersItemDto
                {
                    Assessment = fd.Assesment.Name,
                    Comment = fd.Comment,
                    TypeId = fd.Field.FieldType.Id,
                    TypeName = fd.Field.FieldType.Name,
                    Order =  fd.Order,
                })
                .OrderBy(fd => fd.Order)
                .ToList()
            };
            return details;
        }
        private static string GetFormatedName(FormData formData)
        {
            return $"{formData.User.FirstName} {formData.User.LastName}";
        }

        public Task<FormData> Get(int id)
        {
            return DbContext.Set<FormData>().Include(t => t.FieldData).SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
