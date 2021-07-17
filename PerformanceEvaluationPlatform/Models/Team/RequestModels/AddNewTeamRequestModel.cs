using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Team.RequestModels
{
    public class AddNewTeamRequestModel
    {
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public int Size { get; set; }
        public string TeamLead { get; set; }
    }
}
