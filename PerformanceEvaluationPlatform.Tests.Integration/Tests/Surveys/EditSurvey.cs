using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Survey.RequestModels;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Surveys
{
    [TestFixture]
    public class EditSurvey : IntegrationTestBase
    {

        [Test]
        public async Task Shoud_return_bad_request_when_edit_survey_request_model_is_null() 
        {
            //Arrange
            var surveyId = 5;
            var request = CreatePutHttpRequest(requestModel: null, "surveys", surveyId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_level_does_not_exist()
        {
            //Arrange
            var editSurveyRequestModel = new EditSurveyRequestModel
            {
                RecommendedLevelId = 10000,
                AppointmentDate = new DateTime(2021, 12, 09),
                Summary = "test summary"
            };
            var surveyId = 5;
            var request = CreatePutHttpRequest(requestModel: editSurveyRequestModel, "surveys", surveyId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_appointment_date_is_in_the_past()
        {
            //Arrange
            var editSurveyRequestModel = new EditSurveyRequestModel
            {
                RecommendedLevelId = 1,
                AppointmentDate = DateTime.Now.AddDays(-1),
            };
            var surveyId = 5;
            var request = CreatePutHttpRequest(requestModel: editSurveyRequestModel, "surveys", surveyId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            var editSurveyRequestModel = new EditSurveyRequestModel
            {
                RecommendedLevelId = 1,
                AppointmentDate = new DateTime(2021, 12, 09),
            };
            var surveyId = 1000;
            var request = CreatePutHttpRequest(requestModel: editSurveyRequestModel, "surveys", surveyId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Shoud_update_survey_when_request_model_is_valid()
        {
            //Arrange
            var editSurveyRequestModel = new EditSurveyRequestModel
            {
                RecommendedLevelId = 1,
                AppointmentDate = new DateTime(2021, 12, 09),
            };
            var surveyId = 5;
            var request = CreatePutHttpRequest(requestModel: editSurveyRequestModel, "surveys", surveyId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);
            var survey = await GetSurveyById(surveyId);
            Assert.NotNull(survey);
            Assert.AreEqual(editSurveyRequestModel.AppointmentDate, survey.AppointmentDate);
            Assert.AreEqual(editSurveyRequestModel.RecommendedLevelId, survey.RecommendedLevelId);
            Assert.AreEqual(editSurveyRequestModel.Summary, survey.Summary);
        }

        private async Task<SurveyDetailsViewModel> GetSurveyById(int surveyId)
        {
            var request = CreateGetHttpRequest("surveys", surveyId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<SurveyDetailsViewModel>();
        }
    }
}
