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
    [TestFixture, Order(1)]
    public class GetList : IntegrationTestBase
    {
        private HttpRequestMessage CreateGetHttpRequest(object requestModel)
        {
            return BaseAddress
                            .AppendPathSegment("surveys")
                            .SetQueryParams(requestModel)
                            .WithHttpMethod(HttpMethod.Get);
        }

        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            var request = CreateGetHttpRequest("surveys");

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
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
                AssigneeIds = new List<int> { 2 }
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(4, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_supervisor_user()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                SupervisorIds = new List<int> { 1 }
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_from()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AppointmentDateFrom = new DateTime(2021, 11, 7)
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<SurveyListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(4, content.Count);
        }
        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_to()
        {
            //Arrange
            var requestModel = new SurveyListFilterRequestModel
            {
                AppointmentDateTo = new DateTime(2021, 11, 7)
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
                Search = "Middle Back-End Dev"
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
                Search = "Kristina Lavruk"
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
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].FormName, "Junior Front-End Dev");
            Assert.AreEqual(content[1].FormName, "Junior Front-End Dev");
            Assert.AreEqual(content[2].FormName, "Junior Front-End Dev");
            Assert.AreEqual(content[3].FormName, "Middle Back-End Dev");
            Assert.AreEqual(content[4].FormName, "Middle Front-End Dev");
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
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].FormName, "Middle Front-End Dev");
            Assert.AreEqual(content[1].FormName, "Middle Back-End Dev");
            Assert.AreEqual(content[2].FormName, "Junior Front-End Dev");
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
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].Assignee, "Kiril Krigan");
            Assert.AreEqual(content[1].Assignee, "Kiril Krigan");
            Assert.AreEqual(content[2].Assignee, "Kiril Krigan");
            Assert.AreEqual(content[3].Assignee, "Kiril Krigan");
            Assert.AreEqual(content[4].Assignee, "Kristina Lavruk");
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
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].Assignee, "Kristina Lavruk");
            Assert.AreEqual(content[1].Assignee, "Kiril Krigan");
            Assert.AreEqual(content[2].Assignee, "Kiril Krigan");
        }
    }
}
