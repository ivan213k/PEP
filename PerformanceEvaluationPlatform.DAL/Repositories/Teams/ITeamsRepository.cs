﻿using PerformanceEvaluationPlatform.DAL.Models.Teams.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Teams
{
    public interface ITeamsRepository
    {
        Task<IList<TeamListItemDto>> GetList(TeamListFilterDto filter);
    }
}