using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces
{
    public interface IBaseRepository
    {
        Task SaveChanges();
    }
}
