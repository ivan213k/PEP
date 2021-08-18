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
    public class CopyField : IntegrationTestBase
    {

        [Test]
        public async Task Shoud_return_not_found_when_field_does_not_exist()
        {
            //Arrange
            var fieldId = 1000;
            var request = CreatePostHttpRequest("fields/" + fieldId.ToString());

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Shoud_copy_field_when_id_is_valid()
        {
            //Arrange
            var fieldId = 1;
            var request = CreatePostHttpRequest("fields/" + fieldId.ToString());

            //Act
            var response = await SendRequest(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var idViewModel = await response.Content.DeserializeAsAsync<IdViewModel>();
            var field = await GetFieldById(idViewModel.Id);

            Assert.IsNotNull(field);
            Assert.AreNotEqual(field.Id, fieldId);
        }

        private async Task<FieldDetailsViewModel> GetFieldById(int fieldId)
        {
            var request = CreateGetHttpRequest("fields", fieldId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<FieldDetailsViewModel>();
        }
        
    }
}
