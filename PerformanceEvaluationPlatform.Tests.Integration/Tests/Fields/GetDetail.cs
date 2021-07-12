using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Fields
{
    [TestFixture]
    public class GetFieldTypesDetail : IntegrationTestBase
    {
        [Test]
        public async Task Request_should_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
	            .AppendPathSegment("fields")
	            .AppendPathSegment(10000)
	            .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }
    }
}
