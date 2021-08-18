using Microsoft.Extensions.Logging;
using PerformanceEvaluationPlatform.Application.Interfaces.Fields;
using PerformanceEvaluationPlatform.Application.Interfaces.FormTemplates;
using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Interfaces;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.FormTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.FormTemplates
{
    public class FormTemplatesService: IFormTemplatesService
    {
        private readonly IFormTemplatesRepository _formTemplatesRepository;
        private readonly IFieldsRepository _fieldsRepository;
        private readonly ILogger _logger;

        private const int DraftStatusId = 1;
        private const int ActiveStatusId = 2;
        private const int ArchivedStatusId = 3;
        private const int DefaultVersion = 1;

        public FormTemplatesService(IFormTemplatesRepository formTemplatesRepository, IFieldsRepository fieldsRepository, ILogger<FormTemplatesService> logger)
        {
            _formTemplatesRepository = formTemplatesRepository ?? throw new ArgumentNullException(nameof(formTemplatesRepository));
            _fieldsRepository = fieldsRepository ?? throw new ArgumentNullException(nameof(fieldsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceResponse<int>> Create(CreateFormTemplateDto model)
        {
            var existName = await _formTemplatesRepository.ExistByName(model.Name);
            if (existName)
                return ServiceResponse<int>.Conflict<CreateFormTemplateDto>(t => t.Name, $"Name = {model.Name} exists!");

            var errors = await ValidateFormTemplate(model);
            if (errors != null)
                return ServiceResponse<int>.BadRequest(errors);

            var formTemplate = new FormTemplate
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
                Version = DefaultVersion,
                StatusId = DraftStatusId,
                FormTemplateFieldMaps = model.Fields.Select(t => new FormTemplateFieldMap
                {
                    FieldId = t.Id,
                    Order = t.Order
                }).ToList()
            };

            await _formTemplatesRepository.Create(formTemplate);
            await _formTemplatesRepository.SaveChanges();

            return ServiceResponse<int>.Success(formTemplate.Id);
        }

        public async Task<ServiceResponse<FormTemplateDetailsDto>> GetDetails(int id)
        {
            var item = await _formTemplatesRepository.GetDetails(id);
            return item == null ? ServiceResponse<FormTemplateDetailsDto>.NotFound()
                : ServiceResponse<FormTemplateDetailsDto>.Success(item);
        }

        public async Task<ServiceResponse<IList<FormTemplateListItemDto>>> GetListItems(FormTemplateListFilterOrderDto filter)
        {
            var items = await _formTemplatesRepository.GetList(filter);
            return ServiceResponse<IList<FormTemplateListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<IList<FormTemplateStatusListItemDto>>> GetStatusListItems()
        {
            var items = await _formTemplatesRepository.GetStatusList();
            return ServiceResponse<IList<FormTemplateStatusListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse> ChangeStatusToActive(int id)
        {
            var formTemplate = await _formTemplatesRepository.Get(id);
            if (formTemplate == null)
                return ServiceResponse.NotFound();

            if (formTemplate.StatusId != DraftStatusId)
                return ServiceResponse.UnprocessableEntity("Form Template is not in draft status!");

            var activeFormTemplate = await _formTemplatesRepository.GetActiveFormTemplate(formTemplate.Name);
            if (activeFormTemplate != null)
            {
                if (activeFormTemplate.Count() > 1)
                    _logger.LogWarning("Activated form templates with \"{name}\" name more than one!", formTemplate.Name);
                activeFormTemplate.ToList().ForEach(t => t.StatusId = ArchivedStatusId);
            }

            formTemplate.StatusId = ActiveStatusId;
            await _formTemplatesRepository.SaveChanges();
            return ServiceResponse.Success();
        }

        public async Task<Dictionary<string, ICollection<string>>> ValidateFormTemplate(IFormTemplateRequest request)
        {
            var errors = new Dictionary<string, ICollection<string>>();
            var hasDuplicatedFields = request.Fields.GroupBy(f => f.Id).Where(f => f.Count() > 1).ToList();
            var hasDuplicatedOrder = request.Fields.GroupBy(f => f.Order).Where(f => f.Count() > 1).ToList();

            if (hasDuplicatedFields.Any() || hasDuplicatedOrder.Any())
            {
                for (int i = 0; i < request.Fields.Count(); i++)
                {
                    if (hasDuplicatedFields.Any(t => t.Key == request.Fields[i].Id))
                    {
                        if (errors.ContainsKey($"Fields[{i}].Id"))
                            errors[$"Fields[{i}].Id"].Add($"Field with id={request.Fields[i].Id} is repeated!");
                        else
                            errors.Add($"Fields[{i}].Id", new List<string> { $"Field with id={request.Fields[i].Id} is repeated!" });
                    }

                    if (hasDuplicatedOrder.Any(t => t.Key == request.Fields[i].Order))
                    {
                        if (errors.ContainsKey($"Fields[{i}].Id"))
                            errors[$"Fields[{i}].Id"].Add($"Field with order={request.Fields[i].Order} is repeated!");
                        else
                            errors.Add($"Fields[{i}].Id", new List<string> { $"Field with order={request.Fields[i].Order} is repeated!" });
                    }
                }
            }

            var expectedOrder = Enumerable.Range(1, request.Fields.Count);
            var actualOrder = request.Fields
                    .Select(t => t.Order)
                    .OrderBy(t => t);

            var isOrderValid = expectedOrder.SequenceEqual(actualOrder);
            if (!isOrderValid)
            {
                if (errors.ContainsKey("Fields"))
                    errors["Fields"].Add("Missing order elements!");
                else
                    errors.Add("Fields", new List<string> { "Missing order elements!" });
            }

            var ids = request.Fields.Select(x => x.Id).Distinct();
            var existedFields = await _fieldsRepository.GetListByIds(ids);

            if (existedFields.Count() != ids.Count())
            {
                foreach (var item in ids)
                {
                    if (!existedFields.Any(t => t.Id == item))
                    {
                        if (errors.ContainsKey("Fields"))
                            errors["Fields"].Add($"Field with {item} does not exist!");
                        else
                            errors.Add("Fields", new List<string> { $"Field with {item} does not exist!" });
                    }
                }
            }

            return errors;
        }
    }
}
