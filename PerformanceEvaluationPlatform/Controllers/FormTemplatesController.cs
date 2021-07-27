using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
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
        private readonly IFieldsRepository _fieldsRepository;

        public FormTemplatesController(IFormTemplatesRepository formTemplatesRepository, IFieldsRepository fieldsRepository)
        {
            _formTemplatesRepository = formTemplatesRepository ?? throw new ArgumentNullException(nameof(formTemplatesRepository));
            _fieldsRepository = fieldsRepository ?? throw new ArgumentNullException(nameof(fieldsRepository));
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
        [HttpPost("formtemplates")]
        public async Task<IActionResult> CreateAsync([FromBody]CreateFormTemplateRequestModel requestModel)
        {
            var fieldIds = requestModel.Fields.GroupBy(f => f.Id).Any(f => f.Count() > 1);
            if (fieldIds)
            {
                return BadRequest("Fields can't repeat!");
            }

            var orders = requestModel.Fields.GroupBy(f => f.Order).Any(f => f.Count() > 1);
            if (orders)
            {
                return BadRequest("Orders can't repeat!");
            }

            var existName = await _formTemplatesRepository.ExistByName(requestModel.Name);
            if (existName)
            {
                return BadRequest("This name is exist");
            }

            var ids = requestModel.Fields.Select(x => x.Id);

            var fields = await _fieldsRepository.GetListByIds(ids);

            if (fields.Count() != ids.Count())
                return BadRequest("Not all fields exist!");

            var formTemplate = new FormTemplate
            {
                Name = requestModel.Name,
                CreatedAt = DateTime.Now,
                Version = DefaultVersion,
                StatusId = DraftStateId,
                FormTemplateFieldMaps = requestModel.Fields.Select(t => new FormTemplateFieldMap {
                    FieldId = t.Id,
                    Order = t.Order
                }).ToList()
            };

            await _formTemplatesRepository.Create(formTemplate);
            await _formTemplatesRepository.SaveChanges();
            return Redirect("/formtemplates/" + formTemplate.Id);
        }

        private const int DraftStateId = 2;
        private const int DefaultVersion = 1;
    }
}