using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Surveys
{
    [TestFixture]
    public class GetStates : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            var request = CreateGetHttpRequest("surveys", "states");

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            var content = await response.Content.DeserializeAsAsync<IList<SurveyStateListItemViewModel>>();

            CustomAssert.IsSuccess(response);
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);

            Assert.AreEqual(1, content[0].Id);
            Assert.AreEqual("Draft", content[0].Name);

            Assert.AreEqual(2, content[1].Id);
            Assert.AreEqual("Ready", content[1].Name);

            Assert.AreEqual(3, content[2].Id);
            Assert.AreEqual("Sent", content[2].Name);

            Assert.AreEqual(4, content[3].Id);
            Assert.AreEqual("Ready for review", content[3].Name);

            Assert.AreEqual(5, content[4].Id);
            Assert.AreEqual("Archived", content[4].Name);
        }
    }
}
