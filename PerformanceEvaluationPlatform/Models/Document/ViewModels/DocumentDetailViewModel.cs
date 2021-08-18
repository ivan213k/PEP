using PerformanceEvaluationPlatform.Application.Model.Documents;
using System;

namespace PerformanceEvaluationPlatform.Models.Document.ViewModels
{
    public class DocumentDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public DateTime ValidTo { get; set; }
        public string FileName { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatesByFirstName { get; set; }
        public string LastUpdatesByLastName { get; set; }
        public DateTime? LastUpdatesAt { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static DocumentDetailViewModel AsViewModel(this DocumentDetailDto documentDto)
        {
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
    }
}
