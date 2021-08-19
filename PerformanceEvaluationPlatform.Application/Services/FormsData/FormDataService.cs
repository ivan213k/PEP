﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Interfaces.Fields;
using PerformanceEvaluationPlatform.Application.Interfaces.FormsData;
using PerformanceEvaluationPlatform.Application.Model.FormsData;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Fields;

namespace PerformanceEvaluationPlatform.Application.Services.FormsData
{
    public class FormDataService : IFormDataService
    {
        private readonly IFormDataRepository _formDataRepository;
        private readonly IFieldsRepository _fieldsRepository;
        private const int InProgressStateId = 2;
        private const int SubmittedStateId = 3;

        public FormDataService(IFormDataRepository formDataRepository, IFieldsRepository fieldsRepository)
        {
            _formDataRepository = formDataRepository;
            _fieldsRepository = fieldsRepository;
        }

        public async Task<ServiceResponse<IList<FormDataListItemDto>>> GetListItems(FormDataListFilterDto filter)
        {
            IList<FormDataListItemDto> items = await _formDataRepository.GetList(filter);
            return ServiceResponse<IList<FormDataListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<IList<FormDataStateListItemDto>>> GetStateListItems()
        {
            var items = await _formDataRepository.GetStatesList();
            return ServiceResponse<IList<FormDataStateListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<FormDataDetailsDto>> GetDetails(int id)
        {
            var details = await _formDataRepository.GetDetails(id);
            return details == null
                ? ServiceResponse<FormDataDetailsDto>.NotFound()
                : ServiceResponse<FormDataDetailsDto>.Success(details);
        }

        public async Task<ServiceResponse> Update(int id, IList<UpdateFieldDataDto> model)
        {
            var entity = await _formDataRepository.Get(id);
            if (entity == null)
            {
                return ServiceResponse.NotFound();
            }
            if (entity.FormDataStateId == SubmittedStateId)
            {
                return ServiceResponse.Failure("The form is already completed", 422);
            }

            foreach (var item in model)
            {
                var field = await _fieldsRepository.Get(item.FieldId);
                if (field == null)
                {
                    return ServiceResponse.Failure<UpdateFieldDataDto>(ufd => ufd.FieldId, "Field does not exists.");
                }
                var assessment = await _fieldsRepository.Get(item.AssesmentId);
                if (assessment == null)
                {
                    return ServiceResponse.Failure<UpdateFieldDataDto>(t => t.AssesmentId, "Assesment does not exists.");
                }
                var ids = entity.FieldData.Select(fd => fd.Field.AssesmentGroupId).Distinct();
                if (!ids.Contains(assessment.AssesmentGroupId))
                {
                    return ServiceResponse.Failure<UpdateFieldDataDto>(t => t.AssesmentId, "Assessment is not related to the field");
                }

                var fieldDataList = await _fieldsRepository.GetFieldData(item.FieldId);
                foreach (var fieldData in fieldDataList)
                {
                    if (fieldData.Assesment.IsCommentRequired && string.IsNullOrWhiteSpace(fieldData.Comment))
                    {
                        return ServiceResponse.Failure<UpdateFieldDataDto>(ufd => ufd.Comment, "Comment is required, but not set");
                    }
                }

            }
            entity.FieldData.Clear();
            var startOrder = 1;
            entity.FieldData = model.Select(m => new FieldData
            {
                Comment = m.Comment,
                FieldId = m.FieldId,
                AssesmentId = m.AssesmentId,
                Order = OrderCounter(ref startOrder)
            }).ToList();

            entity.FormDataStateId = InProgressStateId;

            await _formDataRepository.SaveChanges();
            return ServiceResponse.Success();
        }

        private static int OrderCounter(ref int startOrder)
        {
            return startOrder++;
        }
    }
}
