using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks
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
                Search = filter.Search,
                SentToId = filter.SentToId,
                StateIds = filter.StateIds,
                ExpiresAtFrom = filter.ExpiresAtFrom,
                ExpiresAtTo = filter.ExpiresAtTo,
                SentToOrder = filter.SentToOrder,
                ExpiresAtOrder = filter.ExpiresAtOrder,
                Skip = filter.Skip,
                Take = filter.Take


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
                .Include(t=>t.SentBy)
                .Include(t=>t.User)
                .Include(t=>t.Survey).ThenInclude(t=>t.FormTemplate)
                .SingleOrDefaultAsync(t => t.Id == id);
                

            if (deeplink == null)
            {
                return null;
            }

            var details = new DeeplinkDetailsDto
            {
                Id = deeplink.Id,
               // SentToId = deeplink.UserId,
                SentAt = deeplink.SentAt,
               // SentById = deeplink.SentById,
                StateName = deeplink.DeeplinkState.Name,
                ExpiresAt = deeplink.ExpireDate,
                SurveyId = deeplink.SurveyId,
              //  SentToEmail = deeplink.User.Email,
               // SentToFirstName = deeplink.User.FirstName,
               // SentToLastName = deeplink.User.LastName,
               // SentByFirstName = deeplink.SentBy.FirstName,
               // SentByLastName = deeplink.SentBy.LastName,
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
