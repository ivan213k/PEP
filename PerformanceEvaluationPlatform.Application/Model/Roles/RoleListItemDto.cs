namespace PerformanceEvaluationPlatform.Application.Model.Roles
{
    public class RoleListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }
}
