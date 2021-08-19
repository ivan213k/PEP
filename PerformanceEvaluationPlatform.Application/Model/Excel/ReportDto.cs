using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Excel
{
    public class ReportDto
    {
        public ReportHeaderDto Header { get; set; }
        public IEnumerable<ReportFormDataRowDto> Rows { get; set; }
    }
}
