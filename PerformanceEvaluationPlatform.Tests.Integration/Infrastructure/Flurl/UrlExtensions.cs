using System;
using System.Net.Http;
using Flurl;

namespace PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl
{
	public static class UrlExtensions
	{
		public static HttpRequestMessage WithHttpMethod(
			this Url url,
			HttpMethod httpMethod)
		{
			return new HttpRequestMessage()
			{
				RequestUri = new Uri((string)url),
				Method = httpMethod
			};
		}
	}
}
