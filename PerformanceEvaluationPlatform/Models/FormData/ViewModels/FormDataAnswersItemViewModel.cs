using PerformanceEvaluationPlatform.Application.Model.FormsData;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataAnswersItemViewModel
    {
        public string Skills { get; set; }
        public string Comment { get; set; }
        public string Assessment { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }
        public int Order { get; set; }

    }

    public static partial class ViewModelMapperExtensions
    {
        public static FormDataAnswersItemViewModel AsViewModel(this FormDataAnswersItemDto dto)
        {
            return new FormDataAnswersItemViewModel
            {
                Assessment = dto.Assessment,
                Comment = dto.Comment,
                TypeId = dto.TypeId,
                TypeName = dto.TypeName,
                Order = dto.Order,
            };
        }
    }
}
