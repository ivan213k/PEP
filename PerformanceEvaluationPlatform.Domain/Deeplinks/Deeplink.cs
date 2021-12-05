using PerformanceEvaluationPlatform.Domain.Surveys;
using PerformanceEvaluationPlatform.Domain.Users;
using System;

namespace PerformanceEvaluationPlatform.Domain.Deeplinks
{
    public class Deeplink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid Code { get; set; }
        public int StateId { get; set; }
        public int? SentById { get; set; }
        public User SentBy { get; set; }
        public DateTime? SentAt { get; set; }
        public DeeplinkState DeeplinkState { get; set; }
        public User User { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
    