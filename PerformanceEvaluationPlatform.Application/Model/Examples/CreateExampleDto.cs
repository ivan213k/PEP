using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Application.Model.Examples
{
    public class CreateExampleDto
    {
	    public string Title { get; set; }
	    public int TypeId { get; set; }
	    public int StateId { get; set; }
    }
}
