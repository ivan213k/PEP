using System;

namespace PerformanceEvaluationPlatform.Domain.Shared
{
    public interface IUpdatebleCreateable
    {
        public DateTime CreatedAt { get; }
        public DateTime? LastUpdatesAt { get;}
    }
}
