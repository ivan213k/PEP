using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Roles
{
    [TestFixture]
    class ChangeRole : IntegrationTestBase
    {
        private UpdateRoleRequestModel editRoleRequestModel;

        public ChangeRole()
        {
            editRoleRequestModel = new UpdateRoleRequestModel
            {
                Id = 4,
                Title = "Team Lead",
                IsPrimary = true
            };
        }

        [Test]
        public async Task Request_should_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            var roleId = 1000;
            var request = CreatePutHttpRequest(editRoleRequestModel, "roles", roleId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Shoud_return_conflict_when_role_with_the_same_title_already_exist()
        {
            //Arrange
            var roleId = 2;
            editRoleRequestModel.Title = "Backend";
            var request = CreatePutHttpRequest(editRoleRequestModel, "roles", roleId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsConflict(response);
        }


        [Test]
        public async Task Shoud_return_bad_request_when_title_is_not_valid()
        {
            //Arrange
            var roleId = 1;
            editRoleRequestModel.Title = "I";
            var request = CreatePutHttpRequest(editRoleRequestModel, "roles", roleId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsBadRequest(response);
        }

        [Test]
        public async Task Request_should_change_IsPrimary_to_true()
        {
            //Arrange
            var roleId = 4;
            var request = CreatePutHttpRequest(editRoleRequestModel, "roles", roleId);

            //Act
            var response = await SendRequest(request);

            //Assert
            CustomAssert.IsSuccess(response);
            var role = await GetRoleById(roleId);
            Assert.AreEqual(true, role.IsPrimary);
        }

        private async Task<RoleDetailsViewModel> GetRoleById(int roleId)
        {
            var request = CreateGetHttpRequest("roles", roleId);
            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<RoleDetailsViewModel>();
        }
    }
}
