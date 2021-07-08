using System.Net.Http;

namespace PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert
{
	public partial class CustomAssert
	{
		public static void IsBadRequest(HttpResponseMessage response)
		{
			NUnit.Framework.Assert.AreEqual(400, (int) response.StatusCode);
		}

		public static void IsConflict(HttpResponseMessage response)
		{
			NUnit.Framework.Assert.AreEqual(409, (int) response.StatusCode);
		}

		public static void IsSuccess(HttpResponseMessage response)
		{
			NUnit.Framework.Assert.True(response.IsSuccessStatusCode);
		}

		public static void IsUnauthorized(HttpResponseMessage response)
		{
			NUnit.Framework.Assert.AreEqual(401, (int) response.StatusCode);
		}

		public static void IsNotFound(HttpResponseMessage response, string propertyName = null)
		{
			NUnit.Framework.Assert.AreEqual(404, (int) response.StatusCode);
		}

		public static void IsForbidden(HttpResponseMessage response)
		{
			NUnit.Framework.Assert.AreEqual(403, (int) response.StatusCode);
		}
    }
}
