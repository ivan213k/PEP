namespace PerformanceEvaluationPlatform.Application.Model.Roles
{
    public class RoleDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }
}
