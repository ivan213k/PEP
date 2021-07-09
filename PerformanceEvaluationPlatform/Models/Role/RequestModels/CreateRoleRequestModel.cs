namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class CreateRoleRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }
}
