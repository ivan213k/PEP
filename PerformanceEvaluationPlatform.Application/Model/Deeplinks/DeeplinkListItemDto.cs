using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Application.Model.Deeplinks
{
    public class DeeplinkListItemDto
    {
        public int Id { get; set; }

        public string SentToFirstName { get; set; }
        public string SentToLastName { get; set; }
        public DateTime ExpireDate { get; set; }
        public string State { get; set; }
        public string FormTemplate { get; set; }



    }
}
