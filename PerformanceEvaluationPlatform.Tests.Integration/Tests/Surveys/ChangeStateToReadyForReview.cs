using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Net;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Surveys
{
    [TestFixture]
    public class ChangeStateToReadyForReview : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            var surveyId = 10000;
            var request = CreatePutHttpRequest("surveys", surveyId, "readyForReview");

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_unprocessable_entity_when_survey_not_in_sent_state()
        {
            //Arrange
            var surveyId = 4;
            var request = CreatePutHttpRequest("surveys", surveyId, "readyForReview");

            //Act
            var response = await SendRequest(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Test]
        public async Task Request_should_change_state_to_ready_for_review() 
        {
            //Arrange
            var surveyId = 3;
            var request = CreatePutHttpRequest("surveys", surveyId, "readyForReview");

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);
            var survey = await GetSurveyById(surveyId);
            Assert.AreEqual("Ready for review", survey.State);
        }

        private async Task<SurveyDetailsViewModel> GetSurveyById(int surveyId)
        {
            var request = CreateGetHttpRequest("surveys", surveyId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<SurveyDetailsViewModel>();
        }
    }
}
