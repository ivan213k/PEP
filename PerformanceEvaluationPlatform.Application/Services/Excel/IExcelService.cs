using PerformanceEvaluationPlatform.Application.Model.Excel;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PerformanceEvaluationPlatform.Application.Services.Excel
{
    public interface IExcelService
    {
        MemoryStream Convert(ReportDto report);
    }
}
