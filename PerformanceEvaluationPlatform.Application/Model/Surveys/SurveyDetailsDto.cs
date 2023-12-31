﻿using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Surveys
{
    public class SurveyDetailsDto
    {
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Supervisor { get; set; }
        public int SupervisorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string RecommendedLevel { get; set; }
        public int RecommendedLevelId { get; set; }
        public ICollection<SurveyDetailsAssignedUserDto> AssignedUsers { get; set; }
        public ICollection<SurveyFormDataDto> FormData { get; set; }
        public string Summary { get; set; }
    }
}
