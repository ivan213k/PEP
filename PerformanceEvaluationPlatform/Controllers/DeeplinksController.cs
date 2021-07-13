using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Deeplink.RequestModels;

using PerformanceEvaluationPlatform.Models.Deeplink.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class DeeeplinksController : ControllerBase
    {

        [HttpGet("deeplinks")]
        public IActionResult Get([FromQuery] DeeplinkListFilterRequestModel filter)
        {
            var items = GetDeeplinkListItemViewModels();
            items = GetFilteredItems(items, filter);
            items = SortItems(items, filter);
            return Ok(items);
        }

        private IEnumerable<DeeplinkListItemViewModel> GetFilteredItems(IEnumerable<DeeplinkListItemViewModel> items,
            DeeplinkListFilterRequestModel filter)
        {
            InitFilter(filter);

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.SentTo.Contains(filter.Search) || t.FormTemplateName.Contains(filter.Search));
            }

            if (filter.SentToIds != null)
            {
                items = items
                    .Where(t => filter.SentToIds.Contains(t.SentToId));
            }

            if (filter.StateIds != null)
            {
                items = items
                    .Where(t => filter.StateIds.Contains(t.StateId));
            }
            if(filter.ExpiresAtFrom != null &&  filter.ExpiresAtFrom != DateTime.MinValue)
            {
                items = items
                    .Where(t => filter.ExpiresAtFrom <= t.ExpiresAt);
            }
         
            if (filter.ExpiresAtTo != null && filter.ExpiresAtTo != DateTime.MinValue)
            {
                items = items
                    .Where(t => filter.ExpiresAtTo >= t.ExpiresAt);
            }

    

            return items;
        }
        [HttpGet("deeplinks/{id}")]
        public IActionResult GetDeeplinkDetails([FromRoute] int id)
        {
            var DeeplinkDetails = new DeeplinkDetailsViewModel
            {
                SentTo = "User Test",
                SentToEmail = "Test@gmail.com",
                SentAt = new DateTime(2021,07,11),
                SentBy = "Another user",
                State = "InProgrees",
                ExpiresAt = new DateTime(2022,2,26),
                FormTemplateName = "Form 1"
            };

            return Ok(DeeplinkDetails);
        }
        private IEnumerable<DeeplinkListItemViewModel> SortItems(IEnumerable<DeeplinkListItemViewModel> items, DeeplinkListFilterRequestModel filter)
        {
            if (filter.OrderSentTo != null)
            {
                if (filter.OrderSentTo == Models.Shared.Enums.SortOrder.Ascending)
                    items = items.OrderBy(t => t.SentTo);
                else
                    items = items.OrderByDescending(t => t.SentTo);
            }

            if (filter.OrderExpiresAt != null)
            {
                if (filter.OrderExpiresAt == Models.Shared.Enums.SortOrder.Ascending)
                    items = items.OrderBy(t => t.ExpiresAt);
                else
                    items = items.OrderByDescending(t => t.ExpiresAt);
            }

            return items;
        }

            private void InitFilter(DeeplinkListFilterRequestModel filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }

            if (filter.Take == null)
            {
                filter.Take = 30;
            }
        }
        [HttpGet("deeplinks/sentto")]
        public IActionResult GetDeeplinkUserTo([FromRoute] int id)
        {
            var DeeplinkUserTo = new List<DeeplinkSentToListItemViewModel>
            {
                new DeeplinkSentToListItemViewModel
                {
                    Id = 1,
                    Name = "User 1"
                },
                new DeeplinkSentToListItemViewModel
                {
                    Id = 2,
                    Name = "User 2"
                }

            };

            return Ok(DeeplinkUserTo);
        }

        private static IEnumerable<DeeplinkListItemViewModel> GetDeeplinkListItemViewModels()
        {
            var items = new List<DeeplinkListItemViewModel>
            {
                new DeeplinkListItemViewModel
                {
                    Id = 4,
                    SentTo = "TestUser1",
                    SentToId = 4,
                    ExpiresAt = new System.DateTime(2019,7,20),
                    State = "Draft",
                    StateId = 1,
                    FormTemplateName = "Form1",
                    FormTemplateNameId = 1


                },
                new DeeplinkListItemViewModel
                {
                    Id = 1,
                    SentTo = "TestUser2",
                    SentToId = 1,
                    ExpiresAt = new System.DateTime(2022,1,23),
                    State = "Draft",
                    StateId = 1,
                    FormTemplateName = "Form2",
                    FormTemplateNameId = 1


                },
                new DeeplinkListItemViewModel
                {
                    Id = 2,
                    SentTo = "TestUser",
                    SentToId = 2,
                    ExpiresAt = new System.DateTime(2021,7,20),
                    State = "Expired",
                    StateId = 2,
                    FormTemplateName = "Form3",
                    FormTemplateNameId = 1


                },
                new DeeplinkListItemViewModel
                {
                    Id=3,
                    SentTo = "Helloy",
                    SentToId = 3,
                    ExpiresAt = new System.DateTime(2024,7,20),
                    State = "Expired",
                    StateId = 2,
                    FormTemplateName = "Form5",
                    FormTemplateNameId = 2


                },
            };
            return items;
        }
    }
       
    
}
