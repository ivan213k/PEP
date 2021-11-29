using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.User.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Users
{
    [TestFixture]
    class GetDetails : IntegrationTestBase
    {
        [Test]
        public async Task GetUser_SendId_ReturnValidObject()
        {
            // Create user with id =2
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(2)
                .WithHttpMethod(HttpMethod.Get);

            HttpResponseMessage result = await SendRequest(request);

            var content = JsonConvert.DeserializeObject<UserDetailViewModel>(await result.Content.ReadAsStringAsync());

            Assert.That(content, Is.Not.Null);
        }

        [Test]
        public async Task GetUser_SendInvalidId_ReturnNotFound()
        {
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(42)
                .WithHttpMethod(HttpMethod.Get);

            HttpResponseMessage result = await SendRequest(request);


            CustomAssert.IsNotFound(result);
        }
    }
}
