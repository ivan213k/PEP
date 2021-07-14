using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Models.FormData.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.FormData.RequestModels
{
    public class FormDataListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
        public IEnumerable<StateEnum> State { get; set; }
        public DateTime? AppointmentDateFrom { get; set; }
        public DateTime? AppointmentDateTo { get; set; }
        public ICollection<int> AssigneeIds { get; set; }
        public ICollection<int> ReviewersIds { get; set; }
        public SortOrder? FormNameOrderBy { get; set; }
        public SortOrder? AssigneeNameOrderBy { get; set; }

    }
}
