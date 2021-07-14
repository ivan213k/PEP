using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Document.BaseModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            var typeslist = JsonConvert.DeserializeObject<IList<TypeModel>>(await response.Content.ReadAsStringAsync());
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(typeslist);
            Assert.IsNotEmpty(typeslist);
            Assert.AreEqual(5,typeslist.Count);

        }
        
    }
}

