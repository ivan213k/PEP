using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Shared
{
    public class ListItemsViewModel<T>
    {
        public int TotalItemsCount { get; set; }

        public IList<T> Items { get; set; }
    }
}
