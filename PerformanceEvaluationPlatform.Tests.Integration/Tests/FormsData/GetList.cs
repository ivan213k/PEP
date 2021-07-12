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
using PerformanceEvaluationPlatform.Models.FormData.Enums;
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
                    State = new List<StateEnum> {StateEnum.Active }
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
                    AppointmentDateFrom = DateTime.Today.AddDays(-20)
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
        public async Task Request_should_return_valid_items_when_filtering_by_appointment_date_to()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    AppointmentDateTo = DateTime.Today.AddDays(1)
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
                    Search = "Form 1"
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
        public async Task Request_should_return_valid_items_when_searching_by_assignee()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("forms")
                .SetQueryParams(new FormDataListFilterRequestModel
                {
                    Search = "User 1"
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
    }
}
