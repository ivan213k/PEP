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
        public async Task<IActionResult> GetStatusesAsync()
        {
            var itemsDto = await _formTemplatesRepository.GetStatusListAsync();
            var items = itemsDto
                .Select(s => new FormTemplateStatusListItemViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                });
            return Ok(items);
        }

       [HttpGet("formtemplates/{id:int}")]
       public async Task<IActionResult> DetailsAsync(int id)
       {
            var itemDto = await _formTemplatesRepository.GetDetailsAsync(id);
            if(itemDto == null)
            {
                return NotFound();
            }
            var item = new FormTemplateDetailsViewModel
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Version = itemDto.Version,
                CreatedAt = itemDto.CreatedAt,
                StatusId = itemDto.StatusId,
                Status = itemDto.Status,
                Fields = itemDto.Fields?
                .Select(t => new FormTemplateFieldViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Order = t.Order,
                    FieldTypeId = t.FieldTypeId,
                    FieldTypeName = t.FieldTypeName
                }).ToList()
            };

            return Ok(item);

       }
    }
}