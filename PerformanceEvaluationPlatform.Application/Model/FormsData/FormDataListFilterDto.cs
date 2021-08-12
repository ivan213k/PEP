using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.FormsData
{
    public class FormDataListFilterDto
    {
        public ICollection<int> AssigneeIds { get; set; }
        public ICollection<int> ReviewersIds { get; set; }
        public DateTime? AppointmentDateFrom { get; set; }
        public DateTime? AppointmentDateTo { get; set; }
        public int? StateId { get; set; }
        public string Search { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public int? AssigneeSortOrder { get; set; }
        public int? FormNameSortOrder { get; set; }
    }
}
