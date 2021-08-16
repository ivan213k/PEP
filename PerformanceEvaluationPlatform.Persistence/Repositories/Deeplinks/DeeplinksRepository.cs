using System;                                           // wait survey and user 
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Persistence.Repositories;
using PerformanceEvaluationPlatform.Persistence;
using PerformanceEvaluationPlatform.Persistence.DatabaseContext;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using PerformanceEvaluationPlatform.Application.Interfaces.Deeplinks;
using PerformanceEvaluationPlatform.Domain.Deeplinks;

namespace PerformanceEvaluationPlatform.Persistence.Repositories.Deeplinks
{
    public class DeeplinksRepository : BaseRepository, IDeeplinksRepository
    {

        public DeeplinksRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<DeeplinkListItemDto>> GetList(DeeplinkListFilterDto filter)
        {
            var parameters = new
            {
                filter.Search,
                filter.SentToId,
                filter.StateIds,
                filter.ExpiresAtFrom,
                filter.ExpiresAtTo,
                filter.SentToOrder,
                filter.ExpiresAtOrder,
                filter.Skip,
                filter.Take


            };

            return ExecuteSp<DeeplinkListItemDto>("[dbo].[spGetDeeplinkListItems]", parameters);
        }


        public async Task<IList<DeeplinkStateListItemDto>> GetStatesList()
        {
            return await DbContext.Set<DeeplinkState>()
                .Select(t => new DeeplinkStateListItemDto
                {
                    Id = t.Id,
                    Title = t.Name
                })
                .ToListAsync();
        }

        public async Task<DeeplinkDetailsDto> GetDetails(int id)
        {
            var deeplink = await DbContext.Set<Deeplink>()
                .Include(t => t.DeeplinkState)
                //.Include(t=>t.SentBy)                                  /// wait User And Survey
                // .Include(t=>t.User)
                //  .Include(t=>t.Survey).ThenInclude(t=>t.FormTemplate)
                .SingleOrDefaultAsync(t => t.Id == id);


            if (deeplink == null)
            {
                return null;
            }

            var details = new DeeplinkDetailsDto
            {
                Id = deeplink.Id,
                StateName = deeplink.DeeplinkState.Name,
                SentAt = deeplink.SentAt.GetValueOrDefault(),
                ExpiresAt = deeplink.ExpireDate,
                SurveyId = deeplink.SurveyId,
                /* FormTemplateName = deeplink.Survey.FormTemplate.Name,
                 SentTo = new DeeplinkUserRefDto
                     {
                         Id = deeplink.UserId,
                         FirstName = deeplink.User.FirstName,
                         LastName = deeplink.User.LastName,
                         Email = deeplink.User.Email,
                     },
                 SentBy = new DeeplinkUserRefDto
                     {
                         Id = deeplink.SentBy.Id,
                         FirstName = deeplink.SentBy.FirstName,
                         LastName = deeplink.SentBy.LastName,
                         Email = deeplink.SentBy.Email
                     }
                */
            };

            return details;

        }

        public Task<DeeplinkState> GetState(int id)
        {
            return Get<DeeplinkState>(id);
        }

        public Task Create(Deeplink deeplink)
        {
            return Create<Deeplink>(deeplink);
        }


        public Task<Deeplink> Get(int id)
        {
            return Get<Deeplink>(id);
        }

    }
}
