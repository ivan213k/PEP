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
    [TestFixture]
    public class GetList : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("deeplinks")
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            ICollection<DeeplinkListItemViewModel> content = JsonConvert.DeserializeObject<ICollection<DeeplinkListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
      
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_ExpiresAtTo()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("deeplinks")
                .SetQueryParams(new DeeplinkListFilterRequestModel
                {
                    ExpiresAtTo = new DateTime(2020,1,1)
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<DeeplinkListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }
        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_ExpiresAt_ascending()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("deeplinks")
                .SetQueryParams(new DeeplinkListFilterRequestModel
                {
                    OrderExpiresAt = Models.Shared.Enums.SortOrder.Ascending
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);
 

            var content = await response.Content.DeserializeAsAsync<IList<DeeplinkListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].SentTo, "Artur Grugon");
            Assert.AreEqual(content[1].SentTo, "Kiril Krigan");
            Assert.AreEqual(content[2].SentTo, "Kristina Lavruk");
        }
    }
}
