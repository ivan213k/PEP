using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto
{
    public class FormTemplateListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }

        public string StatusName { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
