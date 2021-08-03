using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dto;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
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
        public async Task<IActionResult> Create([FromBody] CreateFieldRequestModel requestModel)
        {
            var fieldType = await _fieldsRepository.GetType(requestModel.TypeId);
            if (fieldType == null)
            {
                return BadRequest("Type does not exists.");
            }

            var assesmentGroup = await _fieldsRepository.GetAssesmentGroup(requestModel.AssesmentGroupId);
            if (assesmentGroup == null)
            {
                return BadRequest("Assesment group does not exists.");
            }

            var field = new Field
            {
                Name = requestModel.Name,
                FieldType = fieldType,
                AssesmentGroup = assesmentGroup,
                IsRequired = requestModel.IsRequired,
                Description = requestModel.Description
            };

            await _fieldsRepository.Create(field);
            await _fieldsRepository.SaveChanges();

            var result = new ObjectResult(new { Id = field.Id }) { StatusCode = 201 };
            return result;
        }

        [HttpPost("fields/{id:int}")]
        public async Task<IActionResult> Copy(int id)
        {
            var entity = await _fieldsRepository.GetField(id);
            if (entity == null)
            {
                return NotFound();
            }

            var field = new Field
            {
                Name = entity.Name,
                FieldTypeId = entity.FieldTypeId,
                AssesmentGroupId = entity.AssesmentGroupId,
                IsRequired = entity.IsRequired,
                Description = entity.Description
            };

            await _fieldsRepository.Create(field);
            await _fieldsRepository.SaveChanges();

            var result = new ObjectResult(new { Id = field.Id }) { StatusCode = 201 };
            return result;
        }

        [HttpPut("fields/{id:int}")]
        public async Task<IActionResult> EditField([FromRoute] int id, [FromBody] EditFieldRequestModel requestModel)
        {
            var entity = await _fieldsRepository.GetField(id);
            if (entity == null)
            {
                return NotFound();
            }

            var fieldType = await _fieldsRepository.GetType(requestModel.TypeId);
            if (fieldType == null)
            {
                return BadRequest("Type does not exists.");
            }

            var assesmentGroup = await _fieldsRepository.GetAssesmentGroup(requestModel.AssesmentGroupId);
            if (assesmentGroup == null)
            {
                return BadRequest("Assesment group does not exists.");
            }

            entity.Name = requestModel.Name;
            entity.FieldTypeId = requestModel.TypeId;
            entity.AssesmentGroupId = requestModel.AssesmentGroupId;
            entity.Description = requestModel.Description;

            await _fieldsRepository.SaveChanges();
            return Ok();
        }
        [HttpDelete("fields/{id:int}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var entity = await _fieldsRepository.GetField(id);
            if (entity == null)
            {
                return NotFound();
            }
            var anyReferenceToFormTemplate = await _fieldsRepository.GetAnyReferenceToFormTemplate(id);
            if (anyReferenceToFormTemplate == false)
            {
                _fieldsRepository.Delete(entity);
                await _fieldsRepository.SaveChanges();

                return NoContent();
            }

            //soft delete
            return BadRequest("The field is already referenced in the form template and could not be deleted!");
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
                AssesmentGroupName = t.AssesmentGroup,
                Type = t.Type,
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
