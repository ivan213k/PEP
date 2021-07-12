﻿namespace PerformanceEvaluationPlatform.Models.Field.ViewModels
{
    public class FieldListItemViewModel
    {   
        public int Id { get; set; } //add Id for edit/delete/copy
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssesmentGroupName { get; set; }
        public int TypeId { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }
    }
}
