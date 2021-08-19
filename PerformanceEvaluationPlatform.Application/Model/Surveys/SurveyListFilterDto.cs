using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Surveys
{
    public class SurveyListFilterDto
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }

        public DateTime? AppointmentDateFrom { get; set; }
        public DateTime? AppointmentDateTo { get; set; }

        public ICollection<int> StateIds { get; set; }
        public ICollection<int> AssigneeIds { get; set; }
        public ICollection<int> SupervisorIds { get; set; }

        public int? FormNameSortOrder { get; set; }
        public int? AssigneeSortOrder { get; set; }
    }
}
