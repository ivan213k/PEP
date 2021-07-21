using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.User
{
    class UserRepository : BaseRepository,IUserRepository
    {
        public UserRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            :base(databaseOptions,dbContext)
        {
        }
        public Task<ICollection<UserListItemDto>> GetUsers(UserFilterDto filter)
        {
            throw new NotImplementedException();
        }
    }
}
