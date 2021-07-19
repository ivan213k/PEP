using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Models.Survey.RequestModels;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Surveys
{
    [TestFixture]
    public class GetList : IntegrationTestBase
    {
        private HttpRequestMessage CreateGetHttpRequest(object requestModel)
        {
            return BaseAddress
                            .AppendPathSegment("surveys")
                            .SetQueryParams(requestModel)
                            .WithHttpMethod(HttpMethod.Get);
        }
        private HttpRequestMessage CreateGetHttpRequest()
        {
            return BaseAddress
                            .AppendPathSegment("surveys")
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

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_state()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                StateIds = new List<int> { 2 }
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_assignee_users()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AssigneeIds = new List<int> { 2, 3 }
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_supervisor_user()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                SupervisorIds = new List<int> { 2 }
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_from()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AppointmentDateFrom = new DateTime(2021, 7, 11)
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }
        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_to()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AppointmentDateTo = new DateTime(2021, 7, 11)
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }
        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_form_name()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                Search = "Manual QA"
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_assignee()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                Search = "Test User 1"
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_form_name_ascending()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                FormNameSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].FormName, ".NET");
            Assert.AreEqual(content[1].FormName, "JS");
            Assert.AreEqual(content[2].FormName, "Manual QA");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_form_name_descending()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                FormNameSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].FormName, "Manual QA");
            Assert.AreEqual(content[1].FormName, "JS");
            Assert.AreEqual(content[2].FormName, ".NET");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_assignee_ascending()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AssigneeSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].Assignee, "Test User");
            Assert.AreEqual(content[1].Assignee, "Test User 1");
            Assert.AreEqual(content[2].Assignee, "Test User 2");
        }
        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_assignee_descending()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AssigneeSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].Assignee, "Test User 2");
            Assert.AreEqual(content[1].Assignee, "Test User 1");
            Assert.AreEqual(content[2].Assignee, "Test User");
        }
    }
}
