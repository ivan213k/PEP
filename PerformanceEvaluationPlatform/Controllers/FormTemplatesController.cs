using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Field;
using PerformanceEvaluationPlatform.Application.Services.FormTemplates;
using PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormTemplatesController : BaseController
    {
        private readonly IFormTemplatesService _formTemplatesService;
        private readonly IFieldService _fieldsService;

        private const int DraftStatusId = 1;
        private const int ActiveStatusId = 2;
        private const int ArchivedStatusId = 3;
        private const int DefaultVersion = 1;

        public FormTemplatesController(IFormTemplatesService formTemplatesService, IFieldService fieldService)
        {
            _formTemplatesService = formTemplatesService ?? throw new ArgumentNullException(nameof(formTemplatesService));
            _fieldsService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
        }

        [HttpGet("formtemplates")]
        public async Task<IActionResult> Get([FromQuery] FormTemplateListFilterOrderRequestModel filter)
        {
            var response = await _formTemplatesService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(response, out IActionResult errorResult))
                return errorResult;

            var formTemplatesDto = response.Payload;
            var listItemsDto = response.Payload;

            var listItemsViewModel = new ListItemsViewModel<FormTemplateListItemViewModel>
            {
                TotalItemsCount = listItemsDto.TotalItemsCount,
                Items = listItemsDto.Items?.Select(t => t.AsViewModel()).ToList()
            };

            return Ok(listItemsViewModel);
        }

        [HttpGet("formtemplates/statuses")]
        public async Task<IActionResult> GetStatuses()
        {
            var itemsResponse = await _formTemplatesService.GetStatusListItems();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
                return errorResult;
            var itemsVm = itemsResponse.Payload.Select(t => t.AsFilterDropDownItemViewModel());

            return Ok(itemsVm);
        }

        [HttpGet("formtemplates/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var itemResponse = await _formTemplatesService.GetDetails(id);

            if (TryGetErrorResult(itemResponse, out IActionResult errorResult))
                return errorResult;

            var itemVm = itemResponse.Payload.AsViewModel();

            return Ok(itemVm);
        }

        [HttpPost("formtemplates")]
        public async Task<IActionResult> Create([FromBody] CreateFormTemplateRequestModel requestModel)
        {
            var response = await _formTemplatesService.Create(requestModel.AsDto());

            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }
            return CreatedAtAction(nameof(Details), new { id = response.Payload }, new IdViewModel { Id = response.Payload });
        }

        //[HttpPut("formtemplates/{id:int}")]
        //public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFormTemplateRequestModel requestModel)
        //{
        //    var formTemplate = await _formTemplatesRepository.Get(id);
        //    if (formTemplate == null)
        //        return NotFound();

        //    await ValidateFormTemplate(requestModel);
        //    if (ModelState.IsValid == false)
        //        return BadRequest(ModelState);

        //    if (formTemplate.StatusId == ArchivedStatusId)
        //    {
        //        var existDraft = await _formTemplatesRepository.ExistDraftFormTemplate(formTemplate.Name);
        //        if (existDraft)
        //            return Forbid();
        //        var idNew = await Copy(formTemplate.Name, requestModel);
        //        return Redirect("/formtemplates/" + idNew);
        //    }

        //    if (formTemplate.StatusId == ActiveStatusId)
        //    {
        //        var existSurveys = await _surveysRepository.ExistByFormTemplateId(formTemplate.Id);
        //        if (existSurveys)
        //        {
        //            var existDraft = await _formTemplatesRepository.ExistDraftFormTemplate(formTemplate.Name);
        //            if (existDraft)
        //                return Forbid();
        //            var idNew = await Copy(formTemplate.Name, requestModel);
        //            return Redirect("/formtemplates/" + idNew);
        //        }
        //    }

        //    formTemplate.FormTemplateFieldMaps.Clear();
        //    formTemplate.FormTemplateFieldMaps = requestModel.Fields.Select(m => new FormTemplateFieldMap
        //    {
        //        FieldId = m.Id,
        //        Order = m.Order
        //    }).ToList();

        //    await _formTemplatesRepository.SaveChanges();

        //    return Redirect("/formtemplates/" + formTemplate.Id);
        //}

        [HttpPut("formtemplates/{id:int}/activate")]
        public async Task<IActionResult> ChangeStatusToActive(int id)
        {
            var response = await _formTemplatesService.ChangeStatusToActive(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
                return errorResult;
            return NoContent();
        }

        //private async Task<int> Copy(string name, UpdateFormTemplateRequestModel requestModel)
        //{
        //    var formTemplate = new FormTemplate
        //    {
        //        Name = name,
        //        CreatedAt = DateTime.Now,
        //        Version = await _formTemplatesRepository.MaxVersion(name) + 1,
        //        StatusId = DraftStatusId,
        //        FormTemplateFieldMaps = requestModel.Fields.Select(t => new FormTemplateFieldMap
        //        {
        //            FieldId = t.Id,
        //            Order = t.Order
        //        }).ToList()
        //    };

        //    await _formTemplatesRepository.Create(formTemplate);
        //    await _formTemplatesRepository.SaveChanges();

        //    return formTemplate.Id;
        //}
    }
}