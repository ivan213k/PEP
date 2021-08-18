using System.Collections.Generic;
using System;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;

namespace PerformanceEvaluationPlatform.Models.Deeplink.RequestModels
{
    public class DeeplinkListFilterRequestModel : BaseFilterRequestModel
    {
        public int? SentToId { get; set; }

        public DateTime? ExpiresAtFrom { get; set; }
        public DateTime? ExpiresAtTo { get; set; }
        public SortOrder? OrderSentTo { get; set; }
        public SortOrder? OrderExpiresAt { get; set; }

        public ICollection<int> StateIds { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static DeeplinkListFilterDto AsDto(this DeeplinkListFilterRequestModel viewmodel)
        {
            return new DeeplinkListFilterDto
            {
                Search = viewmodel.Search,
                SentToId = viewmodel.SentToId,
                Skip = viewmodel.Skip,
                Take = viewmodel.Take,
                StateIds = viewmodel.StateIds,
                ExpiresAtFrom = viewmodel.ExpiresAtFrom,
                ExpiresAtTo = viewmodel.ExpiresAtTo,
                ExpiresAtOrder = (int?)viewmodel.OrderExpiresAt,
                SentToOrder = (int?)viewmodel.OrderSentTo

            };
        }
    }
}
