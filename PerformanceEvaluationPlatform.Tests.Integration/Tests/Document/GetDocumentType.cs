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
    class GetDocumentType : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_not_found_when_get_wrong_id_of_type_requested() {
            //Arrenge
            HttpRequestMessage request = BaseAddress
             .AppendPathSegment("documents/types")
             .AppendPathSegment(30)
             .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            //Assert
            CustomAssert.IsNotFound(response);
        }

        [Test]
        public async Task Request_should_return_valid_TypeModel() {
            //Arrenge
            HttpRequestMessage request = BaseAddress
            .AppendPathSegment("documents/types")
            .AppendPathSegment(1)
            .WithHttpMethod(HttpMethod.Get);
            //Act
            HttpResponseMessage response = await SendRequest(request);
            var typemodel = JsonConvert.DeserializeObject<TypeModel>(await response.Content.ReadAsStringAsync());
            //Assert
            CustomAssert.IsSuccess(response);
            Assert.IsNotNull(typemodel);
            Assert.AreEqual(1,typemodel.Id);
        }
    }
}
