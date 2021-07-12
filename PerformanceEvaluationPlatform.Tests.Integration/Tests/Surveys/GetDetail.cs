using NUnit.Framework;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Assert;
using PerformanceEvaluationPlatform.Tests.Integration.Infrastructure.Flurl;
using PerformanceEvaluationPlatform.Tests.Integration.Tests.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Tests.Surveys
{
    [TestFixture]
    public class GetDetail : IntegrationTestBase
    {
        [Test, Ignore("")]
        public async Task Request_should_return_not_found_when_wrong_id_is_requested()
        {
            //Arrange
            HttpRequestMessage request = BaseAddress
                .AppendPathSegment("surveys")
                .AppendPathSegment(10000)
                .WithHttpMethod(HttpMethod.Get);

            //Act
            HttpResponseMessage response = await SendRequest(request);

            //Assert
            CustomAssert.IsNotFound(response);
        }
    }
}
