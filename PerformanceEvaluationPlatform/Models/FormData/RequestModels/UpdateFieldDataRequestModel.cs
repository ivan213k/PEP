using PerformanceEvaluationPlatform.Application.Model.FormsData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PerformanceEvaluationPlatform.Models.FormData.RequestModels
{
    public class UpdateFieldDataRequestModel
    {
        [Required]
        public int FieldId { get; set; }
        [Required]
        public int AssesmentId { get; set; }
        public string Comment { get; set; }
    }


    public static partial class ViewModelMapperExtensions
    {
        public static IList<UpdateFieldDataDto> AsDto(this IList<UpdateFieldDataRequestModel> requestModel)
        {
            IList<UpdateFieldDataDto> updateFieldDataDtoList = new List<UpdateFieldDataDto> { };

            foreach (var item in requestModel)
            {
                var updateFieldDataDto = new UpdateFieldDataDto
                {
                    Comment = item.Comment,
                    FieldId = item.FieldId,
                    AssesmentId = item.AssesmentId,
                };
                updateFieldDataDtoList.Add(updateFieldDataDto);
            }
            return updateFieldDataDtoList;
        }
    }
}
