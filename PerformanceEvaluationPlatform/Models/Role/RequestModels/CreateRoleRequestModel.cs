﻿using PerformanceEvaluationPlatform.Application.Model.Roles;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class CreateRoleRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title should be greater than 1")]
        public string Title { get; set; }
        [Required]
        public bool IsPrimary { get; set; }

    }

    public static partial class ViewModelMapperExtensions
    {
        public static CreateRoleDto AsDto(this CreateRoleRequestModel requestModel)
        {
            return new CreateRoleDto
            {
                Title = requestModel.Title,
                IsPrimary = requestModel.IsPrimary
            };
        }
    }
}
