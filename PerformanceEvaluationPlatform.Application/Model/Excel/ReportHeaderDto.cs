using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Excel
{
    public class ReportHeaderDto
    {
        public IEnumerable<ReportHeaderPropertiesItemDto> Properties { get; set; }
        public IEnumerable<ReportHeaderAssessmentItemDto> Assessments { get; set; }
        public string Summary { get; set; }
    }
}
