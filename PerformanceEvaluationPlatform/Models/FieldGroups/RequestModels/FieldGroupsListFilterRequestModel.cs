using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
