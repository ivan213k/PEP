namespace PerformanceEvaluationPlatform.Models.Role.ViewModels
{
    public class RoleDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }
}
