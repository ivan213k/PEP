namespace PerformanceEvaluationPlatform.Models.Shared
{
    public class BaseFilterRequestModel
    {
        public int? Skip { get; set; } = 0;
        public int? Take { get; set; } = 30;

        public string Search { get; set; }
    }
}
