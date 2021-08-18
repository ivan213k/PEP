using PerformanceEvaluationPlatform.Application.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceEvaluationPlatform.Models.Document.ViewModels
{
    public class DocumentListItemViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime ValidToDate { get; set;}
        public string FileName { get; set;} 
    }

    public static partial class ViewModelMapperExtensions
    {
        public static DocumentListItemViewModel AsViewModel(this DocumentListItemDto modelDto)
        {
            var documentListItemViewModel = new DocumentListItemViewModel()
            {
                Id = modelDto.Id,
                FileName = modelDto.FileName,
                LastName = modelDto.LastName,
                DocumentTypeName = modelDto.DocumentTypeName,
                ValidToDate = modelDto.ValidToDate,
                FirstName = modelDto.FirstName
            };
            return documentListItemViewModel;
        }
        public static IList<DocumentListItemViewModel> AsIEnumerableViewModel(this IList<DocumentListItemDto> documentDtoItems)
        {
            var converterList = documentDtoItems.Select(t => t.AsViewModel()).ToList();
            return converterList;
        }

    }
}
