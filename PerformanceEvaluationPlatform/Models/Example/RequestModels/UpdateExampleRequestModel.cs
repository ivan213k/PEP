using System.ComponentModel.DataAnnotations;
using PerformanceEvaluationPlatform.Application.Model.Examples;

namespace PerformanceEvaluationPlatform.Models.Example.RequestModels
{
    public class UpdateExampleRequestModel
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int StateId { get; set; }
    }

    public static partial class ViewModelMapperExtensions 
    {
	    public static UpdateExampleDto AsDto(this UpdateExampleRequestModel requestModel) {
		    return new UpdateExampleDto {
			    Title = requestModel.Title,
			    TypeId = requestModel.TypeId,
			    StateId = requestModel.StateId
		    };
	    }
    }
}
