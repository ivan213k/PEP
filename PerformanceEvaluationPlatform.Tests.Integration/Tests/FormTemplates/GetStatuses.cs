using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.FormTemplates
{
    [TestFixture]
    public class GetStatuses : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_items()
        {
            var request = BaseAddress
                .AppendPathSegment("formtemplates")
                .AppendPathSegment("statuses")
                .WithHttpMethod(HttpMethod.Get);

            var response = await SendRequest(request);

            CustomAssert.IsSuccess(response);

            var content = JsonConvert
                .DeserializeObject<IList<FormTemplateStatusListItemViewModel>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Active", content[0].Name);
        }
    }
}
