using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure;

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

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
