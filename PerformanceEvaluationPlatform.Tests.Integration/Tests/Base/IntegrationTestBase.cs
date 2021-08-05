using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json;
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

        protected Url BaseAddress => _client.BaseAddress;

        protected async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response = await _client.SendAsync(request);

            return response;
        }

        protected HttpRequestMessage CreatePutHttpRequest(params object[] segmentPaths)
        {
            var url = AppendPathSegments(segmentPaths);
            return url.WithHttpMethod(HttpMethod.Put);
        }

        protected HttpRequestMessage CreatePutHttpRequest(object requestModel, string segmentPath, int entityId)
        {
            var url = AppendPathSegments(segmentPath, entityId);

            var requestMessage = url.WithHttpMethod(HttpMethod.Put);
            requestMessage.Content = CreateStringContent(requestModel);

            return requestMessage;
        }

        protected HttpRequestMessage CreateGetHttpRequest(params object[] segmentPaths)
        {
            var url = AppendPathSegments(segmentPaths);
            return url.WithHttpMethod(HttpMethod.Get);
        }

        protected HttpRequestMessage CreatePostHttpRequest(string segmentPath, object requestModel) 
        {
            var requestMessage = BaseAddress.AppendPathSegment(segmentPath).WithHttpMethod(HttpMethod.Post);
            requestMessage.Content = CreateStringContent(requestModel);
            return requestMessage;
        }
        protected HttpRequestMessage CreatePostHttpRequest(string segmentPath)
        {
            var requestMessage = BaseAddress.AppendPathSegment(segmentPath).WithHttpMethod(HttpMethod.Post);
            return requestMessage;
        }

        private Url AppendPathSegments(params object[] segmentPaths)
        {
            Url url = BaseAddress;
            foreach (var segmentPath in segmentPaths)
            {
                url.AppendPathSegment(segmentPath);
            }

            return url;
        }

        private static StringContent CreateStringContent(object requestModel)
        {
            var json = JsonConvert.SerializeObject(requestModel);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
