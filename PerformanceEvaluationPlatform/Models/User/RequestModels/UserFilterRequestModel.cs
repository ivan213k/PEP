using PerformanceEvaluationPlatform.Application.Model.Users;
using PerformanceEvaluationPlatform.Models.Shared;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class UserFilterRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> StateIds { get; set; }
        public ICollection<int> RoleIds { get; set; }
        public DateTime? PreviousPEDate { get; set; }
        public DateTime? NextPEDate { get; set; }
        public byte? UserNameSortOrder { get; set; }
        public byte? UserPreviousPESortOrder { get; set; }
        public byte? UserNextPESortOrder { get; set; }


        public UserFilterRequestModel()
        {
            Skip = 0;
            Take = 2;
        }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static UserFilterDto AsDto(this UserFilterRequestModel filter)
        {
            return new UserFilterDto()
            {
                NextPeDate = filter.NextPEDate,
                PreviousPeDate = filter.PreviousPEDate,
                RoleIds = filter.RoleIds,
                Search = filter.Search,
                Skip = filter.Skip,
                StateIds = filter.StateIds,
                UserNameSort = filter.UserNameSortOrder,
                Take = filter.Take,
                UserNextPE = filter.UserNextPESortOrder,
                UserPreviousPE = filter.UserPreviousPESortOrder
            };
        }
    }
}
