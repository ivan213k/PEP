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
    class CreateNewDeeplink : IntegrationTestBase
    {
        private CreateDeeplinkRequestModel _createRequestModel;

        public CreateNewDeeplink()
        {
            _createRequestModel = new CreateDeeplinkRequestModel
            {
                SurveyId = 3,
                ExpiresDate = new DateTime(2021,10,25),
                SentById = 3,
                UserId = 2
            };
        }

        [Test]
        public async Task Shoud_return_bad_request_when_create_deeplink_request_model_is_null()
        {
            //Arrange
            CreateDeeplinkRequestModel _createRequestModel = null;
            var request = CreatePostHttpRequest("deeplinks", _createRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }


        [Test]
        public async Task Shoud_return_bad_request_when_User_does_not_exist()
        {
            //Arrange
            _createRequestModel.UserId = 10000;
            var request = CreatePostHttpRequest("deeplinks", _createRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }


        [Test]
        public async Task Shoud_return_bad_request_when_survey_does_not_exist()
        {
            //Arrange
            _createRequestModel.SurveyId = 10000;
            var request = CreatePostHttpRequest("deeplinks", _createRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_UserTo_and_UserBy_contain_the_same_Users()
        {
            //Arrange
            _createRequestModel.SentById = _createRequestModel.UserId;
            var request = CreatePostHttpRequest("surveys", _createRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }





        [Test]
        public async Task Shoud_create_deeplink_when_request_model_is_valid()
        {
            //Arrange
            var request = CreatePostHttpRequest("deeplinks", _createRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var idViewModel = await response.Content.DeserializeAsAsync<IdViewModel>();
            var deeplink = await GetDeeplinkById(idViewModel.Id);

            Assert.IsNotNull(deeplink);
        }

        private async Task<DeeplinkDetailsViewModel> GetDeeplinkById(int deeplinkId)
        {
            var request = CreateGetHttpRequest("deeplinks", deeplinkId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<DeeplinkDetailsViewModel>();
        }
    }
}
