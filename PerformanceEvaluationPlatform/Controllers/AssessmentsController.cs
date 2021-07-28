using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private readonly IFieldsRepository _fieldsRepository;

        public AssessmentsController(IFieldsRepository fieldsRepository)
        {
            _fieldsRepository = fieldsRepository ?? throw new ArgumentNullException(nameof(fieldsRepository));
        }

        [HttpGet("assessments/groups")]
        public async Task<IActionResult> GetAssesmentGroup()
        {
            var itemsDto = await _fieldsRepository.GetFieldAssesmentGroupList();
            var items = itemsDto
                .Select(t => new FieldTypeListItemViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return Ok(items);
        }

    }
}
