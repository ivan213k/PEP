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

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Field
{
    [TestFixture]
    public class CreateField : IntegrationTestBase
    {
        private CreateFieldRequestModel createFieldRequestModel;

        public CreateField()
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
        public async Task Shoud_return_bad_request_when_create_field_request_model_is_null() 
        {
            //Arrange
            CreateFieldRequestModel createFieldRequestModel = null;
            var request = CreatePostHttpRequest("fields", createFieldRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_field_type_does_not_exist()
        {
            //Arrange
            createFieldRequestModel.TypeId = 10000;
            var request = CreatePostHttpRequest("fields", createFieldRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_assesment_group_does_not_exist()
        {
            //Arrange
            createFieldRequestModel.AssesmentGroupId = 10000;
            var request = CreatePostHttpRequest("fields", createFieldRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_create_field_when_request_model_is_valid()
        {
            //Arrange
            var request = CreatePostHttpRequest("fields", createFieldRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var idViewModel = await response.Content.DeserializeAsAsync<IdViewModel>();
            var field = await GetFieldById(idViewModel.Id);

            Assert.IsNotNull(field);
        }

        private async Task<FieldDetailsViewModel> GetFieldById(int fieldId)
        {
            var request = CreateGetHttpRequest("fields", fieldId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<FieldDetailsViewModel>();
        }
        
    }
}
