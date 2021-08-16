using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Document;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class DocumentController : BaseController
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService) {
            _documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
        }
        [HttpGet("documents")]
        public async Task<IActionResult> GetDocuments([FromQuery] DocumentRequestModel filter)
        {
            var filterDto = filter.AsDto();
            var result = await _documentService.GetDocuments(filterDto);
            if (TryGetErrorResult(result, out IActionResult errorResult)) {
                return errorResult;
            }
            var resultDocuments = result.Payload.Select(t => t.AsViewModel());
            return Ok(resultDocuments);
        }

        [HttpGet("documents/{id:int}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var documentresponce =  await _documentService.GetDocumentDetails(id);
            if (TryGetErrorResult(documentresponce, out IActionResult errorResult)) {
                return errorResult;
            }
            var detailDocumnet = documentresponce.Payload.AsViewModel();
            return Ok(detailDocumnet);
        }

        [HttpGet("documents/types")]
        public async Task<IActionResult> GetDocumentTypes()
        {
            var documentTypesResponce = await _documentService.GetTypes();
            if (TryGetErrorResult(documentTypesResponce, out IActionResult errorResult))
            {
                return errorResult;
            }

            var documentTypes = documentTypesResponce.Payload.Select(t => t.AsViewModel());
            return Ok(documentTypes);
        }

        [HttpGet("documents/types/{id:int}")]
        public async Task<IActionResult> GetDocumentType(int id)
        {
            var documentTypeResponce = await _documentService.GetDocumentType(id);
            if (TryGetErrorResult(documentTypeResponce, out IActionResult errorResult))
            {
                return errorResult;
            }
            var typeViewModel = documentTypeResponce.Payload.AsViewModel();
            return Ok(typeViewModel);
        }

        [HttpPost("document")]
        public async Task< IActionResult> CreateDocument([FromBody] RequestAddDocumentModel model) {
            var createDocumentResponce = await _documentService.Create(model.AsDto());
            return TryGetErrorResult(createDocumentResponce, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = createDocumentResponce.Payload }) { StatusCode = 201 };
        }

        [HttpPut("document/{id:int}")]
        public async Task <IActionResult> EditDocument([FromRoute]int id,[FromBody] RequestUpdateDocumentModel model) {
            var documentUpdateResponce = await _documentService.Update(id, model.AsDto());
            return TryGetErrorResult(documentUpdateResponce, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }
    
        [HttpDelete("document/{id:int}")]
        public async Task<IActionResult> DeleteDocument(int id) {
            var documentDeletingResponce = await _documentService.Delete(id);
            if (TryGetErrorResult(documentDeletingResponce, out IActionResult errorResult))
            {
                return errorResult;
            }
            return Ok();
        }
    }
}
