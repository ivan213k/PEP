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
    class ActivateUser:IntegrationTestBase
    {
        [Test]
        public async Task ActivateUser_WhenCalled_ShouldChangeUserState()
        {
            var item = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(3)
                .AppendPathSegment("activate")
                .WithHttpMethod(HttpMethod.Put);

            var user = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(3)
                .WithHttpMethod(HttpMethod.Get);

             await SendRequest(item);

            HttpResponseMessage responseUser = await SendRequest(user);
            var content = JsonConvert.DeserializeObject<UserDetailViewModel>(await responseUser.Content.ReadAsStringAsync());

            Assert.That(content.StateName, Is.EqualTo("Active"));
        }

        [Test]
        public async Task ActivateUser_NotExistingId_ReturnNOtFound()
        {
            var item = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(100)
                .AppendPathSegment("activate")
                .WithHttpMethod(HttpMethod.Put);

            HttpResponseMessage response = await SendRequest(item);

            CustomAssert.IsNotFound(response);
        }
    }
}
