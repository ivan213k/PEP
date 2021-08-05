using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Dao;
using PerformanceEvaluationPlatform.DAL.Repositories.Document;
using PerformanceEvaluationPlatform.Models.Document.Mappers;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.Validator;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentReposotory _documentsRepository;
        private readonly IDocumentValidator _validatorRequestModels;
        public DocumentController(IDocumentReposotory documentRepository,IDocumentValidator validator) {
            _documentsRepository = documentRepository;
            _validatorRequestModels = validator;
        }
        [HttpGet("documents")]
        public async Task<IActionResult> GetDocuments([FromQuery] DocumentRequestModel filter)
        {
            var filterDto = filter.AsDocumentListFilterDTO();
            var resultDto = await _documentsRepository.GetDocuments(filterDto);
            var result = MyCustomDocumentMapper.ConvertToIenumerableDocumentListItemViewModelFromIenumerableDocumentListItemDto(resultDto);
            return Ok(result);
        }

        [HttpGet("documents/{id:int}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var resultDto = await _documentsRepository.GetDocument(id);
            if (resultDto == null) {
                return NotFound();
            }
            var documentItemDetailViewModel = resultDto.AsDocumentDetailViewModel();
            return Ok(documentItemDetailViewModel);
        }

        [HttpGet("documents/types")]
        public async Task<IActionResult> GetDocumentTypes()
        {
            var documentTypesDtos = await _documentsRepository.GetTypes();
            var typeViewModels = MyCustomDocumentMapper.ConvertToIEnumerableTypeViewModelFromIEnumerableDocumentDetailDto(documentTypesDtos);
            return Ok(typeViewModels);
        }

        [HttpGet("documents/types/{id:int}")]
        public async Task<IActionResult> GetDocumentType(int id)
        {
            var documentTypeDto = await _documentsRepository.GetTypeModel(id);
            if (documentTypeDto == null) {
                return NotFound();
            }
            var typeViewModel = documentTypeDto.AsTypeViewModel();
            return Ok(typeViewModel);
        }

        [HttpPost("document")]
        public async Task< IActionResult> CreateDocument([FromBody] RequestAddDocumentModel model) {
            var errors = _validatorRequestModels.TryValidateRequestAddDocumentModel(model);
            if (!errors.IsValid)
            {
                return BadRequest(errors.ValidationErrors);
            }
            Document docum = new Document()
            {
                Id = model.Id,
                UserId = model.UserId,
                TypeId = model.TypeId,
                ValidToDate = model.ValidToDate,
                FileName = model.FileName,
                CreatedById = model.CreatedById,
                MetaData = model.MetaDate
            };
            await _documentsRepository.Create(docum);
            await _documentsRepository.SaveChanges();
            var result = new ObjectResult(new { Id = docum.Id }) { StatusCode = 201 };
            return Ok(result);
        }

        [HttpPut("document")]
        public async Task <IActionResult> EditDocument([FromBody] RequestUpdateDocumentModel model) {
            var error = await _validatorRequestModels.TryValidateRequestAddDocumentModel(model);
            if (error.IsValid)
            {
                var modelForUpdating = await _documentsRepository.Get(model.Id);
                modelForUpdating.FileName= model.FileName;
                modelForUpdating.LastUpdatesById=model.LastUpdateById;
                modelForUpdating.TypeId= model.TypeId;
                modelForUpdating.ValidToDate = model.ValidToDate;
                await ((DocumentRepository)_documentsRepository).Update(modelForUpdating.Id);
                return Ok(modelForUpdating.Id);
            }
            if (error.ValidationErrors.ContainsKey(nameof(model.Id))) {
                return NotFound();
            }
            return BadRequest(error.ValidationErrors);

        }
    
        [HttpDelete("document/{id:int}")]
        public async Task<IActionResult> DeleteDocument(int id) {
                var DocumentForDeleting =  await _documentsRepository.Get(id);
                if (DocumentForDeleting == null)
                {
                    return NotFound();
                }
                await _documentsRepository.DeleteDocument(id);
           
            return Ok();
        }
    }
}
