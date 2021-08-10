using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Controllers
{

    public class BaseController : ControllerBase
    {
	    protected bool TryGetErrorResult(ServiceResponse serviceResponse, out IActionResult errorResult)
        {
	        errorResult = null;

	        if (serviceResponse.IsValid)
	        {
		        return false;
	        }
	        else
	        {
		        errorResult = new ObjectResult(serviceResponse.Errors)
		        {
			        StatusCode = serviceResponse.StatusCode
		        };
		        return true;
	        }
        }
    }
}
