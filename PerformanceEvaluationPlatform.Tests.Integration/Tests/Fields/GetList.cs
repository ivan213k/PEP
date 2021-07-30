using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Fields
{
    [TestFixture]
    public class GetFieldsList : IntegrationTestBase
    {
        private HttpRequestMessage CreateGetHttpRequest(object requestModel)
        {
            return BaseAddress
                            .AppendPathSegment("fields")
                            .SetQueryParams(requestModel)
                            .WithHttpMethod(HttpMethod.Get);
        }
        private HttpRequestMessage CreateGetHttpRequest()
        {
            return BaseAddress
                            .AppendPathSegment("fields")
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

            var content = await response.Content.DeserializeAsAsync<ICollection<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(4, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_filtering_by_take()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                Take = 2
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_filtering_by_skip()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                Skip = 1
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual("Communication skills", content[0].Name);
            Assert.AreEqual("Full-bleed divider", content[1].Name);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_name()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                Search = "Communication skills"
            };
            var request = CreateGetHttpRequest(requestModel);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_type_id()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                TypeIds = new List<int> { 2 }
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_assesment_group_id()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                AssesmentGroupIds = new List<int> { 1 }
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<ICollection<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_form_name_ascending()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                FieldNameSortOrder = SortOrder.Ascending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Active listening", content[0].Name);
            Assert.AreEqual("Communication skills", content[1].Name);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_form_name_descending()
        {
            //Arrange
            var requestModel = new FieldListFilterRequestModel
            {
                FieldNameSortOrder = SortOrder.Descending
            };
            var request = CreateGetHttpRequest(requestModel);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

            var content = await response.Content.DeserializeAsAsync<IList<FieldListItemViewModel>>();
            Assert.NotNull(content);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Written communication", content[0].Name);
            Assert.AreEqual("Full-bleed divider", content[1].Name);
        }

    }
}
