using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Application.Model.Documents;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Document
{
    [TestFixture]
    class GetDocumentTypes: IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_valid_Types() {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("documents/types")
                .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Asserts
            var responce = await response.Content.ReadAsStringAsync();
            var typeslist = JsonConvert.DeserializeObject<IList<DocumentTypeDto>>(responce);
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(typeslist);
            Assert.IsNotEmpty(typeslist);
            Assert.AreEqual(3,typeslist.Count);

        }
        
    }
}

