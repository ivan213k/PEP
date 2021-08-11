using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Models.FormTemplates.Interfaces;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormTemplatesController : ControllerBase
    {
        private readonly IFormTemplatesRepository _formTemplatesRepository;
        private readonly IFieldsRepository _fieldsRepository;
        private readonly ISurveysRepository _surveysRepository;

        private const int DraftStatusId = 1;
        private const int ActiveStatusId = 2;
        private const int ArchivedStatusId = 3;
        private const int DefaultVersion = 1;

        public FormTemplatesController(IFormTemplatesRepository formTemplatesRepository, IFieldsRepository fieldsRepository, ISurveysRepository surveysRepository)
        {
            _formTemplatesRepository = formTemplatesRepository ?? throw new ArgumentNullException(nameof(formTemplatesRepository));
            _fieldsRepository = fieldsRepository ?? throw new ArgumentNullException(nameof(fieldsRepository));
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
        }

        [HttpGet("formtemplates")]
        public async Task<IActionResult> GetAsync([FromQuery] FormTemplateListFilterOrderRequestModel filter)
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
            if (itemDto == null)
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateFormTemplateRequestModel requestModel)
        {
            var existName = await _formTemplatesRepository.ExistByName(requestModel.Name);
            if (existName)
            {
                ModelState.AddModelError<CreateFormTemplateRequestModel>(t => t.Name, $"Name = {requestModel.Name} exists!");
                return Conflict(ModelState);
            }

            await ValidateFormTemplate(requestModel);
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var formTemplate = new FormTemplate
            {
                Name = requestModel.Name,
                CreatedAt = DateTime.Now,
                Version = DefaultVersion,
                StatusId = DraftStatusId,
                FormTemplateFieldMaps = requestModel.Fields.Select(t => new FormTemplateFieldMap {
                    FieldId = t.Id,
                    Order = t.Order
                }).ToList()
            };

            await _formTemplatesRepository.Create(formTemplate);
            await _formTemplatesRepository.SaveChanges();

            return Redirect("/formtemplates/" + formTemplate.Id);
        }

        [HttpPut("formtemplates/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFormTemplateRequestModel requestModel)
        { 
            var formTemplate = await _formTemplatesRepository.Get(id);
            if (formTemplate == null)
                return NotFound();

            await ValidateFormTemplate(requestModel);
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (formTemplate.StatusId == ArchivedStatusId)
            {
                var existDraft = await _formTemplatesRepository.ExistDraftFormTemplate(formTemplate.Name);
                if(existDraft)
                    return Forbid();
                var idNew = await Copy(formTemplate.Name, requestModel);
                return Redirect("/formtemplates/" + idNew);
            }

            if (formTemplate.StatusId == ActiveStatusId)
            {
                var existSurveys = await _surveysRepository.ExistByFormTemplateId(formTemplate.Id);
                if(existSurveys)
                {
                    var existDraft = await _formTemplatesRepository.ExistDraftFormTemplate(formTemplate.Name);
                    if (existDraft)
                        return Forbid();
                    var idNew = await Copy(formTemplate.Name, requestModel);
                    return Redirect("/formtemplates/" + idNew);
                }
            }

            formTemplate.FormTemplateFieldMaps.Clear();
            formTemplate.FormTemplateFieldMaps = requestModel.Fields.Select(m => new FormTemplateFieldMap
            {
                FieldId = m.Id,
                Order = m.Order
            }).ToList();

            await _formTemplatesRepository.SaveChanges();

            return Redirect("/formtemplates/" + formTemplate.Id);
        }

        private async Task ValidateFormTemplate(IFormTemplateRequest request)
        {
            var hasDuplicatedFields = request.Fields.GroupBy(f => f.Id).Where(f => f.Count() > 1).ToList();
            var hasDuplicatedOrder = request.Fields.GroupBy(f => f.Order).Where(f => f.Count() > 1).ToList();

            if(hasDuplicatedFields.Any()||hasDuplicatedOrder.Any())
            {
                for (int i = 0; i < request.Fields.Count(); i++)
                {
                    if (hasDuplicatedFields.Any(t => t.Key == request.Fields[i].Id))
                        ModelState.AddModelError<IFormTemplateRequest>(t => t.Fields[i].Id, $"Field with id={request.Fields[i].Id} is repeated!");
                    if (hasDuplicatedOrder.Any(t => t.Key == request.Fields[i].Order))
                        ModelState.AddModelError<IFormTemplateRequest>(t => t.Fields[i].Id, $"Field with order={request.Fields[i].Order} is repeated!");
                }
            }

            var expectedOrder = Enumerable.Range(1, request.Fields.Count);
            var actualOrder = request.Fields
                    .Select(t => t.Order)
                    .OrderBy(t => t);

            var isOrderValid = expectedOrder.SequenceEqual(actualOrder);
            if (!isOrderValid)
                ModelState.AddModelError<IFormTemplateRequest>(t => t.Fields, "Missing order elements!");

            var ids = request.Fields.Select(x => x.Id).Distinct();
            var existedFields = await _fieldsRepository.GetListByIds(ids);

            if (existedFields.Count() != ids.Count())
            {
                foreach (var item in ids)
                {
                    if (!existedFields.Any(t => t.Id == item))
                        ModelState.AddModelError<IFormTemplateRequest>(t => t.Fields, $"Field with {item} does not exist!");
                }
            }
        }

        private async Task<int> Copy(string name, UpdateFormTemplateRequestModel requestModel)
        {
            var formTemplate = new FormTemplate
            {
                Name = name,
                CreatedAt = DateTime.Now,
                Version = await _formTemplatesRepository.MaxVersion(name) + 1,
                StatusId = DraftStatusId,
                FormTemplateFieldMaps = requestModel.Fields.Select(t => new FormTemplateFieldMap
                {
                    FieldId = t.Id,
                    Order = t.Order
                }).ToList()
            };

            await _formTemplatesRepository.Create(formTemplate);
            await _formTemplatesRepository.SaveChanges();

            return formTemplate.Id;
        }
    }
}