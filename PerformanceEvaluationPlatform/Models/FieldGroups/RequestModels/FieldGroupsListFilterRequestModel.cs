using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Models.FieldGroups.RequestModels
{
    public class FieldGroupsListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public bool IsNotEmptyOnly { get; set; }
        public int? CountFrom { get; set; }
        public int? CountTo { get; set; }

        public SortOrder? TitleSetOrder { get; set; }
        public SortOrder? FieldCountSetOrder { get; set; }
    }
}
