using PerformanceEvaluationPlatform.Application.Interfaces.Documents;
using PerformanceEvaluationPlatform.Application.Model.Documents;
using PerformanceEvaluationPlatform.Application.Packages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Document
{
    public class DocumentService : IDocumentService
    {
        //TODO: Uncomment after adding User into project
        private readonly IDocumentReposotory _documentReposotory;
        private readonly IDocumentStorage _documentStorage;
        //private readonly IUserRepository _userRepository;
        public DocumentService(IDocumentReposotory documumentRepository, IDocumentStorage documentStorage)
        {
            _documentReposotory = documumentRepository;
            _documentStorage = documentStorage;
        }
        public async Task<ServiceResponse<int>> Create(CreateDocumentDto model)
        {
            if (model.ValidToDate < DateTime.Now) {
                 ServiceResponse.Failure<CreateDocumentDto>(t => t.ValidToDate, "Validation date is expired");   
            }
            if (string.IsNullOrWhiteSpace(model.FileName)) {
                 ServiceResponse.Failure<CreateDocumentDto>(t => t.FileName, "File name cann`t be empty");
            }
            var documentType =  await _documentReposotory.GetTypeModel(model.TypeId);
            if (documentType == null) {
                ServiceResponse.Failure<CreateDocumentDto>(t => t.TypeId, "Document type doesn`t exist");
            }
            //var userDocument = _userRepository.Get(model.UserId);
            //if (userDocument == null) {
               // ServiceResponse.Failure<CreateDocumentDto>(t => t.UserId, "User doesn`t exist");
            //}
            //var createdByUser= _userRepository.Get(model.CreatedById);
            //if (createdByUser == null) {
               // ServiceResponse.Failure<CreateDocumentDto>(t => t.CreatedById, "User doesn`t exist");
            //}
            var document = new Domain.Documents.Document()
            {
                Id=model.Id,
                UserId=model.UserId,
                TypeId=model.TypeId,
                ValidToDate=model.ValidToDate,
                FileName=model.FileName,
                CreatedById=model.CreatedById,
                MetaData=model.MetaDate

            };
            await _documentReposotory.Create(document);
            await _documentReposotory.SaveChanges();
            var path = PathConstructorForStorage(document.UserId, document.FileName);
            await _documentStorage.Upload(path, model.File);
            return ServiceResponse<int>.Success(document.Id, 201);
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var document = await _documentReposotory.Get(id);
            if (document == null) {
               return ServiceResponse.NotFound();
            }
            await _documentReposotory.DeleteDocument(id);
            var path = PathConstructorForStorage(document.UserId, document.FileName);
            await _documentStorage.Delete(path);
           return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<DocumentDetailDto>> GetDocumentDetails(int id)
        {
            var documentDetail = await _documentReposotory.GetDocument(id);
            if (documentDetail == null) {
                return ServiceResponse<DocumentDetailDto>.NotFound();
            }
            return ServiceResponse<DocumentDetailDto>.Success(documentDetail);
        }

        public async Task<ServiceResponse<IList<DocumentListItemDto>>> GetDocuments(DocumentListFilterDto filter)
        {
            var documents = await _documentReposotory.GetDocuments(filter);
            return ServiceResponse<IList<DocumentListItemDto>>.Success(documents);
        }

        public async Task<ServiceResponse<DocumentTypeDto>> GetDocumentType(int id)
        {
            var documentType = await _documentReposotory.GetTypeModel(id);
            return documentType == null
                                    ?ServiceResponse<DocumentTypeDto>.NotFound()
                                    :ServiceResponse<DocumentTypeDto>.Success(documentType);
        }

        public async Task<ServiceResponse<IList<DocumentTypeDto>>> GetTypes()
        {
            var documentsTypes = await _documentReposotory.GetTypes();
            return ServiceResponse<IList<DocumentTypeDto>>.Success(documentsTypes);
        }

        public async Task<ServiceResponse> Update(int id, UpdateDocumentDto model)
        {
            var document = await _documentReposotory.Get(id);
            if (document == null) {
                return ServiceResponse.NotFound();
            }
            if (model.ValidToDate < DateTime.Now)
            {
                return ServiceResponse.Failure<UpdateDocumentDto>(t => t.ValidToDate, "Validation date is expired");
            }
            if (string.IsNullOrWhiteSpace(model.FileName))
            {
                return ServiceResponse.Failure<UpdateDocumentDto>(t => t.FileName, "File name cann`t be empty");
            }
            var documentType = await _documentReposotory.GetTypeModel(model.TypeId);
            if (documentType == null)
            {
                return ServiceResponse.Failure<UpdateDocumentDto>(t => t.TypeId, "Document type doesn`t exist");
            }
            //var userDocument = _userRepository.Get(model.UserId);
            //if (userDocument == null) {
            // return ServiceResponse.Failure<UpdateDocumentDto>(t => t.UserId, "User doesn`t exist");
            //}
            //var createdByUser= _userRepository.Get(model.LastUpdateById);
            //if (createdByUser == null) {
            // return ServiceResponse.Failure<UpdateDocumentDto>(t => t.LastUpdateById, "User doesn`t exist");
            //}
            var oldPath = PathConstructorForStorage(document.UserId, document.FileName);
            var newPath = PathConstructorForStorage(document.UserId, model.FileName);
            document.TypeId = model.TypeId;
            document.ValidToDate = model.ValidToDate;
            document.FileName = model.FileName;
            document.LastUpdatesById = model.LastUpdateById;
            document.MetaData = model.MetaData;
            await _documentReposotory.SaveChanges();
            await _documentStorage.UpdateFileInStorage(oldPath, newPath, model.File);
            return ServiceResponse.Success();
        }
        public async Task<ServiceResponse<BlobFileDto>> FileDownload(int id) {
            var document = await _documentReposotory.Get(id);
            if (document == null)
            {
                return ServiceResponse<BlobFileDto>.NotFound("Document does not exist");
            }
            var path = PathConstructorForStorage(document.UserId, document.FileName);
             var documentFile =await _documentStorage.Download(path);
            return ServiceResponse<BlobFileDto>.Success(documentFile);

        }
        private string PathConstructorForStorage(int UserId, string fileName) {
            return UserId + "/" + fileName;
        }
    }
}
