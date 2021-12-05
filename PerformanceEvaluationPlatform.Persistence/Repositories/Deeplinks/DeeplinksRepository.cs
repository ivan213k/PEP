using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Persistence.DatabaseContext;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using PerformanceEvaluationPlatform.Application.Interfaces.Deeplinks;
using PerformanceEvaluationPlatform.Domain.Deeplinks;
using PerformanceEvaluationPlatform.Application.Model.Shared;

namespace PerformanceEvaluationPlatform.Persistence.Repositories.Deeplinks
{
    public class DeeplinksRepository : BaseRepository, IDeeplinksRepository
    {

        public DeeplinksRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }

        public Task<ListItemsDto<DeeplinkListItemDto>> GetList(DeeplinkListFilterDto filter)
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

            return ExecuteGetListItemsSp<DeeplinkListItemDto>("[dbo].[spGetDeeplinkListItems]", parameters);
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
                .Include(t => t.SentBy)                                  
                .Include(t => t.User)
                .Include(t => t.Survey).ThenInclude(t => t.FormTemplate)
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
                FormTemplateName = deeplink.Survey.FormTemplate.Name,
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
