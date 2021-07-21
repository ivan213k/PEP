using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormTemplatesController: ControllerBase
    {
        private readonly IFormTemplatesRepository _formTemplatesRepository;

        public FormTemplatesController(IFormTemplatesRepository formTemplatesRepository)
        {
            _formTemplatesRepository = formTemplatesRepository ?? throw new ArgumentNullException(nameof(formTemplatesRepository));
        }

        [HttpGet("formtemplates")]
        public async Task<IActionResult> GetAsync([FromQuery]FormTemplateListFilterOrderRequestModel filter)
        {
            var filterDto = new FormTemplateListFilterOrderDto
            {
                Search = filter.Search,
                StatusIds = filter.StatusIds,
                NameSortOrder = (int?)filter.NameSortOrder,
                Skip = (int)filter.Skip,
                Take = (int)filter.Take
            };
            var itemsDto = await _formTemplatesRepository.GetList(filterDto);
            var items = itemsDto.Select(f => new FormTemplateListItemViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Version = f.Version,
                StatusName = f.StatusName,
                StatusId = f.StatusId,
                CreatedAt = f.CreatedAt
            });
            return Ok(items);
        }

        [HttpGet("formtemplates/statuses")]
        public IActionResult GetStatuses()
        {
            var items = GetFormTemplatesStatusesListItemViewModel();
            return Ok(items);
        }

        private IEnumerable<FormTemplateListItemViewModel> GetFilteredItems(IEnumerable<FormTemplateListItemViewModel> items, FormTemplateListFilterOrderRequestModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items.Where(i => i.Name.Contains(filter.Search));
            }
            if (filter.StatusIds != null)
            {
                items = items
                    .Where(i => filter.StatusIds.Contains(i.StatusId));
            }
            items = GetSortedItems(items, filter);

            items = items.Skip(filter.Skip.Value).Take(filter.Take.Value);

            return items;
        }

        private IEnumerable<FormTemplateListItemViewModel> GetSortedItems(IEnumerable<FormTemplateListItemViewModel> items, FormTemplateListFilterOrderRequestModel filter)
        {
            if (filter.NameSortOrder != null)
            {
                if (filter.NameSortOrder == SortOrder.Ascending)
                    items = items.OrderBy(i => i.Name);
                else
                    items = items.OrderByDescending(i => i.Name);
            }

            return items;
        }

        private IEnumerable<FormTemplateStatusListItemViewModel> GetFormTemplatesStatusesListItemViewModel()
        {
            var items = new List<FormTemplateStatusListItemViewModel>
            {
                new FormTemplateStatusListItemViewModel
                {
                    Id = 1,
                    Name = "Active"
                },
                new FormTemplateStatusListItemViewModel
                {
                    Id = 2,
                    Name = "Draft"
                }
            };
            return items;
        }

        private IEnumerable<FormTemplateListItemViewModel> GetFormTemplatesListItemViewModel()
        {
            var items = new List<FormTemplateListItemViewModel> {
                new FormTemplateListItemViewModel{
                    Name = "Middle Back-End Dev",
                    Version = 12,
                    StatusName = "Draft",
                    StatusId = 2,
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
                new FormTemplateListItemViewModel{
                    Name = "Middle Front-End Dev",
                    Version = 1,
                    StatusName = "Active",
                    StatusId = 1,
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
                new FormTemplateListItemViewModel{
                    Name = "Junior Front-End Dev",
                    Version = 1,
                    StatusName = "Active",
                    StatusId = 1,
                    CreatedAt = new DateTime(2021, 7, 1, 9, 15, 0)
                },
            };
            return items;
        }
    }
}