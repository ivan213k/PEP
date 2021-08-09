using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormData
{
    [TestFixture]
    public class GetStates : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
	            .AppendPathSegment("forms")
	            .AppendPathSegment("states")
	            .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            var content = JsonConvert.DeserializeObject<IList<FormDataStateListItemViewModel>>(await response.Content.ReadAsStringAsync());

            CustomAssert.IsSuccess(response);
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);

            Assert.AreEqual(1, content[0].Id);
            Assert.AreEqual("Draft", content[0].Name);

            Assert.AreEqual(2, content[1].Id);
            Assert.AreEqual("InProgress", content[1].Name);

            Assert.AreEqual(3, content[2].Id);
            Assert.AreEqual("Submitted", content[2].Name);
        }
    }
}
