using Auth0.ManagementApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.Auth0
{
     public interface IAuth0ClientFactory
    {
        Task<ManagementApiClient> Create();
    }
}
