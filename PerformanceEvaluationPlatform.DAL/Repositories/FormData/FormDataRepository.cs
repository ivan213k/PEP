using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormData
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
    }
}
