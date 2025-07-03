
using MediatR;
using OnlineShop.Application.Events.User.TrackingCode;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Proxies;
using OnlineShop.DomainService.Specifications;

namespace OnlineShop.Application.Commands.User.SetTrackingCodes
{
    public class SetTrackingCodesCommandHandler(IUnitOfWork unitOfWork, ITrackingCodeProxy trackingCodeProxy, IMediator mediator) : IRequestHandler<SetTrackingCodesCommand>
    {
        public async Task Handle(SetTrackingCodesCommand request, CancellationToken cancellationToken)
        {
            var specification = new GetUserWithNoTrackinCodeSpecification();
            var (_, entities) = await unitOfWork.UserRepository.GetListAsync(specification, cancellationToken);

            List<string>? trackingCodes = [];

            if (entities.Count == 0)
                return;

            

            try
            {
                trackingCodes = await trackingCodeProxy.Get(entities.Count, cancellationToken);
            }

            catch
            {
                return; // In Real program Log will be here
            }

            if (trackingCodes.Count != entities.Count)
                return;

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].SetTrackingCode(trackingCodes[i]);
            }


            unitOfWork.UserRepository.Update(entities);
            await unitOfWork.CommitAsync(cancellationToken);


            await mediator.Publish(trackingCodes.ToEvent());
        }
    }
}
