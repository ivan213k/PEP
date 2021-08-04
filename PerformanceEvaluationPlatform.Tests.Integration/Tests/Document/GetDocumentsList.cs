using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Document
{
    [TestFixture]
    class GetDocumentsList: IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items() {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("documents")
                .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Asserts
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(documentlist);
            Assert.IsNotEmpty(documentlist);
            

        }
        [Test]
        public async Task Request_should_return_valid_items_after_pagination() {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("documents")
                .SetQueryParams(new DocumentRequestModel
                {
                    Skip = 0,
                    Take =3
                })
                .WithHttpMethod(HttpMethod.Get);

            //Act
                HttpResponseMessage responseMessage = await SendRequest(request);

            //Asserts
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await responseMessage.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(responseMessage);
            Assert.IsNotNull(documentlist);
            Assert.IsNotEmpty(documentlist);
            Assert.AreEqual(3,documentlist.Count);
            Assert.AreEqual(1,documentlist[0].Id);
        }
        [Test]
        public async Task Request_should_return_valid_items_after_default_pagination_behavior()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("documents")
                .SetQueryParams(new DocumentRequestModel())
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage responseMessage = await SendRequest(request);

            //Asserts
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await responseMessage.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(responseMessage);
            Assert.IsNotNull(documentlist);
            Assert.IsNotEmpty(documentlist);
            Assert.AreEqual(1,documentlist[0].Id);
        }
        [Test]
        public async Task Request_should_return_valid_items_after_searching_paramets() {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   Search = "File"
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(documentlist);
        }
        [Test]
        public async Task Request_should_return_emptyList() {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   Search = "1234112"
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsEmpty(documentlist);
        }
        [Test]
        public async Task Request_should_return_list_of_document_which_refer_to_user()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   UserIds =new List<int> {1,2,3}
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(documentlist);
        }
        [Test]
        public async Task Request_should_return_empty_list_of_document_which_refer_to_user()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   UserIds = new List<int> {300, 900, 1000 }
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var DocumentList = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsEmpty(DocumentList);
        }

        [Test]
        public async Task Request_should_return_empty_list_of_document_which_refer_to_document_types()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   TypeIds = new List<int> { 300, 900, 1000 }
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsEmpty(documentlist);
        }
        [Test]
        public async Task Request_should_return_list_of_document_which_refer_to_document_types()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   TypeIds = new List<int> { 1, 2, 3 }
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotEmpty(documentlist);
        }
        [Test]
        public async Task Request_should_return_list_of_document_which_containt_one_element_valid_to_date()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   ValidTo=DateTime.Now.AddDays(2)
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotEmpty(documentlist);
            Assert.AreEqual(1,documentlist.Count);
        }
        [Test]
        public async Task Request_should_return_emty_list_of_document_when_get_invalid_valid_to_date()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   ValidTo = new DateTime(1970, 6, 15)
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsEmpty(documentlist);
        }
        [Test]
        public async Task Request_should_return_ordered_ascendeing_list_of_document_by_username()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   TypeSortOrder=Models.Shared.Enums.SortOrder.Ascending,
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotEmpty(documentlist);
            
        }
        [Test]
        public async Task Request_should_return_ordered_descending_list_of_document_by_username()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   NameSortOrder = Models.Shared.Enums.SortOrder.Descending
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documnetlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotEmpty(documnetlist);
            
        }
        [Test]
        public async Task Request_should_return_ordered_ascendeing_list_of_document_by_typername()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   TypeSortOrder = Models.Shared.Enums.SortOrder.Descending,
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var DocumentList = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotEmpty(DocumentList);
           
        }
        [Test]
        public async Task Request_should_return_ordered_descending_list_of_document_by_typename()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("documents")
               .SetQueryParams(new DocumentRequestModel
               {
                   NameSortOrder = Models.Shared.Enums.SortOrder.Ascending
               })
               .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            var documentlist = JsonConvert.DeserializeObject<IList<DocumentListItemViewModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotEmpty(documentlist);
            
        }
    }
}

