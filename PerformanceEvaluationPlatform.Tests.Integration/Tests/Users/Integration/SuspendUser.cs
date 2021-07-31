using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.User.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Users.Integration
{
    [TestFixture]
    class SuspendUser : IntegrationTestBase
    {
        [Test]
        public async Task SuspendUser_WhenCalled_ReturnOkAndChangeUserState()
        {
            var item = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(1)
                .AppendPathSegment("suspend")
                .WithHttpMethod(HttpMethod.Put);

            var user = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(1)
                .WithHttpMethod(HttpMethod.Get);

            HttpResponseMessage responseItem = await SendRequest(item);

            HttpResponseMessage responseUser = await SendRequest(user);
            var content = JsonConvert.DeserializeObject<UserDetailViewModel>(await responseUser.Content.ReadAsStringAsync());

            Assert.That(content.StateName, Is.EqualTo("Suspend"));
        }
        [Test]
        public async Task SuspendUser_NotExistingId_ReturnNOtFound()
        {
            var item = BaseAddress
                .AppendPathSegment("users")
                .AppendPathSegment(100)
                .AppendPathSegment("suspend")
                .WithHttpMethod(HttpMethod.Put);

            HttpResponseMessage response = await SendRequest(item);

            CustomAssert.IsNotFound(response);
        }
    }
}
