using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class FormTemplateListFilterOrderRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> StatusIds { get; set; }
        public SortOrder? NameSortOrder { get; set; }
    }

	public static partial class ViewModelMapperExtensions
	{
		public static FormTemplateListFilterOrderDto AsDto(this FormTemplateListFilterOrderRequestModel requestModel)
		{
			return new FormTemplateListFilterOrderDto
            {
                Search = requestModel.Search,
                StatusIds = requestModel.StatusIds,
                NameSortOrder = (int?)requestModel.NameSortOrder,
                Skip = (int)requestModel.Skip,
                Take = (int)requestModel.Take
            }; ;
		}
	}
}
