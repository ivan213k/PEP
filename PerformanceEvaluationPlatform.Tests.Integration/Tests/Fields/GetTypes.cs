using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Fields
{
    [TestFixture]
    public class GetTypesList : IntegrationTestBase
    {
        private HttpRequestMessage CreateGetHttpRequest(object requestModel)
        {
            return BaseAddress
                            .AppendPathSegment("fields/types")
                            .SetQueryParams(requestModel)
                            .WithHttpMethod(HttpMethod.Get);
        }
        private HttpRequestMessage CreateGetHttpRequest()
        {
            return BaseAddress
                            .AppendPathSegment("fields/types")
                            .WithHttpMethod(HttpMethod.Get);
        }
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            var request = CreateGetHttpRequest();

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<FieldTypeListItemViewModel>>();

            CustomAssert.IsSuccess(response);
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);

            Assert.AreEqual(1, content[0].Id);
            Assert.AreEqual("Divider", content[0].Name);

            Assert.AreEqual(2, content[1].Id);
            Assert.AreEqual("Dropdown with comment", content[1].Name);
        }
    }
}
