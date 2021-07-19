using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Survey.RequestModels
{
    public class SurveyListFilterRequestModel : BaseFilterRequestModel
    {
        public DateTime? AppointmentDateFrom { get; set; }
        public DateTime? AppointmentDateTo { get; set; }

        public ICollection<int> StateIds { get; set; }
        public ICollection<int> AssigneeIds { get; set; }
        public ICollection<int> SupervisorIds { get; set; }

        public SortOrder? FormNameSortOrder { get; set; }
        public SortOrder? AssigneeSortOrder { get; set; }
    }
}
