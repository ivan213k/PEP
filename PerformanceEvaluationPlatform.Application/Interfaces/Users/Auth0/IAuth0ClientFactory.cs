using Auth0.AuthenticationApi;
using Auth0.ManagementApi;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Users.Auth0
{
     public interface IAuth0ClientFactory
    {
        Task<ManagementApiClient> CreateManagementApi();
        AuthenticationApiClient CreateAuthenticationApi();
    }
}
