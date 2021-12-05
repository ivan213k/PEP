using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Interfaces.Deeplinks;
using PerformanceEvaluationPlatform.Application.Interfaces.Surveys;
using PerformanceEvaluationPlatform.Application.Interfaces.Users;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Deeplinks;

namespace PerformanceEvaluationPlatform.Application.Services.Deeplinks
{
    public class DeeplinksService : IDeeplinksService
    {
        private readonly IDeeplinksRepository _deeplinkRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISurveysRepository _surveysRepository;
        public DeeplinksService(IDeeplinksRepository deeplinkRepository, IUserRepository userRepository, ISurveysRepository surveysRepository)
        {
            _deeplinkRepository = deeplinkRepository;
            _userRepository = userRepository;
            _surveysRepository = surveysRepository;
        }

        public async Task<ServiceResponse<ListItemsDto<DeeplinkListItemDto>>> GetList(DeeplinkListFilterDto filter)
        {
            ListItemsDto<DeeplinkListItemDto> items = await _deeplinkRepository.GetList(filter);
            return ServiceResponse<ListItemsDto<DeeplinkListItemDto>>.Success(items);
        }


        public async Task<ServiceResponse<IList<DeeplinkStateListItemDto>>> GetStatesList()
        {
            var items = await _deeplinkRepository.GetStatesList();
            return ServiceResponse<IList<DeeplinkStateListItemDto>>.Success(items);
        }

        public async Task<ServiceResponse<DeeplinkDetailsDto>> GetDetails(int id)
        {
            var details = await _deeplinkRepository.GetDetails(id);
            return details == null
                ? ServiceResponse<DeeplinkDetailsDto>.NotFound()
                : ServiceResponse<DeeplinkDetailsDto>.Success(details);

        }

        public async Task<ServiceResponse<int>> Create(CreateDeeplinkDto createDeeplinkDto)
        {            
            if (createDeeplinkDto == null)
            {
                ServiceResponse.BadRequest();
            }

            var sentToUser = await _userRepository.Get(createDeeplinkDto.UserId);
            if (sentToUser is null)
            {
                ServiceResponse.Failure<CreateDeeplinkDto>(m => m.UserId, "User does not exists.");
            }
            
            var sentByUser = await _userRepository.Get(createDeeplinkDto.SentById);
            if (sentByUser == null)
            {
                ServiceResponse.Failure<CreateDeeplinkDto>(m => m.SentById, "Sent by user does not exists.");
            }

            var survey = await _surveysRepository.Get(createDeeplinkDto.SurveyId);
            if (survey == null)
            {
                ServiceResponse.Failure<CreateDeeplinkDto>(m => m.SurveyId, "Survey does not exists.");
            }
            
            if (createDeeplinkDto.UserId == createDeeplinkDto.SentById)
            {
                ServiceResponse.BadRequest("Sent by and sent to users are same");
            }

            var deeplink = new Deeplink
            {
                StateId = GetNewStateId(),
                UserId = createDeeplinkDto.UserId,
                SentById = createDeeplinkDto.SentById,
                ExpireDate = createDeeplinkDto.ExpiresDate,
                SurveyId = createDeeplinkDto.SurveyId,
                Code = Guid.NewGuid(),
                SentAt = DateTime.Today

            };

            await _deeplinkRepository.Create(deeplink);
            await _deeplinkRepository.SaveChanges();

            return ServiceResponse<int>.Success(deeplink.Id, 201);
        }


        public async Task<ServiceResponse> Update(int id, UpdateDeeplinkDto model)
        {

            var entity = await _deeplinkRepository.Get(id);
	        if (entity == null)
	        {
		        return ServiceResponse.NotFound();
	        }
            if (model.ExpiresAt <= DateTime.Today)
            {
                return ServiceResponse.BadRequest();
            }

            entity.ExpireDate = model.ExpiresAt;

            await _deeplinkRepository.SaveChanges();
            return ServiceResponse.Success();

        }
        private int GetNewStateId()
        {
            return 1;
        }
    }
}
 