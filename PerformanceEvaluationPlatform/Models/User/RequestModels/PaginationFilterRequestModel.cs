using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class PaginationFilterRequestModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public PaginationFilterRequestModel()
        {
            CurrentPage = 1;
            PageSize = 2;
        }
    }
}
