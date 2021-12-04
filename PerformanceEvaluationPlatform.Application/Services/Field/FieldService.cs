using PerformanceEvaluationPlatform.Application.Interfaces.Fields;
using PerformanceEvaluationPlatform.Application.Model.Fields;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Application.Model.Shared;

namespace PerformanceEvaluationPlatform.Application.Services.Field
{
    public class FieldService : IFieldService
    {
        private readonly IFieldsRepository _fieldsRepository;

        public FieldService(IFieldsRepository fieldsRepository)
        {
            _fieldsRepository = fieldsRepository;
        }

        public async Task<ServiceResponse<ListItemsDto<FieldListItemDto>>> GetListItems(FieldListFilterDto filter)
        {
            ListItemsDto<FieldListItemDto> items = await _fieldsRepository.GetList(filter);
            return ServiceResponse<ListItemsDto<FieldListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<IList<FieldTypeListItemDto>>> GetTypeListItems()
        {
            var items = await _fieldsRepository.GetTypesList();
            return ServiceResponse<IList<FieldTypeListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<FieldDetailsDto>> GetDetails(int id)
        {
            var details = await _fieldsRepository.GetDetails(id);
            return details == null
                ? ServiceResponse<FieldDetailsDto>.NotFound()
                : ServiceResponse<FieldDetailsDto>.Success(details);
        }

        public async Task<ServiceResponse> Update(int id, EditFieldDto model)
        {
            var entity = await _fieldsRepository.Get(id);
            if (entity == null)
            {
                return ServiceResponse.NotFound();
            }

            var fieldType = await _fieldsRepository.GetType(model.TypeId);
            if (fieldType == null)
            {
                return ServiceResponse.Failure<EditFieldDto>(t => t.TypeId, "Type does not exists.");
            }

            var fieldAssesmentGroup = await _fieldsRepository.GetAssesmentGroup(model.AssesmentGroupId);
            if (fieldAssesmentGroup == null)
            {
                return ServiceResponse.Failure<EditFieldDto>(t => t.AssesmentGroupId, "Assesment group does not exists.");
            }

            entity.Name = model.Name;
            entity.FieldTypeId = model.TypeId;
            entity.AssesmentGroupId = model.AssesmentGroupId;
            entity.Description = model.Description;

            await _fieldsRepository.SaveChanges();
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<int>> Create(CreateFieldDto model)
        {
            var fieldType = await _fieldsRepository.GetType(model.TypeId);
            if (fieldType == null)
            {
                return ServiceResponse<int>.Failure<CreateFieldDto>(t => t.TypeId, "Type does not exists."); //виникає internal server error 
            }

            var fieldAssesmentGroup = await _fieldsRepository.GetAssesmentGroup(model.AssesmentGroupId);
            if (fieldAssesmentGroup == null)
            {
                return ServiceResponse<int>.Failure<CreateFieldDto>(t => t.AssesmentGroupId, "Assesment group does not exists."); 
            }

            var field = new Domain.Fields.Field
            {
                Name = model.Name,
                FieldType = fieldType,
                AssesmentGroup = fieldAssesmentGroup,
                IsRequired = model.IsRequired,
                Description = model.Description
            };

            await _fieldsRepository.Create(field);
            await _fieldsRepository.SaveChanges();

            return ServiceResponse<int>.Success(field.Id, 201);
        }

        public async Task<ServiceResponse<int>> Copy(int id)
        {
            var entity = await _fieldsRepository.Get(id);
            if (entity == null)
            {
                return ServiceResponse<int>.NotFound();
            }
            var field = new Domain.Fields.Field
            {
                Name = entity.Name,
                FieldTypeId = entity.FieldTypeId,
                AssesmentGroupId = entity.AssesmentGroupId,
                IsRequired = entity.IsRequired,
                Description = entity.Description
            };

            await _fieldsRepository.Create(field);
            await _fieldsRepository.SaveChanges();

            return ServiceResponse<int>.Success(field.Id, 201);
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var entity = await _fieldsRepository.Get(id);
            if (entity == null)
            {
                return ServiceResponse.NotFound();
            }
            var anyReferenceToFormTemplate = await _fieldsRepository.GetAnyReferenceToFormTemplate(id);
            if (anyReferenceToFormTemplate == false)
            {
                _fieldsRepository.Delete(entity);
                await _fieldsRepository.SaveChanges();

                return ServiceResponse.Success();
            }

            return ServiceResponse.BadRequest("The field is already referenced in the form template and could not be deleted!");
        }
    }
}
