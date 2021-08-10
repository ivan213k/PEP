using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Deeplink.RequestModels;

using PerformanceEvaluationPlatform.Models.Deeplink.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;


namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Deeplink
{
    [TestFixture]
    class UpdateDeeplink : IntegrationTestBase
    {


        [Test]
        public async Task Should_return_bad_request_when_update_deeplink_request_model_is_null()
        {
            //Arrange
            var deeplinkId = 2;
            var request = CreatePutHttpRequest(requestModel: null, "deeplinks", deeplinkId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Should_return_bad_request_when_expires_date_is_in_the_past()
        {
            //Arrange
            var updateDeeplinkRequestModel = new UpdateDeeplinkRequestModel
            {
                ExpiresAt = DateTime.Now.AddDays(-1),
            };
            var deeplinkId = 2;
            var request = CreatePutHttpRequest(requestModel: updateDeeplinkRequestModel, "deeplinks", deeplinkId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Should_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            var updateDeeplinkRequestModel = new UpdateDeeplinkRequestModel
            {

                ExpiresAt = new DateTime(2021, 12, 09),
            };
            var deeplinkId = 1000;
            var request = CreatePutHttpRequest(requestModel: updateDeeplinkRequestModel, "deeplinks", deeplinkId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Should_update_deeplink_when_request_model_is_valid()
        {
            //Arrange
            var updateDeeplinkRequestModel = new UpdateDeeplinkRequestModel
            {
               ExpiresAt = new DateTime(2021, 11, 24),
            };
            var deeplinkId = 2;
            var request = CreatePutHttpRequest(requestModel: updateDeeplinkRequestModel, "deeplinks", deeplinkId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);
            var deeplink = await GetDeeplinkById(deeplinkId);
            Assert.NotNull(deeplink);
            Assert.AreEqual(updateDeeplinkRequestModel.ExpiresAt, deeplink.ExpiresAt);
        }

        private async Task<DeeplinkDetailsViewModel> GetDeeplinkById(int deeplinkId)
        {
            var request = CreateGetHttpRequest("deeplinks", deeplinkId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<DeeplinkDetailsViewModel>();
        }
    }
}
