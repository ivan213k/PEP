using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Document
{
    [TestFixture]
    class CreateEditDeleteOperationOnDocument : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_id_item_after_added_document()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("document")
                .WithHttpMethod(HttpMethod.Post);

            RequestAddDocumentModel model = new RequestAddDocumentModel()
            {
                UserId = 1,
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                FileName = "Added.doc",
                CreatedById = 3,
                MetaDate = "Some new Added metadate"
            };

            var bodyContent = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await SendRequest(request);
            var define = new { id = 0 };
            var result = await response.Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeAnonymousType(result, define);
            //Assert
            CustomAssert.IsSuccess(response);

            //Use Helper to clean 
            await DeletingDocumentHelperTest(document.id);

        }
        [Test]
        public async Task Request_should_return_valid_result_after_updating_entety()
        {
            var Id=await GreatingDocumentHelperForTests();
            //Updating Document
            //Arrange
            RequestUpdateDocumentModel model = new RequestUpdateDocumentModel()
            {
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                FileName = "Updated.doc",
                LastUpdateById = 3,
                MetaData = "Some MetadataUpdated"
            };
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("document")
                .AppendPathSegment(Id)
                .WithHttpMethod(HttpMethod.Put);

            var bodyContent = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");

            HttpRequestMessage getModelForChecking = BaseAddress
                .AppendPathSegment("documents")
                .AppendPathSegment(Id)
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);
            HttpResponseMessage responceDocumentModel = await SendRequest(getModelForChecking);
            var document = JsonConvert.DeserializeObject<DocumentDetailViewModel>(await responceDocumentModel.Content.ReadAsStringAsync());

            //Asserts
            CustomAssert.IsSuccess(response);
            Assert.AreEqual(document.Id, Id);
            Assert.AreEqual(document.Id, Id);
            Assert.AreEqual(document.ValidTo, model.ValidToDate);
            Assert.AreEqual(document.FileName, model.FileName);

            //Deleting
            await DeletingDocumentHelperTest(Id);
        }

        [Test]
        public async Task Request_should_return_valid_result_of_deleting()
        {
            var Id = await GreatingDocumentHelperForTests();
            //Arrange
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("document")
            .AppendPathSegment(Id)
            .WithHttpMethod(HttpMethod.Delete);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);

        }
        [Test]
        public async Task Request_should_return_not_found()
        {
            //Arrange
            RequestUpdateDocumentModel model = new RequestUpdateDocumentModel()
            {
               
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                FileName = "Updated.doc",
                LastUpdateById = 3,
                MetaData = "Some MetadataUpdated"
            };
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("document")
               .AppendPathSegment(1000000)
               .WithHttpMethod(HttpMethod.Put);

           
            //Act
            var bodyContent = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await SendRequest(request);
            await response.Content.ReadAsStringAsync();
            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_bad_request_whene_get_invalid_valid_to_date()
        {
            //Arrange
            RequestUpdateDocumentModel model = new RequestUpdateDocumentModel()
            {
               
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(-365),
                FileName = "Updated.doc",
                LastUpdateById = 3,
                MetaData = "Some MetadataUpdated"
            };
            HttpRequestMessage request = BaseAddress
               .AppendPathSegment("document")
               .AppendPathSegment(1)
               .WithHttpMethod(HttpMethod.Put);

            
            //Act
            var bodyContent = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await SendRequest(request);
            await response.Content.ReadAsStringAsync();

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        //Private Helper Functions
        private async Task<int> GreatingDocumentHelperForTests() {
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("document")
                .WithHttpMethod(HttpMethod.Post);

            RequestAddDocumentModel model = new RequestAddDocumentModel()
            {
                UserId = 1,
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                FileName = "Helper.doc",
                CreatedById = 3,
                MetaDate = "Some new Helper metadate"
            };

            var bodyContent = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await SendRequest(request);
            var define = new { id = 0 };
            var result = await response.Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeAnonymousType(result, define);
            return document.id;
        }
        private async Task DeletingDocumentHelperTest(int id) {
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("document")
            .AppendPathSegment(id)
            .WithHttpMethod(HttpMethod.Delete);
            //Act
            HttpResponseMessage response = await SendRequest(request);
        }
       
    }
}
