using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Deeplink.RequestModels;
using PerformanceEvaluationPlatform.Models.Deeplink.VievModels;


namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class DeeeplinksController : ControllerBase
    {

        [HttpGet("Deeplink")]
        public IActionResult Get([FromQuery] DeeplinkListFilterRequestModel filter)
        {
            var items = GetDeeplinkListItemViewModels();
            items = GetFilteredItems(items, filter);
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

            if (filter.SentToId != null)
            {
                items = items
                    .Where(t => t.SentToId == filter.SentToId);
            }

            if (filter.StateIds != null)
            {
                items = items
                    .Where(t => filter.StateIds.Contains(t.StateId));
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

        private static IEnumerable<DeeplinkListItemViewModel> GetDeeplinkListItemViewModels()
        {
            var items = new List<DeeplinkListItemViewModel>
            {
                new DeeplinkListItemViewModel
                {
                    SentTo = "TestUser1",
                    SentToId = 1,
                    ExspiresAt = "23.01.2022",
                    State = "Draft",
                    StateId = 1,
                    FormTemplateName = "Form1",
                    FormTemplateNameId = 1


                },
                new DeeplinkListItemViewModel
                {
                    SentTo = "TestUser",
                    SentToId = 1,
                    ExspiresAt = "23.01.2022",
                    State = "Draft",
                    StateId = 1,
                    FormTemplateName = "Form2",
                    FormTemplateNameId = 1


                },
                new DeeplinkListItemViewModel
                {
                    SentTo = "TestUser",
                    SentToId = 2,
                    ExspiresAt = "23.01.2022",
                    State = "Expired",
                    StateId = 2,
                    FormTemplateName = "Form3",
                    FormTemplateNameId = 1


                },
            };
            return items;
        }
    }
       
    
}
