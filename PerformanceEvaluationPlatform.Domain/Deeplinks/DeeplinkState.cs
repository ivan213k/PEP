using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Domain.Deeplinks
{
    public class DeeplinkState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Deeplink> Deeplinks { get; set; }

    }
}
