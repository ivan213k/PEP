namespace PerformanceEvaluationPlatform.Application.Model.Surveys
{
    public class SurveyListItemFormDataDto
    {
        public int FormDataAssignedUserId { get; set; }
        public int AssignedUserStateId { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SurveyListItemFormDataDto item))
            {
                return false;
            }
            return FormDataAssignedUserId.Equals(item.FormDataAssignedUserId)
                && AssignedUserStateId.Equals(item.AssignedUserStateId);
        }
        public override int GetHashCode()
        {
            return FormDataAssignedUserId.GetHashCode() ^ AssignedUserStateId.GetHashCode();
        }
    }
}
