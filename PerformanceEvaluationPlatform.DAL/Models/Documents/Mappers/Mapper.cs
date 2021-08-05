using PerformanceEvaluationPlatform.DAL.Models.Documents.Dto;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Dao;
using Documentdoc = PerformanceEvaluationPlatform.DAL.Models.Documents.Dao.Document;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.DAL.Models.Documents.Mappers
{
    internal static class Mapper
    {
        static internal DocumentDetailDto AsDocumentDetailDto(this Documentdoc document) {
            var documentDetailDto = new DocumentDetailDto()
            {
                Id = document.Id,
                FileName = document.FileName,
                LastName = document.User.LastName,
                DocumentType = document.DocumentType.Name,
                FirstName = document.User.FirstName,
                ValidToDate = document.ValidToDate,
                CreatedByFirstName = document.CreatedBy.FirstName,
                CreatedByLastName = document.CreatedBy.LastName,
                CreatedAt = document.CreatedAt,
                LastUpdatesByFirstName = document.UpdatedBy?.FirstName,
                LastUpdatesByLastName = document.UpdatedBy?.LastName,
                LastUpdatesAt = document.LastUpdatesAt
            };
            return documentDetailDto;
        }

        static internal DocumentTypeDto AsDocumentTypeDto( this  DocumentType doctype) {
            var docDto = new DocumentTypeDto() 
            {
                Id=doctype.Id,
                Name=doctype.Name
            };
            return docDto;
        }

        static internal IList<DocumentTypeDto> ConvertToIEnumerableDocumetTypeDtoFromIEnumerableDocumentType(IList<DocumentType> doctypes) {
            List<DocumentTypeDto> docTypesDtos = new List<DocumentTypeDto>();
            foreach (var doctype in doctypes) {
                docTypesDtos.Add(doctype.AsDocumentTypeDto());
            }
            return docTypesDtos;
        }
    }
}
