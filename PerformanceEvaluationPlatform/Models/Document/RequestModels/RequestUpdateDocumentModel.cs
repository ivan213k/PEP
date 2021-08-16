using PerformanceEvaluationPlatform.Application.Model.Documents;
using System;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class RequestUpdateDocumentModel
    {
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

    public static partial class ViewModelMapperExtensions
    {
        public static UpdateDocumentDto AsDto(this RequestUpdateDocumentModel request)
        {
            var updateDocumentDto = new UpdateDocumentDto()
            {
                TypeId=request.TypeId,
                ValidToDate=request.ValidToDate,
                FileName=request.FileName,
                LastUpdateById=request.LastUpdateById,
                MetaData=request.MetaData
            };
            return updateDocumentDto;
        }
    }
}
