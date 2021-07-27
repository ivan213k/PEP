using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Projects
{
    public class ProjectListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { set; get; }
        public string Coordinator { set; get; }
    }
}
