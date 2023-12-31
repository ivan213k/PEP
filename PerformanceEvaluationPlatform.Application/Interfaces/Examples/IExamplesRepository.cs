﻿using PerformanceEvaluationPlatform.Application.Model.Examples;
using PerformanceEvaluationPlatform.Domain.Examples;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Examples
{
    public interface IExamplesRepository : IBaseRepository
    {
        Task<IList<ExampleListItemDto>> GetList(ExampleListFilterDto filter);
        Task<IList<ExampleTypeListItemDto>> GetTypesList();
        Task<IList<ExampleStateListItemDto>> GetStatesList();
        Task<ExampleDetailsDto> GetDetails(int id);
        Task<Example> Get(int id);
        Task<ExampleType> GetType(int id);
        Task<ExampleState> GetState(int id);
        Task Create(Example example);
    }
}
