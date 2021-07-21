﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplateListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }
        
        public string StatusName { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
