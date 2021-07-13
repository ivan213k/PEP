using PerformanceEvaluationPlatform.Models.FormData.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataDetailsViewModel
    {
        public FormDataDetailViewModel Detail { get; set; }
        public ICollection<FormDataAnswersItemViewModel> Answers { get; set; }

    }
}
