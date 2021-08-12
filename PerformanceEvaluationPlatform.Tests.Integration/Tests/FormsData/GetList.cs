using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormData
{
    [TestFixture]
    public class GetList : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            ICollection<FormDataListItemViewModel> content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_state()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    StateId = 3
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_assignee()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    AssigneeIds = new List<int> { 1, 2, 3 }
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_reviewers()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    ReviewersIds = new List<int> { 1, 2 }
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_from()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    AppointmentDateFrom = new DateTime(2021, 11, 7)
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_to()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    AppointmentDateTo = new DateTime(2021, 12, 7)
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_form_name()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    Search = "Middle Back-End Dev"
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_searching_by_assignee()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    Search = "Artur Grugon"
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = JsonConvert.DeserializeObject<ICollection<FormDataListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }
    }
}
