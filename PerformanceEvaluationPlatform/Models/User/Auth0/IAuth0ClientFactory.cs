using Auth0.AuthenticationApi;
using Auth0.ManagementApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.Auth0
{
     public interface IAuth0ClientFactory
    {
        Task<ManagementApiClient> CreateManagementApi();
        Task<AuthenticationApiClient> CreateAuthenticationApi();
    }
}
