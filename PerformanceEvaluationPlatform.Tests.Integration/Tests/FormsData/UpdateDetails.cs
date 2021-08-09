using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormData
{
    [TestFixture]
    class UpdateDetails : IntegrationTestBase
    {
        [Test]
        public async Task Update_WhenFillForm_ShouldChangeState()
        {
            var formDataId = 2;
            var request = CreatePutHttpRequest(requestModel: GetFieldDataRequestModel(), "forms", formDataId);

            var formData = BaseAddress
                .AppendPathSegment("forms")
                .AppendPathSegment(2)
                .WithHttpMethod(HttpMethod.Get);

            await SendRequest(request);

            HttpResponseMessage responseFormData = await SendRequest(formData);
            var content = JsonConvert.DeserializeObject<FormDataDetailViewModel>(await responseFormData.Content.ReadAsStringAsync());

            Assert.That(content.StateId, Is.EqualTo(2));
        }

        [Test]
        public async Task Return_not_found_when_wrong_id()
        {
            var formDataId = 800;
            var request = CreatePutHttpRequest(requestModel: GetFieldDataRequestModel(), "forms", formDataId);

            var response = await SendRequest(request);

            CustomAssert.IsNotFound(response);
        }


        private static IList<UpdateFieldDataRequestModel> GetFieldDataRequestModel()
        {
            var updateFieldDataRequestModel =  new List<UpdateFieldDataRequestModel>
            {
                new UpdateFieldDataRequestModel
                {
                    FieldId = 1,
                    AssesmentId = 1,
                    Comment = "test comment",
                },
                 new UpdateFieldDataRequestModel
                 {
                    FieldId = 2,
                    AssesmentId = 2,
                    Comment = "my comment",
                 }
             };
            return updateFieldDataRequestModel;
        }
    }
}
