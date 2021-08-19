using PerformanceEvaluationPlatform.Domain.FormsData;

namespace PerformanceEvaluationPlatform.Domain.Fields
{
    public class FieldData
    {
        public int Id { get; set; }
        public int FormDataId { get; set; }
        public int FieldId { get; set; }
        public int AssesmentId { get; set; }
        public string Comment { get; set; }
        public int Order { get; set; }

        public FormData FormData { get; set; }
        public Field Field { get; set; }
        public Assesment Assesment { get; set; }
    }
}
