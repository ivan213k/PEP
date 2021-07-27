using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class CreateFormTemplateRequestModel
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        public ICollection<FormTemplateFieldRequestModel> Fields { get; set; }
    }

    public class FormTemplateFieldRequestModel
    {
        [Range(1,int.MaxValue)]
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int Order { get; set; }
    }
}
