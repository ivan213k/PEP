using System;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dto
{
    public class FieldGroupDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> FieldsNames { get; set; }
    }
}
