using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
//using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Fields
{
    public class FieldsRepository : BaseRepository, IFieldsRepository
    {
        public FieldsRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) 
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<FieldListItemDto>> GetList(FieldListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                AssesmentGroupIds = filter.AssesmentGroupIds,
                TypeIds = filter.TypeIds,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = filter.TitleSortOrder
            };

            return ExecuteSp<FieldListItemDto>("[dbo].[spGetFieldListItems]", parameters);
        }

    }
}
