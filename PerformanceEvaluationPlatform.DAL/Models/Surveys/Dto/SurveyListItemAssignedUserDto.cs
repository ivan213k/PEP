namespace PerformanceEvaluationPlatform.DAL.Models.Surveys.Dto
{
    public class SurveyListItemAssignedUserDto
    {
        public int AssignedUserId { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SurveyListItemAssignedUserDto item))
            {
                return false;
            }

            return AssignedUserId.Equals(item.AssignedUserId);
        }

        public override int GetHashCode()
        {
            return AssignedUserId.GetHashCode();
        }
    }
}
