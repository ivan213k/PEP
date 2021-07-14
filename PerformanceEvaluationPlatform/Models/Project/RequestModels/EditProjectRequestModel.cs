using System;

namespace PerformanceEvaluationPlatform.Models.Project.RequestModels
{
    public class EditProjectRequestModel
    {   
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string Coordinator { get; set; }
        //delete later)
    }
}
