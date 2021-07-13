using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormTemplates
{
    [TestFixture]
    public class GetList : IntegrationTestBase
    {
        private HttpRequestMessage GetHttpRequest()
        {
            return BaseAddress
                .AppendPathSegment("formtemplates")
                .WithHttpMethod(HttpMethod.Get);
        }

        private HttpRequestMessage GetHttpRequest(object values)
        {
            return BaseAddress
                .AppendPathSegment("formtemplates")
                .SetQueryParams(values)
                .WithHttpMethod(HttpMethod.Get);
        }

        [Test]
        public async Task Request_should_return_valid_items()
        {
            var request = GetHttpRequest();

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<ICollection<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_status()
        {
            var request = GetHttpRequest(new FormTemplateListFilterOrderRequestModel
            {
                StatusIds = new List<int> { 1 }
            });

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<ICollection<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_assesment_groups()
        {
            var request = GetHttpRequest(new FormTemplateListFilterOrderRequestModel
            {
                AssesmentGroupIds = new List<int> { 2 }
            });

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<ICollection<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(1, content.Count);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_ascending()
        {
            var request = GetHttpRequest(new FormTemplateListFilterOrderRequestModel
            {
                Sort = SortOrder.Ascending
            });

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<IList<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual("Junior Front-End Dev", content[0].Name);
            Assert.AreEqual("Middle Back-End Dev", content[1].Name);
            Assert.AreEqual("Middle Front-End Dev", content[2].Name);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_ordering_by_descending()
        {
            var request = GetHttpRequest(new FormTemplateListFilterOrderRequestModel
            {
                Sort = SortOrder.Descending
            });

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<IList<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual("Junior Front-End Dev", content[2].Name);
            Assert.AreEqual("Middle Back-End Dev", content[1].Name);
            Assert.AreEqual("Middle Front-End Dev", content[0].Name);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_filtering_by_search()
        {
            var request = GetHttpRequest(new FormTemplateListFilterOrderRequestModel
            {
                Search="Middle"
            });

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<IList<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Middle Back-End Dev", content[0].Name);
            Assert.AreEqual("Middle Front-End Dev", content[1].Name);
        }

        [Test]
        public async Task Request_should_return_valid_items_when_pagination()
        {
            var request = GetHttpRequest(new FormTemplateListFilterOrderRequestModel
            {
                Skip=0,
                Take=2
            });

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<IList<FormTemplateListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(content);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Middle Back-End Dev", content[0].Name);
            Assert.AreEqual("Middle Front-End Dev", content[1].Name);
        }

    }
}
