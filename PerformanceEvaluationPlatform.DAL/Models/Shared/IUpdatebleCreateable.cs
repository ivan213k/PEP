using System;

namespace PerformanceEvaluationPlatform.DAL.Models.Shared
{
    public interface IUpdatebleCreateable
    {
        public DateTime CreatedAt { get; }
        public DateTime? LastUpdatesAt { get;}

    }
}
