using PerformanceEvaluationPlatform.Models.Document.BaseModels;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Repositories.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.Mappers
{
    public static class MyCustomDocumentMapper
    {
        //TODO: Refactor after UserModel
        //refactor after addingUserModel
        public static DocumentListItemViewModel MappListItem(DocumentModel modeldoc, TypeViewModel typedoc) {
            DocumentListItemViewModel item = new DocumentListItemViewModel()
            {
                Id = modeldoc.Id,
                Username = $"testUser {modeldoc.UserId}",//refactor after created UserModel
                DocumentTypeName = typedoc.TypeName,
                ValidToDate = modeldoc.ValidToDate,
                FileName = modeldoc.FileName
            };
            return item;
        }
        //TODO: Remake Mapper
        public static DocumentDetailViewModel MappDetailDocument(DocumentModel modeldoc, TypeModel typedoc) {
            DocumentDetailViewModel item = new DocumentDetailViewModel() {
                Id = modeldoc.Id,
                UserName = $"testUser {modeldoc.UserId}", //refactor after created UserModel
                DocumentType = typedoc.Name,
                ValidTo = modeldoc.ValidToDate,
                FileName = modeldoc.FileName,
                CreatedBy = $"User {modeldoc.CreatedById}",
                CreatedAt = modeldoc.CreatedAt,
                LastUpdatesBy = $"User {modeldoc.LastUpdateById}",
                LastUpdatesAt = modeldoc.LastUpdatesAt.Value
                //TODO: parsing and converting Metadata from json

            };
            return item;
        }
        public static IEnumerable<DocumentListItemViewModel> GetListOfMappedDocumentsItemViewModels(IEnumerable<DocumentModel> documents, IEnumerable<TypeModel> types) {
            List<DocumentListItemViewModel> models = new List<DocumentListItemViewModel>();
            foreach (var document in documents)
            {
                var type = types.FirstOrDefault(x => x.Id == document.TypeId);
                models.Add(MyCustomDocumentMapper.MappListItem(document, MyCustomDocumentMapper.TypeViewModelFromType(type)));
            }
            return models;
        }

        public static TypeViewModel TypeViewModelFromType(TypeModel type) {
            if (type == null) {
                return null;
            }
            return new TypeViewModel { 
                Id=type.Id,
                TypeName = type.Name 
            };
        }
        public static IEnumerable<TypeViewModel> GetTypeViewModelsfromTypes(IEnumerable<TypeModel> types) {
            List<TypeViewModel> typesViewModels = new List<TypeViewModel>();
            foreach (var type in types) {
                typesViewModels.Add(MyCustomDocumentMapper.TypeViewModelFromType(type));
            }
            return typesViewModels;
        }

        public static DocumentModel ConvertRequestAddDocumentModelToBaseModel(RequestAddDocumentModel model) {
            return new DocumentModel
            {
                Id = model.Id,
                UserId = model.UserId,
                TypeId = model.TypeId,
                ValidToDate = model.ValidToDate,
                FileName = model.FileName,
                CreatedById = model.CreatedById,
                CreatedAt = model.CreatedByAt,
                MetaData = model.MetaDate,
            };
        }
    }
}
