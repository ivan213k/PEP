using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.Auth0
{
    public class Auth0Configure
    {
        public string Domain { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
