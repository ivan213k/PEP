using PerformanceEvaluationPlatform.DAL.Models.Documents.Dto;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceEvaluationPlatform.Models.Document.Mappers
{
    public static class MyCustomDocumentMapper
    {
        public static DocumentListFilterDto AsDocumentListFilterDTO(this DocumentRequestModel request) {
            var documentListFilterDto = new DocumentListFilterDto()
            {
                UserIds=request.UserIds,
                TypeIds=request.TypeIds,
                ValidTo=request.ValidTo,
                Skip=request.Skip,
                Take=request.Take,
                Search=request.Search
            };
            if (request.TypeSortOrder.HasValue) {
                documentListFilterDto.TypeSortOrder = ((int)request.TypeSortOrder.Value);
            }
            if (request.NameSortOrder.HasValue) {
                documentListFilterDto.NameSortOrder = ((int)request.NameSortOrder.Value);
            }
            return documentListFilterDto;
        }

        public static DocumentListItemViewModel AsDocumentListItemViewModel( this DocumentListItemDto modelDto) {
            var documentListItemViewModel = new DocumentListItemViewModel()
            {
                Id=modelDto.Id,
                FileName=modelDto.FileName,
                LastName=modelDto.LastName,
                DocumentTypeName=modelDto.DocumentTypeName,
                ValidToDate=modelDto.ValidToDate,
                FirstName=modelDto.FirstName
            };
            return documentListItemViewModel;
        }

        public static IList<DocumentListItemViewModel> ConvertToIenumerableDocumentListItemViewModelFromIenumerableDocumentListItemDto(IList<DocumentListItemDto> documentDtoItems) {
            var converterList = documentDtoItems.Select(t => t.AsDocumentListItemViewModel()).ToList();
            return converterList;
        }

        public static DocumentDetailViewModel AsDocumentDetailViewModel( this DocumentDetailDto documentDto) {
            var documentitemdetailItem = new DocumentDetailViewModel()
            {
                Id = documentDto.Id,
                FileName = documentDto.FileName,
                FirstName = documentDto.FirstName,
                LastName = documentDto.LastName,
                DocumentType = documentDto.DocumentType,
                ValidTo = documentDto.ValidToDate,
                CreatedByFirstName = documentDto.CreatedByFirstName,
                CreatedByLastName = documentDto.CreatedByLastName,
                CreatedAt = documentDto.CreatedAt,
                LastUpdatesByFirstName = documentDto.LastUpdatesByFirstName,
                LastUpdatesByLastName = documentDto.LastUpdatesByLastName,
                LastUpdatesAt = documentDto.LastUpdatesAt
            };
            return documentitemdetailItem;
        }

        public static TypeViewModel AsTypeViewModel( this DocumentTypeDto doctype) {
            var typeViewModel = new TypeViewModel()
            {
                Id = doctype.Id,
                TypeName = doctype.Name
            };
            return typeViewModel;
        }

        public static IList<TypeViewModel> ConvertToIEnumerableTypeViewModelFromIEnumerableDocumentDetailDto(IList<DocumentTypeDto> documentTypesDtos) {
            var typeViewModels = documentTypesDtos.Select(t => t.AsTypeViewModel()).ToList();
            return typeViewModels;
        }

    }
}
