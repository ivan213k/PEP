using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using PerformanceEvaluationPlatform.Application.Packages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Field
{
    [TestFixture]
    public class DeleteField : IntegrationTestBase
    {
        private CreateFieldRequestModel createFieldRequestModel;

        public DeleteField()
        {
            createFieldRequestModel = new CreateFieldRequestModel
            {
                Id = 1,
                Name = "test field",
                TypeId = 1,
                AssesmentGroupId = 1,
                IsRequired = true,
                Description = "test"
            };

        }

        [Test]
        public async Task Shoud_return_not_found_when_field_does_not_exist()
        {
            //Arrange
            var fieldId = 1000;
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("fields")
            .AppendPathSegment(fieldId)
            .WithHttpMethod(HttpMethod.Delete);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_field_get_any_reference_to_form_template()
        {
            //Arrange
            var fieldId = 1;
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("fields")
            .AppendPathSegment(fieldId)
            .WithHttpMethod(HttpMethod.Delete);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_delete_field_when_id_is_valid()
        {   
            //Arrange
            var fieldId = CreateField();
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("fields")
            .AppendPathSegment(fieldId.Id)
            .WithHttpMethod(HttpMethod.Delete);
            //Act
            HttpResponseMessage response = await SendRequest(request);         
            //Assert
            var fieldResponse = await GetFieldById(fieldId.Id);
            CustomAssert.IsNotFound(fieldResponse);
        }

        private async Task<int> CreateField()
        {
            var postRequest = CreatePostHttpRequest("fields", createFieldRequestModel);
            var postResponse = await SendRequest(postRequest);

            Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);
            var idViewModel = await postResponse.Content.DeserializeAsAsync<IdViewModel>();

            return idViewModel.Id;
        }

        private async Task<HttpResponseMessage> GetFieldById(int fieldId)
        {
            var request = CreateGetHttpRequest("fields", fieldId);
            var response = await SendRequest(request);

            return response;
        }

    }
}
