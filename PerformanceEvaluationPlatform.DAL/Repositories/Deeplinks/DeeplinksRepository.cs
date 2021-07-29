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
                .SingleOrDefaultAsync(t => t.Id == id);
            if (deeplink == null)
            {
                return null;
            }

               var details = new DeeplinkDetailsDto
               {
           /*      Id = deeplink.Id,
                   SentToFirstName = 
                   SentToEmail 
                   SentAt 
                   SentBy 
                   StateName = deeplink.DeeplinkState
                   ExpiresAt = deeplink.ExpiresAt
                   FormTemplateName 
           */
                };            
            return details;
         
        } 
    }
}
