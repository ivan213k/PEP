using PerformanceEvaluationPlatform.Application.Model.Documents;
using System;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class RequestAddDocumentModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public DateTime ValidToDate { get; set; }
        [Required]
        [MaxLength(512,ErrorMessage ="File name lenght can`t be more 512 symbols")]
        public string FileName { get; set; }
        public int CreatedById { get; set; }
        public string MetaDate { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static CreateDocumentDto AsDto(this RequestAddDocumentModel request)
        {
            var createDocumentDto = new CreateDocumentDto()
            {
                Id=request.Id,
                UserId=request.UserId,
                TypeId=request.TypeId,
                ValidToDate=request.ValidToDate,
                FileName=request.FileName,
                CreatedById=request.CreatedById,
                MetaDate=request.MetaDate
            };
            return createDocumentDto;
        }
    }
}
