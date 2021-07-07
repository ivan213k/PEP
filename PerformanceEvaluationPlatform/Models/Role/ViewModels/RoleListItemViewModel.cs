namespace PerformanceEvaluationPlatform.Models.Role.ViewModels
{
    public class RoleListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }
}
