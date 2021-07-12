using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Examples
{
    [TestFixture]
    public class GetTypes : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
	            .AppendPathSegment("examples")
	            .AppendPathSegment("types")
	            .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            var content = JsonConvert.DeserializeObject<IList<ExampleTypeListItemViewModel>>(await response.Content.ReadAsStringAsync());

            CustomAssert.IsSuccess(response);
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);

            Assert.AreEqual(1, content[0].Id);
            Assert.AreEqual("Type 1", content[0].Name);

            Assert.AreEqual(2, content[1].Id);
            Assert.AreEqual("Type 2", content[1].Name);
        }
    }
}
