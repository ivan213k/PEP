using PerformanceEvaluationPlatform.Application.Model.Excel;
using System.IO;

namespace PerformanceEvaluationPlatform.Application.Services.Field
{
    public interface IExcelService
    {
        Stream Convert(ReportDto report);
    }
}
