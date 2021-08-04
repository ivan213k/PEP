using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Base
{
    [Category("Acceptance")]
    public abstract class IntegrationTestBase : IDisposable
    {
        private readonly CustomWebApplicationFactory<PerformanceEvaluationPlatform.Startup> _factory;
        private readonly HttpClient _client;

        protected IntegrationTestBase()
        {
            _factory = new CustomWebApplicationFactory<PerformanceEvaluationPlatform.Startup>();
            _client = _factory.CreateClient();
        }

        protected Flurl.Url BaseAddress => _client.BaseAddress;

        protected async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response = await _client.SendAsync(request);

            return response;
        }

        protected HttpRequestMessage CreatePutHttpRequest(params object[] segmentPaths)
        {
            Url url = BaseAddress;
            foreach (var segmentPath in segmentPaths)
            {
                url.AppendPathSegment(segmentPath);
            }
            return url.WithHttpMethod(HttpMethod.Put);
        }

        protected HttpRequestMessage CreateGetHttpRequest(params object[] segmentPaths)
        {
            Url url = BaseAddress;
            foreach (var segmentPath in segmentPaths)
            {
                url.AppendPathSegment(segmentPath);
            }
            return url.WithHttpMethod(HttpMethod.Get);
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
