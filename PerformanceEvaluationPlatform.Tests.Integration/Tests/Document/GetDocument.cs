using Newtonsoft.Json;
using NUnit.Framework;
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
    class GetDocument : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_not_found_when_get_wrong_id_requeted() {
            //Arrenge
            HttpRequestMessage request = BaseAddress
             .AppendPathSegment("documents")
             .AppendPathSegment(5000)
             .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_valid_DocumentDetailedViewModel() {
            //Arrenge
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("documents")
            .AppendPathSegment(1)
            .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            var document = JsonConvert.DeserializeObject<DocumentDetailViewModel>(await response.Content.ReadAsStringAsync());
            //Assert
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(document);
            Assert.AreEqual(1,document.Id);
        }
    }
}
