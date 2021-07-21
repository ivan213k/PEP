using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates
{
    public interface IFormTemplatesRepository
    {
        Task<IList<FormTemplateListItemDto>> GetList(FormTemplateListFilterOrderDto filter);
    }
}
