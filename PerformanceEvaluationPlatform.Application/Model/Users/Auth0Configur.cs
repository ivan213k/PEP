using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Model.Users
{
    public class Auth0Configur
    {
        public string Domain { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ConnectionId { get; set; }
        public string DefaultPassword { get; set; }

        public string ManagementApiUrl => $"https://{Domain}/api/v2/";
    }
}
