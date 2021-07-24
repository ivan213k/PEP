using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories
{
    public interface IBaseRepository
    {
        Task SaveChanges();
    }
}
