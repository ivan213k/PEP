namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class CreateRoleRequestModel
    {
        public string Title { get; set; }
        public bool IsPrimary { get; set; }

        public bool IsTitleValid()
        {
            return Title == null || Title.Length < 2;
        }
    }
}
