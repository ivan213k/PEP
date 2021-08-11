using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Net;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Roles
{
    [TestFixture]
    class CreateRole : IntegrationTestBase
    {
        private CreateRoleRequestModel createRoleRequestModel;

        public CreateRole()
        {
            createRoleRequestModel = new CreateRoleRequestModel
            {
                Title = "Test",
                IsPrimary = false
            };
        }

        [Test]
        public async Task Shoud_return_bad_request_when_create_role_request_model_is_null()
        {
            //Arrange
            CreateRoleRequestModel createRoleRequestModel = null;
            var request = CreatePostHttpRequest("roles", createRoleRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_bad_request_when_title_is_not_valid()
        {
            //Arrange
            createRoleRequestModel.Title = "I";
            var request = CreatePostHttpRequest("roles", createRoleRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Shoud_return_conflict_when_role_with_the_same_title_already_exist()
        {
            //Arrange
            var request = CreatePostHttpRequest("roles", createRoleRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            }
        }

        [Test]
        public async Task Shoud_create_role_when_request_model_is_valid()
        {
            //Arrange
            var request = CreatePostHttpRequest("roles", createRoleRequestModel);

            //Act
            var response = await SendRequest(request);

            //Assert
            if (response.StatusCode != HttpStatusCode.Conflict)
            {
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                var idViewModel = await response.Content.DeserializeAsAsync<IdViewModel>();
                var role = await GetRoleById(idViewModel.Id);

                Assert.IsNotNull(role);
            }
        }

        private async Task<RoleDetailsViewModel> GetRoleById(int roleId)
        {
            var request = CreateGetHttpRequest("roles", roleId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<RoleDetailsViewModel>();
        }
    }
}
