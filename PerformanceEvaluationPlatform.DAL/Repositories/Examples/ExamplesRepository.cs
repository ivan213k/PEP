using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Examples
{
    public class ExamplesRepository : BaseRepository, IExamplesRepository
    {
        public ExamplesRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) 
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<ExampleListItemDto>> GetList(ExampleListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                StateId = filter.StateId,
                TypeIds = filter.TypeIds,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = filter.TitleSortOrder
            };

            return ExecuteSp<ExampleListItemDto>("[dbo].[spGetExampleListItems]", parameters);
        }

        public async Task<IList<ExampleTypeListItemDto>> GetTypesList()
        {
            return await DbContext.Set<ExampleType>()
                .Select(t => new ExampleTypeListItemDto
                {
                    Id = t.Id,
                    Title = t.Name
                })
                .ToListAsync();
        }

        public async Task<IList<ExampleStateListItemDto>> GetStatesList()
        {
            return await DbContext.Set<ExampleState>()
                .Select(t => new ExampleStateListItemDto
                {
                    Id = t.Id,
                    Title = t.Name
                })
                .ToListAsync();
        }

        public async Task<ExampleDetailsDto> GetDetails(int id)
        {
            var example = await DbContext.Set<Example>()
                .Include(t => t.ExampleState)
                .Include(t => t.ExampleType)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (example == null)
            {
                return null;
            }

            var details = new ExampleDetailsDto
            {
                Id = example.Id,
                Title = example.Title,
                StateName = example.ExampleState.Name,
                TypeName = example.ExampleType.Name,
            };

            return details;
        }

        public Task<ExampleState> GetState(int id)
        {
            return Get<ExampleState>(id);
        }

        public Task Create(Example example)
        {
            return Create<Example>(example);
        }

        public Task<ExampleType> GetType(int id)
        {
            return Get<ExampleType>(id);
        }

        public Task<Example> Get(int id)
        {
            return Get<Example>(id);
        }

        public async Task Update(int id)
        {
            
          
            await DbContext.SaveChangesAsync();
        }
    }
}
