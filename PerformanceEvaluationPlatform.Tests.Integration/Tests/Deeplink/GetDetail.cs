using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Deeplink.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Deeplink
{

    [TestFixture]
    public class GetDetail : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("deeplinks")
                .AppendPathSegment(10000)
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_valid_data_requested()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("deeplinks")
                .AppendPathSegment(1)
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);


            var content = JsonConvert.DeserializeObject<DeeplinkDetailsViewModel>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(content.Id, 1);
            Assert.AreEqual(content.SentTo, "Kiril Krigan");
            Assert.AreEqual(content.SentBy, "Artur Grugon");
            Assert.AreEqual(content.FormTemplateName, "Middle Back-End Dev");
            Assert.AreEqual(content.ExpiresAt, new DateTime(2019,11,25));
        }
    }
}
