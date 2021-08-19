using PerformanceEvaluationPlatform.Application.Model.Surveys;
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
    public static partial class ViewModelMapperExtensions
    {
        public static SurveyListFilterDto AsDto(this SurveyListFilterRequestModel requestModel)
        {
            return new SurveyListFilterDto
            {
                AppointmentDateFrom = requestModel.AppointmentDateFrom,
                AppointmentDateTo = requestModel.AppointmentDateTo,
                AssigneeSortOrder = (int?)requestModel.AssigneeSortOrder,
                FormNameSortOrder = (int?)requestModel.FormNameSortOrder,
                Search = requestModel.Search,
                StateIds = requestModel.StateIds,
                AssigneeIds = requestModel.AssigneeIds,
                SupervisorIds = requestModel.SupervisorIds,
                Skip = requestModel.Skip,
                Take = requestModel.Take
            };
        }
    }
}
