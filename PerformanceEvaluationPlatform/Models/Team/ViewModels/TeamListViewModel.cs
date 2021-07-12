using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Team.ViewModels
{
    public class TeamListViewModel
    {
        public string TeamTitle { get; set; }
        public int TeamId { get; set; }
        public string ProjectTitle { get; set; }
        public int ProjectId { get; set; }
        public int TeamSize { get; set; }
        public string TeamLead { get; set; }
    }
}