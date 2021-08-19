using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Excel
{
    public class ReportFormDataRowDto
    {
        public string FieldName { get; set; }
        public int FieldTypeId { get; set; }
        public int Order { get; set; }
        public IEnumerable<ReportFieldDataAssessmentDto> Assessments { get; set; }
    }
}
