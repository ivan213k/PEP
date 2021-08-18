using PerformanceEvaluationPlatform.Domain.Documents;
using System;

namespace PerformanceEvaluationPlatform.Application.Model.Documents
{
    public class DocumentDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public DateTime ValidToDate { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatesByFirstName { get; set; }
        public string LastUpdatesByLastName { get; set; }
        public DateTime? LastUpdatesAt { get; set; }

    }
    public static partial class DtoExtensionMapperExtensions
    {
      public  static  DocumentDetailDto AsDto(this Document document)
        {
            var documentDetailDto = new DocumentDetailDto()
            {
                Id = document.Id,
                FileName = document.FileName,
               // LastName = document.User.LastName,
                DocumentType = document.DocumentType.Name,
               // FirstName = document.User.FirstName,
                ValidToDate = document.ValidToDate,
               // CreatedByFirstName = document.CreatedBy.FirstName,
               // CreatedByLastName = document.CreatedBy.LastName,
                CreatedAt = document.CreatedAt,
               // LastUpdatesByFirstName = document.UpdatedBy?.FirstName,
               // LastUpdatesByLastName = document.UpdatedBy?.LastName,
                LastUpdatesAt = document.LastUpdatesAt
            };
            return documentDetailDto;
        }
    }


}
