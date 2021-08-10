using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Deeplink.RequestModels;

using PerformanceEvaluationPlatform.Models.Deeplink.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Deeplink
{
    class GetStates : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_data_requested()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("deeplinks")
                .AppendPathSegment("states")
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);


            var content = await response.Content.DeserializeAsAsync<IList<DeeplinkStateListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(content.Count, 2);
            Assert.AreEqual(content[0].Id, 1);
            Assert.AreEqual(content[0].Name, "Draft");
            Assert.AreEqual(content[1].Id, 2);
            Assert.AreEqual(content[1].Name, "Expired");

        }
    }
}
