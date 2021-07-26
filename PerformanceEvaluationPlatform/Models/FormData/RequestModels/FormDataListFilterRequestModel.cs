using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.Models.Shared;

namespace PerformanceEvaluationPlatform.Models.FormData.RequestModels
{
    public class FormDataListFilterRequestModel : BaseFilterRequestModel
    {
        public int? StateId { get; set; }
        public DateTime? AppointmentDateFrom { get; set; }
        public DateTime? AppointmentDateTo { get; set; }
        public ICollection<int> AssigneeIds { get; set; }
        public ICollection<int> ReviewersIds { get; set; }
        public SortOrder? AssigneeSortOrder { get; set; }
        public SortOrder? FormNameSortOrder { get; set; }
    }
}