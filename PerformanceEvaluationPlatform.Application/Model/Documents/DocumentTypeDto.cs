using PerformanceEvaluationPlatform.Domain.Documents;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Documents
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class DtoExtensionMapperExtensions {
       public  static  DocumentTypeDto AsDto(this DocumentType doctype)
        {
            var docDto = new DocumentTypeDto()
            {
                Id = doctype.Id,
                Name = doctype.Name
            };
            return docDto;
        }

        public static IList<DocumentTypeDto> AsIEnumerableDtos(this IList<DocumentType> doctypes)
        {
            List<DocumentTypeDto> docTypesDtos = new List<DocumentTypeDto>();
            foreach (var doctype in doctypes)
            {
                docTypesDtos.Add(doctype.AsDto());
            }
            return docTypesDtos;
        }
    }
}
