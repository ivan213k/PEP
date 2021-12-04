using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Field;
using PerformanceEvaluationPlatform.Application.Services.Excel;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Application.Packages;
using System.IO;
using PerformanceEvaluationPlatform.Application.Model.Excel;
using PerformanceEvaluationPlatform.Models.Shared;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldsController : BaseController
    {
        private readonly IFieldService _fieldService;
        private readonly IExcelService _excelService;

        public FieldsController(IFieldService fieldService, IExcelService excelService)
        {
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
        }

        [HttpPost("fields")]
        public async Task<IActionResult> Create([FromBody] CreateFieldRequestModel requestModel)
        {
            ServiceResponse<int> serviceResponse = await _fieldService.Create(requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };
        }

        [HttpPost("fields/{id:int}")]
        public async Task<IActionResult> Copy(int id)
        {
            ServiceResponse<int> serviceResponse = await _fieldService.Copy(id);
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };
        }

        [HttpPut("fields/{id:int}")]
        public async Task<IActionResult> EditField([FromRoute] int id, [FromBody] EditFieldRequestModel requestModel)
        {
            ServiceResponse serviceResponse = await _fieldService.Update(id, requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }
        [HttpDelete("fields/{id:int}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            ServiceResponse serviceResponse = await _fieldService.Delete(id);
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }

        [HttpGet("fields")]
        public async Task<IActionResult> Get([FromQuery] FieldListFilterRequestModel filter)
        {
            var response = await _fieldService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            var itemsVm = new ListItemsViewModel<FieldListItemViewModel>
            {
                Items = response.Payload?.Items.Select(t => t.AsViewModel()).ToList(),
                TotalItemsCount = response.Payload.TotalItemsCount
            }; 
            return Ok(itemsVm);
        }

        [HttpGet("fields/{id:int}")]
        public async Task<IActionResult> GetFieldDetails(int id)
        {
            var detailsResponse = await _fieldService.GetDetails(id);
            if (TryGetErrorResult(detailsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var detailsVm = detailsResponse.Payload.AsViewModel();
            return Ok(detailsVm);
        }

        [HttpGet("fields/types")]
        public async Task<IActionResult> GetTypes()
        {
            var itemsResponse = await _fieldService.GetTypeListItems();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var items = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(items);
        }
        [HttpPost("fields/excel")]
        public FileStreamResult CreateExcel()
        {   //тестове формування файла
            var propertyList = new List<ReportHeaderPropertiesItemDto>
            {
                new ReportHeaderPropertiesItemDto{ PropertyName = "Responsible person:", PropertyValue = "Petro Petrov" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Developer's name:", PropertyValue = "Ivan Vasylenko", IsBold = true },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Current technical level:", PropertyValue = "Junior" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Technical level (Knowledge evaluation):", PropertyValue = "" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "English level:", PropertyValue = "" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Date:", PropertyValue = "июня 26, 2021" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Project:", PropertyValue = "Bla-Bla Project" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Period:", PropertyValue = "01.01.2021-30.06.2021" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Work experience at SharpMinds:", PropertyValue = "0 years 6 months" },
                new ReportHeaderPropertiesItemDto{ PropertyName = "Current position:", PropertyValue = "QC Engineer" }
            };

            var assessmentsList = new List<ReportHeaderAssessmentItemDto>
            {
                new ReportHeaderAssessmentItemDto{ Name = "A (найвищий бал)", IsCommentRequired = true },
                new ReportHeaderAssessmentItemDto{ Name = "B", IsCommentRequired = false },
                new ReportHeaderAssessmentItemDto{ Name = "C", IsCommentRequired = true },
                new ReportHeaderAssessmentItemDto{ Name = "D", IsCommentRequired = true },
                new ReportHeaderAssessmentItemDto{ Name = "E (найнижчий бал)", IsCommentRequired = true },
                new ReportHeaderAssessmentItemDto{ Name = "Not Applicable", IsCommentRequired = false },
                new ReportHeaderAssessmentItemDto{ Name = "Hard to Evaluate", IsCommentRequired = false }
            };

            var rowData = new List<ReportFormDataRowDto>
            {
                new ReportFormDataRowDto{ FieldName = "Communication skills", FieldTypeId = 1, Order = 2, Assessments = new List<ReportFieldDataAssessmentDto>
                    {
                        new ReportFieldDataAssessmentDto{ ReporterName = "Self-evaluation" },
                        new ReportFieldDataAssessmentDto{ ReporterName = "Team Leader evaluation" }
                    } 
                },
                new ReportFormDataRowDto{ FieldName = "Written communication", FieldTypeId = 2, Order = 3, Assessments = new List<ReportFieldDataAssessmentDto>
                    {
                        new ReportFieldDataAssessmentDto{ ReporterName = "Self-evaluation", AssessmentName = "A", Comment = "Test 1" },
                        new ReportFieldDataAssessmentDto{ ReporterName = "Team Leader evaluation", AssessmentName = "B", Comment = "Test 2" }
                    }
                },
                new ReportFormDataRowDto{ FieldName = "Oral communication", FieldTypeId = 2, Order = 4, Assessments = new List<ReportFieldDataAssessmentDto>
                    {
                        new ReportFieldDataAssessmentDto{ ReporterName = "Self-evaluation", AssessmentName = "B", Comment = "Test 3" },
                        new ReportFieldDataAssessmentDto() { ReporterName = "Team Leader evaluation", AssessmentName = "C", Comment = "Test 4" }
                    }
                },
                new ReportFormDataRowDto{ FieldName = "Professional Skills", FieldTypeId = 1, Order = 5, Assessments = new List<ReportFieldDataAssessmentDto>
                    {
                        new ReportFieldDataAssessmentDto{ ReporterName = "Self-evaluation" },
                        new ReportFieldDataAssessmentDto{ ReporterName = "Team Leader evaluation" }
                    }
                },
                new ReportFormDataRowDto{ FieldName = "Requirements analysis", FieldTypeId = 2, Order = 7, Assessments = new List<ReportFieldDataAssessmentDto>
                    {
                        new ReportFieldDataAssessmentDto{ ReporterName = "Self-evaluation", AssessmentName = "B", Comment = "Test 7" },
                        new ReportFieldDataAssessmentDto() { ReporterName = "Team Leader evaluation", AssessmentName = "C", Comment = "Test 8" }
                    }
                },
                new ReportFormDataRowDto{ FieldName = "Test planning", FieldTypeId = 2, Order = 6, Assessments = new List<ReportFieldDataAssessmentDto>
                    {
                        new ReportFieldDataAssessmentDto{ ReporterName = "Self-evaluation", AssessmentName = "C", Comment = "Test 5" },
                        new ReportFieldDataAssessmentDto() { ReporterName = "Team Leader evaluation", AssessmentName = "E", Comment = "Test 6" }
                    }
                },

            };

            ReportDto report = new ReportDto
            {
                Header = new ReportHeaderDto
                {
                    Properties = propertyList,
                    Assessments = assessmentsList,
                    Summary = "Test test test test Test test test test Test test test test Test test test test Test test test test"
                },
                Rows = rowData
            };

            var memoryStream = _excelService.Convert(report);
            var fileStreamReturn = new FileStreamResult(memoryStream, "application/xlsx");
            fileStreamReturn.FileDownloadName = "ExcelTest.xlsx";

            return fileStreamReturn;
        }
    }
}
