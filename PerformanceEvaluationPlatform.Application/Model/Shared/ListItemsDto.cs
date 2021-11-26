using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Application.Model.Shared
{
    public class ListItemsDto<TItem>
    {
        public int TotalItemsCount { get; set; }

        public IList<TItem> Items { get; set; }
    }
}
