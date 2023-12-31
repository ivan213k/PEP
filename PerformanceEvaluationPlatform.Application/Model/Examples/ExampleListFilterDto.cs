﻿using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Examples
{
    public class ExampleListFilterDto
    {
        public int? StateId { get; set; }
        public ICollection<int> TypeIds { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public int? TitleSortOrder { get; set; }
    }
}
