using System;
using System.Net.Http;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.Models.Project.RequestModels;
using PerformanceEvaluationPlatform.Models.Project.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Projects
{
    [TestFixture]
    class GetList : IntegrationTestBase
    {
        private HttpRequestMessage CreateGetHttpRequest(object requestModel)
        {
            return BaseAddress
                            .AppendPathSegment("projects")
                            .SetQueryParams(requestModel)
                            .WithHttpMethod(HttpMethod.Get);
        }
        private HttpRequestMessage CreateGetHttpRequest()
        {
            return BaseAddress
                            .AppendPathSegment("projects")
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

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();

            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_title_ascending()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                TitleSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<ProjectListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].Title, "Project 1");
            Assert.AreEqual(content[1].Title, "Project 2");
            Assert.AreEqual(content[2].Title, "Project 3");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_title_descending()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                TitleSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<ProjectListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual(content[0].Title, "Project 3");
            Assert.AreEqual(content[1].Title, "Project 2");
            Assert.AreEqual(content[2].Title, "Project 1");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_Start_date_ascending()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                StartDateSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_Start_date_descending()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                StartDateSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_Coordinator_ascending()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                CoordinatorSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_Coordinator_descending()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                CoordinatorSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_Title()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                Search = "Project 1"
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();

            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_Coordinator()
        {
            //Arrange
            var requestModel = new ProjectListFilterRequestModel
            {
                CoordinatorIds = new List<int> { 1, 2, 3 }
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<ProjectListItemViewModel>>();

            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }
    }
}
