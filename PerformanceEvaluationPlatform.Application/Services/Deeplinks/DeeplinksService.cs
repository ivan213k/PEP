using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Application.Interfaces.Deeplinks;
using PerformanceEvaluationPlatform.Application.Model.Deeplinks;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Deeplinks;

namespace PerformanceEvaluationPlatform.Application.Services.Deeplinks
{
    public class DeeplinksService : IDeeplinksService
    {
        private readonly IDeeplinksRepository _deeplinkRepository;
        public DeeplinksService(IDeeplinksRepository deeplinkRepository)
        {
            _deeplinkRepository = deeplinkRepository;
        }

        public async Task<ServiceResponse<IList<DeeplinkListItemDto>>> GetList(DeeplinkListFilterDto filter)
        {
            IList<DeeplinkListItemDto> items = await _deeplinkRepository.GetList(filter);
            return ServiceResponse<IList<DeeplinkListItemDto>>.Success(items);
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

       // public Task<ServiceResponse<DeeplinkState>> GetState(int id)
      //  {
          //  var item = _deeplinkRepository.GetState(id);
          //  return ServiceResponse<DeeplinkState>.Success(item);

       // }

        public async Task<ServiceResponse<int>> Create(CreateDeeplinkDto model)
        {            
            if (model == null)
            {
                ServiceResponse.BadRequest();
            }
            //Exist?
            // User To
           /* var userTo = await _usersRepository.Get(model.UserId);
            if (userTo == null)
            {
                ServiceResponse.Failure<CreateDeeplinkDto>(m => m.UserId, "User does not exists.");
            }
            // User by
            var userBy = await _usersRepository.Get(model.SentById);
            if (userBy == null)
            {
                ServiceResponse.Failure<CreateDeeplinkDto>(m => m.SentById, "UserSentBy does not exists.");
            }

            //  Survey
            var survey = await _surveysRepository.Get(model.SurveyId);
            if (survey == null)
            {
                ServiceResponse.Failure<CreateDeeplinkDto>(m => m.SurveyId, "Survey does not exists.");
            }*/
            // ID Users
            if (ContainsSameUserIds(model.UserId, model.SentById))
            {
                ServiceResponse.BadRequest("Contains same user id");
            }

            var deeplink = new Deeplink
            {
                StateId = GetNewStateId(),
                UserId = model.UserId,
                SentById = model.SentById,
                ExpireDate = model.ExpiresDate,
                SurveyId = model.SurveyId,
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
        private bool ContainsSameUserIds(int SentToId, int SentById)
        {
            return (SentToId == SentById);
        }
        private int GetNewStateId()
        {
            return 1;
        }
    }
}
 