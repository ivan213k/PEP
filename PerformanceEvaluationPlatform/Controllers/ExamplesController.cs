using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class ExamplesController : ControllerBase
    {

        [HttpGet("examples")]
        public IActionResult Get([FromQuery]ExampleListFilterRequestModel filter)
        {
            var items = GetExampleListItemViewModels();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        [HttpGet("examples/types")]
        public IActionResult GetTypes()
        {
            var items = new List<ExampleTypeListItemViewModel>
            {
                new ExampleTypeListItemViewModel
                {
                    Id = 1,
                    Name = "Type 1"
                },
                new ExampleTypeListItemViewModel
                {
                    Id = 2,
                    Name = "Type 2"
                }
            };

            return Ok(items);
        }

        [HttpGet("examples/states")]
        public IActionResult GetStates()
        {
            var items = new List<ExampleStateListItemViewModel>
            {
                new ExampleStateListItemViewModel
                {
                    Id = 1,
                    Name = "Active"
                },
                new ExampleStateListItemViewModel
                {
                    Id = 2,
                    Name = "Blocked"
                }
            };

            return Ok(items);
        }

        private IEnumerable<ExampleListItemViewModel> GetFilteredItems(IEnumerable<ExampleListItemViewModel> items,
            ExampleListFilterRequestModel filter)
        {
            InitFilter(filter);

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Title.Contains(filter.Search) || t.User.Contains(filter.Search));
            }

            if (filter.StateId != null)
            {
                items = items
                    .Where(t => t.StateId == filter.StateId);
            }

            if (filter.TypeIds != null)
            {
                items = items
                    .Where(t => filter.TypeIds.Contains(t.TypeId));
            }

            return items;
        }

        private void InitFilter(ExampleListFilterRequestModel filter)
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
        private static IEnumerable<ExampleListItemViewModel> GetExampleListItemViewModels()
        {
            var items = new List<ExampleListItemViewModel>
            {
                new ExampleListItemViewModel
                {
                    Title = "Example 1",
                    Type = "Type 1",
                    TypeId = 1,
                    State = "Active",
                    StateId = 1,
                    User = "Test User 1"
                },
                new ExampleListItemViewModel
                {
                    Title = "Example 2",
                    Type = "Type 2",
                    TypeId = 2,
                    State = "Blocked",
                    StateId = 2,
                    User = "Test User 2"
                },
                new ExampleListItemViewModel
                {
                    Title = "Example 3",
                    Type = "Type 1",
                    TypeId = 1,
                    State = "Active",
                    StateId = 1,
                    User = "Test User 3"
                }
            };
            return items;
        }
    }
}
