using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.IO;
using System.Net;
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
            var form = GetFileMock("text/csv", "test;test;","Creating.csv");
            RequestAddDocumentModel model = new RequestAddDocumentModel()
            {
                UserId = 1,
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                CreatedById = 3,
                MetaData = "Some new Added metadate",
                File=form
            };

            string contentType = GetHttpContentType();

            var formData = FormDataConstructor(model, form);
            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress.AppendPathSegment("document"))
            {
                Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), contentType }
            },
                Content = formData
            };



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
            var Id=await CreatingDocumentHelper(3);
            //Updating Document
            //Arrange
            var form = GetFileMock("text/csv", "test;test;", "Updating.csv");
            RequestUpdateDocumentModel model = new RequestUpdateDocumentModel()
            {
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                LastUpdateById = 3,
                MetaData = "Some MetadataUpdated"
            };
            string contentType = GetHttpContentType();

            var formData = FormDataConstructor(model, form);
            var request = new HttpRequestMessage(HttpMethod.Put, BaseAddress.AppendPathSegment("document").AppendPathSegment(Id))
            {
                Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), contentType }
            },
                Content = formData
            };

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
            Assert.AreEqual(document.ValidTo.ToShortDateString(), model.ValidToDate.ToShortDateString());
            

            //Deleting
            await DeletingDocumentHelperTest(Id);
        }

        [Test]
        public async Task Request_should_return_valid_result_of_deleting()
        {
            var Id = await CreatingDocumentHelper(1);
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
                LastUpdateById = 3,
                MetaData = "Some MetadataUpdated"
            };
            var form = GetFileMock("text/csv", "test;test;", "Search.csv");
            
            string contentType = GetHttpContentType();

            var formData = FormDataConstructor(model, form);
            var request = new HttpRequestMessage(HttpMethod.Put, BaseAddress.AppendPathSegment("document").AppendPathSegment(100000000))
            {
                Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), contentType }
            },
                Content = formData
            };


            //Act
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
        private async Task<int> CreatingDocumentHelper(int userId) {
            //Arrange
            var form = GetFileMock("text/csv", "test;test;", "Helper.csv");
            RequestAddDocumentModel model = new RequestAddDocumentModel()
            {
                UserId = userId,
                TypeId = 2,
                ValidToDate = System.DateTime.Now.AddDays(365),
                CreatedById = 1,
                MetaData = "Some helper metadata",
                File = form
            };
          
            string contentType = GetHttpContentType();

            var formData = FormDataConstructor(model, form);
            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress.AppendPathSegment("document"))
            {
                Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), contentType }
            },
                Content = formData
            };



            //Act
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
            await SendRequest(request);
        }
        private IFormFile GetFileMock(string contentType, string content,string fileName)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            var file = new FormFile(
                baseStream: new MemoryStream(bytes),
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "Data",
                fileName: fileName
            )
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            return file;
        }
        private MultipartFormDataContent FormDataConstructor(RequestAddDocumentModel model,IFormFile file) {
            var formData = new MultipartFormDataContent() {
               { new StringContent(model.UserId.ToString()), nameof(model.UserId) },
               { new StringContent(model.TypeId.ToString()), nameof(model.TypeId) },
               { new StringContent(model.ValidToDate.ToString()), nameof(model.ValidToDate) },
               { new StringContent(model.CreatedById.ToString()), nameof(model.CreatedById) },
               { new StringContent(model.MetaData), nameof(model.MetaData) },
            };
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Close();
            formData.Add(new ByteArrayContent(ms.ToArray()), nameof(model.File), file.FileName);
            return formData;
        }
        private MultipartFormDataContent FormDataConstructor(RequestUpdateDocumentModel model, IFormFile file)
        {
            var formData = new MultipartFormDataContent() {
               { new StringContent(model.TypeId.ToString()), nameof(model.TypeId) },
               { new StringContent(model.ValidToDate.ToString()), nameof(model.ValidToDate) },
               { new StringContent(model.LastUpdateById.ToString()), nameof(model.LastUpdateById) },
               { new StringContent(model.MetaData), nameof(model.MetaData) },
            };
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Close();
            formData.Add(new ByteArrayContent(ms.ToArray()), nameof(model.File), file.FileName);
            return formData;
        }
        private  string GetHttpContentType() {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            return contentType;
        }
    }
}
