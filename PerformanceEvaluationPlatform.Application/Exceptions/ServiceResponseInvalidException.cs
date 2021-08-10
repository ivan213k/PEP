using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Application
{
	class ServiceResponseInvalidException : Exception
	{
		public override string Message => "Service response is invalid. Check IsValid() property prior accessing to Payload, please.";
	}
}
