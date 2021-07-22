using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldsRepository _fieldsRepository;

        public FieldsController(IFieldsRepository fieldsRepository)
        {
            _fieldsRepository = fieldsRepository ?? throw new ArgumentNullException(nameof(fieldsRepository));
        }

        [HttpPost("fields")]
        public IActionResult Create([FromBody] CreateFieldRequestModel field)
        {
            var items = GetFieldListItemViewModels();
            if (field == null)
            {
                return BadRequest();
            }

            bool fieldAlreadyExists = items.Any(t => t.Id == field.Id);

            if (fieldAlreadyExists)
            {
                ModelState.AddModelError("", "This field already exists");
                return Conflict(ModelState);
            }

            var newField = new FieldListItemViewModel
            {
                //in FieldListItemViewModel we dont use "FieldGroupName", "FieldGroupId", "Description"
                Id = field.Id,
                Name = field.Name,
                Type = field.Type,
                TypeId = field.TypeId,
                AssesmentGroupName = field.AssesmentGroupName,
                AssesmentGroupId = field.AssesmentGroupId,
                IsRequired = field.IsRequired
            };

            items = items.Append(newField);

            return Ok(newField);
        }

        [HttpPost("fields/{id:int}")]
        public IActionResult Copy(int id)
        {
            var items = GetFieldListItemViewModels();
            var item = items.SingleOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var MaxId = items.Max(t => t.Id);
            var newField = new FieldListItemViewModel
            {
                Id = MaxId + 1,
                Name = item.Name,
                Type = item.Type,
                TypeId = item.TypeId,
                AssesmentGroupName = item.AssesmentGroupName,
                AssesmentGroupId = item.AssesmentGroupId,
                IsRequired = item.IsRequired
            };

            items = items.Append(newField);

            return Ok(newField);
        }

        [HttpPut("fields/{id:int}")]
        public IActionResult EditField(int id, [FromBody] EditFieldRequestModel fieldRequestModel)
        {
            if (fieldRequestModel == null)
            {
                return BadRequest();
            }

            var items = GetFieldListItemViewModels();
            var item = items.SingleOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = fieldRequestModel.Name;
            item.IsRequired = fieldRequestModel.IsRequired;

            return Ok();
        }
        [HttpDelete("fields/{id:int}")]
        public IActionResult DeleteField(int id)
        {
            var items = GetFieldListItemViewModels();
            var item = items.SingleOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            items = items.Where(t => t.Id != item.Id).ToList();

            return NoContent();
        }

        [HttpGet("fields")]
        public async Task<IActionResult> Get([FromQuery] FieldListFilterRequestModel filter)
        {
            var filterDto = new FieldListFilterDto
            {
                Search = filter.Search,
                Skip = (int)filter.Skip,
                Take = (int)filter.Take,
                AssesmentGroupIds = filter.AssesmentGroupIds,
                TitleSortOrder = (int?)filter.FieldNameSortOrder,
                TypeIds = filter.TypeIds
            };

            var itemsDto = await _fieldsRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new FieldListItemViewModel
            {
                Id = t.Id,
                Name = t.Name,
                AssesmentGroupName = t.AssesmentGroupName,
                Type = t.TypeName,
                IsRequired = t.IsRequired
            });
            return Ok(items);
        }

        [HttpGet("fields/{id:int}")]
        public async Task<IActionResult> GetFieldDetails(int id)
        {
            var detailsDto = await _fieldsRepository.GetDetails(id);
            if (detailsDto == null)
            {
                return NotFound();
            }

            var detailsVm = new FieldDetailsViewModel
            {
                Id = detailsDto.Id,
                Name = detailsDto.Name,
                AssesmentGroupName = detailsDto.AssasmentGroupName,
                Type = detailsDto.TypeName,
                IsRequired = detailsDto.IsRequired,
                Description = detailsDto.Description
            };

            return Ok(detailsVm);
        }

        //delete FieldGroupListItemViewModel because Olexandr Melnychuk maked this part 

        [HttpGet("fields/types")]
        public async Task<IActionResult> GetTypes()
        {
            var itemsDto = await _fieldsRepository.GetTypesList();
            var items = itemsDto
                .Select(t => new FieldTypeListItemViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return Ok(items);
        }

        private IEnumerable<FieldListItemViewModel> GetOrderedItems(IEnumerable<FieldListItemViewModel> items,
            FieldListFilterRequestModel filter)
        {
            if (filter.FieldNameSortOrder != null)
            {
                if (filter.FieldNameSortOrder == SortOrder.Ascending)
                    items = items.OrderBy(t => t.Name);
                else if (filter.FieldNameSortOrder == SortOrder.Descending)
                    items = items.OrderByDescending(t => t.Name);
            }
            else items = items.OrderBy(t => t.Name);

            return items;
        }

        private IEnumerable<FieldListItemViewModel> GetFilteredItems(IEnumerable<FieldListItemViewModel> items,
        FieldListFilterRequestModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(t => t.Name.Contains(filter.Search));
            }

            if (filter.AssesmentGroupIds != null)
            {
                items = items
                    .Where(t => filter.AssesmentGroupIds.Contains(t.AssesmentGroupId));
            }

            if (filter.TypeIds != null)
            {
                items = items
                    .Where(t => filter.TypeIds.Contains(t.TypeId));
            }

            items = GetOrderedItems(items, filter);

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        private static IEnumerable<FieldListItemViewModel> GetFieldListItemViewModels()
        {
            var items = new List<FieldListItemViewModel>
            {
                new FieldListItemViewModel
                {
                    Id = 1,
                    Name = "Communication skills",
                    Type = "Dropdown with comment",
                    TypeId = 2,
                    AssesmentGroupName = "A-F Group",
                    AssesmentGroupId = 2,
                    IsRequired = true
                },
                new FieldListItemViewModel
                {
                    Id = 2,
                    Name = "Written communication",
                    Type = "Dropdown with comment",
                    TypeId = 2,
                    AssesmentGroupName = "A-F Group",
                    AssesmentGroupId = 2,
                    IsRequired = true
                },
                new FieldListItemViewModel
                {
                    Id = 3,
                    Name = "Full-bleed divider",
                    Type = "Divider",
                    TypeId = 1,
                    AssesmentGroupName = "NoAssesment",
                    AssesmentGroupId = 1,
                    IsRequired = false
                }
            };
            return items;
        }
    }
}
