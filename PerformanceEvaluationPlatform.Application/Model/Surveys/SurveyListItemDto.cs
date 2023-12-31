﻿using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Surveys
{
    public class SurveyListItemDto
    {
        public int Id { get; set; }
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeLastName { get; set; }
        public int AssigneeId { get; set; }
        public string SupervisorFirstName { get; set; }
        public string SupervisorLastName { get; set; }
        public int SupervisorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StateName { get; set; }
        public int StateId { get; set; }
        public double ProgressInPercenteges { get; set; }
        public ICollection<SurveyListItemAssignedUserDto> AssignedUsers { get; set; }
        public ICollection<SurveyListItemFormDataDto> FormData { get; set; }
    }
}
