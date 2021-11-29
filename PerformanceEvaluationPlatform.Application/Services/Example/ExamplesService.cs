using PerformanceEvaluationPlatform.Application.Interfaces.Examples;
using PerformanceEvaluationPlatform.Application.Model.Examples;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Application.Services.Example
{
    public class ExamplesService : IExamplesService
    {
        private readonly IExamplesRepository _examplesRepository;

        public ExamplesService(IExamplesRepository examplesRepository)
        {
            _examplesRepository = examplesRepository;
        }

        public async Task<ServiceResponse<IList<ExampleListItemDto>>> GetListItems(ExampleListFilterDto filter)
        {
	        IList<ExampleListItemDto> items = await _examplesRepository.GetList(filter);
	        return ServiceResponse<IList<ExampleListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<IList<ExampleTypeListItemDto>>> GetTypeListItems() 
        {
	        var items =  await _examplesRepository.GetTypesList();
	        return ServiceResponse<IList<ExampleTypeListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<IList<ExampleStateListItemDto>>> GetStateListItems() 
        {
			var items = await _examplesRepository.GetStatesList();
			return ServiceResponse<IList<ExampleStateListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<ExampleDetailsDto>> GetDetails(int id) {
	        var details = await _examplesRepository.GetDetails(id);
			return details == null 
				? ServiceResponse<ExampleDetailsDto>.NotFound() 
				: ServiceResponse<ExampleDetailsDto>.Success(details);
        }

        public async Task<ServiceResponse> Update(int id, UpdateExampleDto model) {
	        var entity = await _examplesRepository.Get(id);
	        if (entity == null)
	        {
		        return ServiceResponse.NotFound();
	        }

	        var exampleType = await _examplesRepository.GetType(model.TypeId);
	        if (exampleType == null)
	        {
				return ServiceResponse.Failure<UpdateExampleDto>(t => t.TypeId, "Type does not exists.");
	        }

	        var exampleState = await _examplesRepository.GetState(model.StateId);
	        if (exampleState == null)
	        {
		        return ServiceResponse.Failure<UpdateExampleDto>(t => t.StateId, "Type does not exists.");
	        }

	        entity.Title = model.Title;
	        entity.ExampleTypeId = model.TypeId;
	        entity.ExampleStateId = model.StateId;

	        await _examplesRepository.SaveChanges();
	        return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<int>> Create(CreateExampleDto model) {
	        var exampleType = await _examplesRepository.GetType(model.TypeId);
	        if (exampleType == null)
	        {
		        ServiceResponse.Failure<CreateExampleDto>(t => t.TypeId, "Type does not exists.");
	        }

	        var exampleState = await _examplesRepository.GetState(model.StateId);
	        if (exampleState == null)
	        {
		        ServiceResponse.Failure<CreateExampleDto>(t => t.StateId, "State does not exists.");
	        }

	        var example = new Domain.Examples.Example
	        {
		        Title = model.Title,
		        ExampleState = exampleState,
		        ExampleType = exampleType
	        };

	        await _examplesRepository.Create(example);
	        await _examplesRepository.SaveChanges();

			return ServiceResponse<int>.Success(example.Id, 201);
        }
    }
}
