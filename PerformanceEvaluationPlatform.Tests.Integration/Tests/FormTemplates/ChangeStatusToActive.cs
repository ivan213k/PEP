using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Extensions;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormTemplates
{
    [TestFixture]
    public class ChangeStatusToActive: IntegrationTestBase
    {
        private const int ActiveStatusId = 2;
        private const int ArchivedStatusId = 3;

        [Test]
        public async Task Request_should_return_not_found_when_form_template_does_not_exist_with_this_id()
        {
            var formTemplateId = 10000;
            var request = CreatePutHttpRequest("formtemplates", formTemplateId, "activate");

            var response = await SendRequest(request);

            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_unacceptable_when_status_is_archived()
        {
            var formTemplateId = 3;
            var request = CreatePutHttpRequest("formtemplates", formTemplateId, "activate");

            var response = await SendRequest(request);

            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Test]
        public async Task Request_should_return_unacceptable_when_status_is_active()
        {
            var formTemplateId = 2;
            var request = CreatePutHttpRequest("formtemplates", formTemplateId, "activate");

            var response = await SendRequest(request);

            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Test]
        public async Task Request_should_change_status_to_active_without_previous_active_form_template()
        {
            var formTemplateId = 8;
            var request = CreatePutHttpRequest("formtemplates", formTemplateId, "activate");

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);
            var formTemplate = await GetFormTemplateById(formTemplateId);
            Assert.AreEqual(ActiveStatusId, formTemplate.StatusId);
        }

        [Test]
        public async Task Request_should_change_status_to_active_with_previous_active_form_template()
        {
            var formTemplateId = 4;
            var request = CreatePutHttpRequest("formtemplates", formTemplateId, "activate");

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var formTemplate = await GetFormTemplateById(formTemplateId);

            var previousActiveFormTemplateId = 2;
            var previousActiveFormTemplate = await GetFormTemplateById(previousActiveFormTemplateId);

            Assert.AreEqual(ActiveStatusId, formTemplate.StatusId);
            Assert.AreEqual(ArchivedStatusId, previousActiveFormTemplate.StatusId);
        }

        private async Task<FormTemplateDetailsViewModel> GetFormTemplateById(int formTemplateId)
        {
            var request = CreateGetHttpRequest("formtemplates", formTemplateId);

            var response = await SendRequest(request);

            return await response.Content.DeserializeAsAsync<FormTemplateDetailsViewModel>();
        }
    }
}
