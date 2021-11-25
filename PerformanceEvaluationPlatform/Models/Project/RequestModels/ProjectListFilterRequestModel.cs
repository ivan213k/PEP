using PerformanceEvaluationPlatform.Application.Model.Projects;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Project.RequestModels
{
    public class ProjectListFilterRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> CoordinatorIds { get; set; }

        public SortOrder? TitleSortOrder { get; set; }
        public SortOrder? StartDateSortOrder { get; set; }
        public SortOrder? CoordinatorSortOrder { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static ProjectListFilterDto AsDto(this ProjectListFilterRequestModel requestModel)
        {
            return new ProjectListFilterDto
            {
                CoordinatorIds = requestModel.CoordinatorIds,
                TitleSortOrder = (int?)requestModel.TitleSortOrder,
                StartDateSortOrder = (int?)requestModel.StartDateSortOrder,
                CoordinatorSortOrder = (int?)requestModel.CoordinatorSortOrder,
                Search = requestModel.Search,
                Skip = requestModel.Skip,
                Take = requestModel.Take,
            };
        }
    }
}