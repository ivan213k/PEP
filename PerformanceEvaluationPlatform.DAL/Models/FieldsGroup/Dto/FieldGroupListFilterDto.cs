namespace PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dto
{
    public class FieldGroupListFilterDto
    {
        public bool IsNotEmptyOnly { get; set; }
        public int? CountFrom { get; set; }
        public int? CountTo { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public int? TitleSetOrder { get; set; }
        public int? FieldCountSetOrder { get; set; }

    }
}
