using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;

namespace PerformanceEvaluationPlatform.Models.FieldGroups.ViewModels
{
    public class FieldGroupDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> FieldsNames { get; set; }
    }
}
