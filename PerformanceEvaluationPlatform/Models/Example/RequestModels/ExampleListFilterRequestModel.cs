using PerformanceEvaluationPlatform.Application.Model.Examples;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Example.RequestModels
{
    public class ExampleListFilterRequestModel : BaseFilterRequestModel
    {
        public int? StateId { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public SortOrder? TitleSortOrder { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static ExampleListFilterDto AsDto(this ExampleListFilterRequestModel viewmodel)
        {
            return new ExampleListFilterDto
            {
                Search = viewmodel.Search,
                Skip = viewmodel.Skip,
                Take = viewmodel.Take,
                StateId = viewmodel.StateId,
                TitleSortOrder = (int?)viewmodel.TitleSortOrder,
                TypeIds = viewmodel.TypeIds
            };
        }
    }
}
