using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FieldGroups.ViewModels;
using PerformanceEvaluationPlatform.Models.FieldGroups.RequestModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dto;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dao;
using PerformanceEvaluationPlatform.DAL.Repositories.FieldsGroup;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldGroupsController : ControllerBase
    {
        private readonly IFieldsGroupRepository _fieldsGroupRepository;

        public FieldGroupsController(IFieldsGroupRepository fieldsGroupRepository)
        {
            _fieldsGroupRepository = fieldsGroupRepository ?? throw new ArgumentNullException(nameof(fieldsGroupRepository));
        }

        [HttpGet("fields/groups")]
        public async Task<IActionResult> Get([FromQuery]FieldGroupsListFilterRequestModel filter)
        {
            var filterDto = new FieldGroupListFilterDto
            {
                Search = filter.Search,
                CountFrom = filter.CountFrom,
                CountTo = filter.CountTo,
                IsNotEmptyOnly = filter.IsNotEmptyOnly,
                TitleSetOrder = (int?)filter.TitleSetOrder,
                FieldCountSetOrder = (int?)filter.FieldCountSetOrder,
                Skip = filter.Skip,
                Take = filter.Take
            };

            var itemsDto = await _fieldsGroupRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new FieldGroupsListItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                FieldCount = t.FieldCount
            });
            return Ok(items);
        }
    }
}
