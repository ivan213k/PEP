using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Teams.Dto
{
    public class TeamListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ProjectTitle { get; set; }
        public int ProjectId { get; set; }
        public int Size { get; set; }
        public string TeamLead { get; set; }
    }
}
