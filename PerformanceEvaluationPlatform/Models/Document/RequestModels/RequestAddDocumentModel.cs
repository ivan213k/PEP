using Microsoft.AspNetCore.Http;
using PerformanceEvaluationPlatform.Application.Model.Documents;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

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
        public int CreatedById { get; set; }
        public string MetaData { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public  static CreateDocumentDto AsDto(this RequestAddDocumentModel request)
        {
            MemoryStream ms = new MemoryStream();
             request.File.OpenReadStream().CopyTo(ms);
            var fileByte = ms.ToArray();
            ms.Close();
            var createDocumentDto = new CreateDocumentDto()
            {
                Id=request.Id,
                UserId=request.UserId,
                TypeId=request.TypeId,
                ValidToDate=request.ValidToDate,
                FileName=request.File.FileName,
                CreatedById=request.CreatedById,
                MetaDate=request.MetaData,
                File=fileByte
            };
            return createDocumentDto;
        }
    }
}
