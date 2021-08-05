using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class RequestUpdateDocumentModel
    {
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public DateTime ValidToDate { get; set; }
        [Required]
        [MaxLength(512, ErrorMessage = "File name lenght can`t be more 512 symbols")]
        public string FileName { get; set; }
        public int? LastUpdateById { get; set; }
        public string MetaData { get; set; }
    }
}
