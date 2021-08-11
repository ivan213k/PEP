using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class FieldListFilterRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> AssesmentGroupIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public SortOrder? FieldNameSortOrder { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static FieldListFilterDto AsDto(this FieldListFilterRequestModel viewmodel)
        {
            return new FieldListFilterDto
            {
                Search = viewmodel.Search,
                Skip = (int)viewmodel.Skip,
                Take = (int)viewmodel.Take,
                AssesmentGroupIds = viewmodel.AssesmentGroupIds,
                TitleSortOrder = (int?)viewmodel.FieldNameSortOrder,
                TypeIds = viewmodel.TypeIds
            };
        }
    }
   
}