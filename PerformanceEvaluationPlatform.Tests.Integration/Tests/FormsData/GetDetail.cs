﻿using System.Net.Http;
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
    public class GetDetail : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_not_found_when_wrong_path_is_requested()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .AppendPathSegment(100)
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .AppendPathSegment(1)
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<FormDataDetailViewModel>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
        }
    }
}
