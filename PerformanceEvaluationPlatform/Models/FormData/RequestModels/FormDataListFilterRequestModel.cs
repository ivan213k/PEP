using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Application.Model.FormsData;

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

    public static partial class ViewModelMapperExtensions
    {
        public static FormDataListFilterDto AsDto(this FormDataListFilterRequestModel requestModel)
        {
            return new FormDataListFilterDto
            {
                Search = requestModel.Search,
                StateId = requestModel.StateId,
                AssigneeSortOrder = (int?)requestModel.AssigneeSortOrder,
                FormNameSortOrder = (int?)requestModel.FormNameSortOrder,
                AssigneeIds = requestModel.AssigneeIds,
                ReviewersIds = requestModel.ReviewersIds,
                AppointmentDateFrom = requestModel.AppointmentDateFrom,
                AppointmentDateTo = requestModel.AppointmentDateTo,
                Skip = requestModel.Skip,
                Take = requestModel.Take
            };
        }
    }
}