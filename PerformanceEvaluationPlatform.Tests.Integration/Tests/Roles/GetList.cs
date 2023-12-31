﻿using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Roles
{
    [TestFixture]
    class GetList : IntegrationTestBase
    {
        private HttpRequestMessage CreateGetHttpRequest(object requestModel)
        {
            return BaseAddress
                            .AppendPathSegment("roles")
                            .SetQueryParams(requestModel)
                            .WithHttpMethod(HttpMethod.Get);
        }
        private HttpRequestMessage CreateGetHttpRequest()
        {
            return BaseAddress
                            .AppendPathSegment("roles")
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

            var content = await response.Content.DeserializeAsAsync<ICollection<RoleListItemViewModel>>();

            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_title_ascending()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                TitleSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<RoleListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].Title, "Backend");
            Assert.AreEqual(content[1].Title, "Frontend");
            Assert.AreEqual(content[2].Title, "QA");
            Assert.AreEqual(content[3].Title, "Team Lead");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_title_descending()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                TitleSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<RoleListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].Title, "Test");
            Assert.AreEqual(content[1].Title, "Team Lead");
            Assert.AreEqual(content[2].Title, "QA");
            Assert.AreEqual(content[3].Title, "Frontend");
            Assert.AreEqual(content[4].Title, "Backend");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_IsPrimary_ascending()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                IsPrimarySortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<RoleListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
            if (content.Count == 4)
            {
                Assert.AreEqual(content[0].Title, "Team Lead");
            }
            else
            {
                Assert.AreEqual(content[0].Title, "Test");
            }
           
             
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_IsPrimary_descending()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                IsPrimarySortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<RoleListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(5, content.Count);
            Assert.AreEqual(content[0].Title, "Backend");
            Assert.AreEqual(content[1].Title, "Frontend");
            Assert.AreEqual(content[2].Title, "QA");
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_isPrimary()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                IsPrimary = true
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<RoleListItemViewModel>>();

            Assert.NotNull(content);
            Assert.IsTrue(content.Count == 4 || content.Count == 5);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_usersCountFrom()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                UsersCountFrom = 1
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<RoleListItemViewModel>>();

            Assert.NotNull(content);

            if (content.Count > 0)
            {
                foreach (RoleListItemViewModel contentElement in content)
                {
                    Assert.IsTrue(contentElement.UsersCount >= 1);
                }
            }
            else
            {
                Assert.IsTrue(content.Count == 0);
            }
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_usersCountTo()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                UsersCountTo = 3
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<RoleListItemViewModel>>();

            if (content.Count > 0)
            {
                foreach (RoleListItemViewModel contentElement in content)
                {
                    Assert.IsTrue(contentElement.UsersCount <= 3);
                }
            }
            else
            {
                Assert.IsTrue(content.Count == 0);
            }
        }

        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_Title()
        {
            //Arrange
            var requestModel = new RoleListFilterRequestModel
            {
                Search = "Frontend"
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<RoleListItemViewModel>>();

            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }
    }
}
