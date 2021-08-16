using PerformanceEvaluationPlatform.Application.Model.Documents;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceEvaluationPlatform.Models.Document.ViewModels
{
    public class TypeViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static TypeViewModel AsViewModel(this DocumentTypeDto doctype)
        {
            var typeViewModel = new TypeViewModel()
            {
                Id = doctype.Id,
                TypeName = doctype.Name
            };
            return typeViewModel;
        }
        public static IList<TypeViewModel> AsIEnumerableViewModel(this IList<DocumentTypeDto> documentTypesDtos)
        {
            var typeViewModels = documentTypesDtos.Select(t => t.AsViewModel()).ToList();
            return typeViewModels;
        }
    }
}
