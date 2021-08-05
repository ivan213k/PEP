using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.Validator
{
    public class ValidationError
    {
        public Dictionary<string, string> ValidationErrors { get; set; } = new Dictionary<string, string>();
        public bool IsValid { get; set; }
    }
}
