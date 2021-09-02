using Microsoft.AspNetCore.Http;
using PerformanceEvaluationPlatform.Application.Model.Documents;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class RequestUpdateDocumentModel
    {
        [Required]
        public int TypeId { get; set; }
        [Required]
        public DateTime ValidToDate { get; set; }
        public int? LastUpdateById { get; set; }
        public string MetaData { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static UpdateDocumentDto AsDto(this RequestUpdateDocumentModel request)
        {
            MemoryStream ms = new MemoryStream();
            request.File.OpenReadStream().CopyTo(ms);
            var fileByte = ms.ToArray();
            ms.Close();
            var updateDocumentDto = new UpdateDocumentDto()
            {
                TypeId = request.TypeId,
                ValidToDate = request.ValidToDate,
                FileName = request.File.FileName,
                LastUpdateById = request.LastUpdateById,
                MetaData = request.MetaData,
                File=fileByte
            };
            return updateDocumentDto;
        }
    }
}
