using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Survey.RequestModels;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Surveys
{
    [TestFixture]
    public class CreateSurvey : IntegrationTestBase
    {
        private CreateSurveyRequestModel createSurveyRequestModel;

        public CreateSurvey()
        {
            createSurveyRequestModel = new CreateSurveyRequestModel
            {
                FormId = 1,
                SupervisorId = 1,
                AssigneeId = 2,
                RecommendedLevelId = 3,
                AppointmentDate = new DateTime(2021, 12, 07),
                AssignedUserIds = new List<int> { 2, 3 }
            };
        }

        [Test]
        public async Task Shoud_return_bad_request_when_create_survey_request_model_is_null() 
        {
            //Arrange
            CreateSurveyRequestModel createSurveyRequestModel = null;
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_form_template_does_not_exist()
        {
            //Arrange
            createSurveyRequestModel.FormId = 10000;
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_assignee_does_not_exist()
        {
            //Arrange
            createSurveyRequestModel.AssigneeId = 10000;
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_supervisor_does_not_exist()
        {
            //Arrange
            createSurveyRequestModel.SupervisorId = 10000;
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_level_does_not_exist()
        {
            //Arrange
            createSurveyRequestModel.RecommendedLevelId = 10000;
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_assigned_user_ids_contain_the_same_values()
        {
            //Arrange
            createSurveyRequestModel.AssignedUserIds = new List<int> { 2, 2, 3 };
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_assigned_user_does_not_exist()
        {
            //Arrange
            createSurveyRequestModel.AssignedUserIds = new List<int> { 2, 3, 10000 };
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_appointment_date_in_the_past()
        {
            //Arrange
            createSurveyRequestModel.AppointmentDate = DateTime.Now.AddYears(-1);
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_create_survey_when_request_model_is_valid()
        {
            //Arrange
            var request = CreatePostHttpRequest("surveys", createSurveyRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var idViewModel = await response.Content.DeserializeAsAsync<IdViewModel>();
            var survey = await GetSurveyById(idViewModel.Id);

            Assert.IsNotNull(survey);
        }

        private async Task<SurveyDetailsViewModel> GetSurveyById(int surveyId)
        {
            var request = CreateGetHttpRequest("surveys", surveyId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<SurveyDetailsViewModel>();
        }
    }
}
