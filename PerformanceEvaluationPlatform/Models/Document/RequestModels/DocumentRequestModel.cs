using PerformanceEvaluationPlatform.Application.Model.Documents;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class DocumentRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> UserIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public DateTime? ValidTo { get; set; }
        public SortOrder? NameSortOrder { get; set; }
        public SortOrder? TypeSortOrder { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static DocumentListFilterDto AsDto(this DocumentRequestModel request)
        {
            var documentListFilterDto = new DocumentListFilterDto()
            {
                UserIds = request.UserIds,
                TypeIds = request.TypeIds,
                ValidTo = request.ValidTo,
                Skip = request.Skip,
                Take = request.Take,
                Search = request.Search
            };
            if (request.TypeSortOrder.HasValue)
            {
                documentListFilterDto.TypeSortOrder = ((int)request.TypeSortOrder.Value);
            }
            if (request.NameSortOrder.HasValue)
            {
                documentListFilterDto.NameSortOrder = ((int)request.NameSortOrder.Value);
            }
            return documentListFilterDto;
        }
    }
}
