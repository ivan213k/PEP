using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.User.RequestModels;
using PerformanceEvaluationPlatform.Models.User.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Users
{
    [TestFixture]
    class GetUsers : IntegrationTestBase
    {

        private HttpRequestMessage GetArrangeWithFilteredParams(UserFilterRequestModel filterModel)
        {
            var users = BaseAddress
                .AppendPathSegment("users")
                .SetQueryParams(filterModel)
                .WithHttpMethod(HttpMethod.Get);
            return users;
                    }
        private HttpRequestMessage GetArrangeWithSortedParams(UserSortingRequestModel sortingModel)
        {
            var users = BaseAddress
                .AppendPathSegment("users")
                .SetQueryParams(sortingModel)
                .WithHttpMethod(HttpMethod.Get);
            return users;
        }

        [Test]
        public async Task Get_WhenCalled_ReturnValidItems()
        {
            //Arrange
            var items = BaseAddress
                .AppendPathSegment("users")
                .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(items);

            //Assert

            CustomAssert.IsSuccess(response);
            var content = JsonConvert.DeserializeObject<ICollection<UserViewModel>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(content);
            Assert.AreEqual(content.Count,2);
        }

        [Test]
        public async Task Get_FilteredByEmail_ReturnValidFilteredItems()
        {
            //Arrange
            var items = GetArrangeWithFilteredParams(new UserFilterRequestModel() { EmailOrName = "k" });

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Get_FilteredByStateIds_ReturnValidFilteredItems()
        {
            //Arrange
            var filter = new UserFilterRequestModel() { StateIds = new[] { 1 } };
            var items = GetArrangeWithFilteredParams(filter);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(filter.Take));
        }


        [Test]
        public async Task Get_FilteredByRoleIds_ReturnValidFilteredItems()
        {
            //Arrange
            var filter = new UserFilterRequestModel() { RoleIds = new[] { 1 } };
            var items = GetArrangeWithFilteredParams(filter);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Get_FilteredByPreviousPEDate_ReturnValidFilteredItems()
        {
            //Arrange
            var filter = new UserFilterRequestModel() { PreviousPEDate = new DateTime(2021, 07, 10) };
            var items = GetArrangeWithFilteredParams(filter);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Get_FilteredByNextPEDate_ReturnValidFilteredItems()
        {
            //Arrange
            var filter = new UserFilterRequestModel() { NextPEDate = new DateTime(2021, 07, 12) };
            var items = GetArrangeWithFilteredParams(filter);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Get_SortingbyUserNameAsc_ReturnValidSortedItems()
        {
            //Arrange
            var sorting = new UserSortingRequestModel() { UserName = 1 };
            var items = GetArrangeWithSortedParams(sorting);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(content[0].FirstName, Does.StartWith("A").IgnoreCase);
        }


        [Test]
        public async Task Get_SortingbyUserPreviousPEDesc_ReturnValidSortedItems()
        {
            //Arrange
            var sorting = new UserSortingRequestModel() { UserPreviousPE =2};
            var items = GetArrangeWithSortedParams(sorting);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(content[0].Id, Is.EqualTo(2));
        }


        [Test]
        public async Task Get_SortingbyUserNextPEAsc_ReturnValidSortedItems()
        {
            //Arrange
            var sorting = new UserSortingRequestModel() { UserNextPE=1};
            var items = GetArrangeWithSortedParams(sorting);

            //Act
            HttpResponseMessage result = await SendRequest(items);

            //Assert

            var content = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(content[0].Id, Is.EqualTo(1));
        }

    }
}
