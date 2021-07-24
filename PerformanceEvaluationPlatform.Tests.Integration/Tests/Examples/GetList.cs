using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Examples
{
    [TestFixture]
    public class GetList : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("examples")
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            ICollection<ExampleListItemViewModel> content = JsonConvert.DeserializeObject<ICollection<ExampleListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_state()
        {
	        //Arrange
	        HttpRequestMessage request = BaseAddress
		        .AppendPathSegment("examples")
		        .SetQueryParams(new ExampleListFilterRequestModel {
                    StateId = 2
		        })
		        .WithHttpMethod(HttpMethod.Get);

	        //Act
	        HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<ExampleListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
	        Assert.AreEqual(1, content.Count);
        }
    }
}
