using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FieldGroups.ViewModels
{
    public class FieldGroupsListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FieldCount { get; set; }
    }
}
