namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class EditRoleRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
    }
}
