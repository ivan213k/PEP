using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormData.Integration
{
    [TestFixture]
    class UpdateDetails : IntegrationTestBase
    {
        [Test]
        public async Task Update_WhenFillForm_ShouldChangeState()
        {
            var item = BaseAddress
                .AppendPathSegment("forms")
                .AppendPathSegment(2)
                .WithHttpMethod(HttpMethod.Put);

            var formData = BaseAddress
                .AppendPathSegment("forms")
                .AppendPathSegment(2)
                .WithHttpMethod(HttpMethod.Get);

            await SendRequest(item);

            HttpResponseMessage responseFormData = await SendRequest(formData);
            var content = JsonConvert.DeserializeObject<FormDataDetailViewModel>(await responseFormData.Content.ReadAsStringAsync());

            Assert.That(content.StateId, Is.EqualTo(2));
        }

        [Test]
        public async Task Update_NotExistingId_ReturnNotFound()
        {
            var item = BaseAddress
                .AppendPathSegment("forms")
                .AppendPathSegment("wrong path")
                .WithHttpMethod(HttpMethod.Put);

            HttpResponseMessage response = await SendRequest(item);

            CustomAssert.IsNotFound(response);
        }
    }
}
