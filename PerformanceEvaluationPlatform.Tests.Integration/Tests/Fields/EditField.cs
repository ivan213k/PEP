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
    public class EditField : IntegrationTestBase
    {
        private EditFieldRequestModel editFieldRequestModel;

        [Test]
        public async Task Shoud_return_bad_request_when_edit_field_request_model_is_null()
        {
            //Arrange
            var fieldId = 1;
            var request = CreatePutHttpRequest(requestModel: null, "fields", fieldId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_field_type_does_not_exist()
        {
            //Arrange
            var editFieldRequestModel = new EditFieldRequestModel
            {
                Name = "Test Edit",
                TypeId = 10000,
                AssesmentGroupId = 1,
                Description = "Test"
            };
            var fieldId = 1;
            var request = CreatePutHttpRequest(requestModel: editFieldRequestModel, "fields", fieldId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_assesment_group_does_not_exist()
        {
            //Arrange
            var editFieldRequestModel = new EditFieldRequestModel
            {
                Name = "Test Edit",
                TypeId = 1,
                AssesmentGroupId = 10000,
                Description = "Test"
            };
            var fieldId = 1;
            var request = CreatePutHttpRequest(requestModel: editFieldRequestModel, "fields", fieldId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            var editFieldRequestModel = new EditFieldRequestModel
            {
                Name = "Test Edit",
                TypeId = 1,
                AssesmentGroupId = 1,
                Description = "Test"
            };
            var fieldId = 1000;
            var request = CreatePutHttpRequest(requestModel: editFieldRequestModel, "fields", fieldId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Shoud_update_field_when_request_model_is_valid()
        {
            //Arrange
            var editFieldRequestModel = new EditFieldRequestModel
            {
                Name = "Test edit",
                TypeId = 1,
                AssesmentGroupId = 1,
                Description = "Test"
            };
            var fieldId = 1;
            var request = CreatePutHttpRequest(requestModel: editFieldRequestModel, "fields", fieldId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);
            var field = await GetFieldById(fieldId);
            Assert.NotNull(field);
            Assert.AreEqual(editFieldRequestModel.Name, "Test edit");
            Assert.AreEqual(editFieldRequestModel.Description, "Test");
        }

        private async Task<FieldDetailsViewModel> GetFieldById(int fieldId)
        {
            var request = CreateGetHttpRequest("fields", fieldId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<FieldDetailsViewModel>();
        }
        
    }
}
