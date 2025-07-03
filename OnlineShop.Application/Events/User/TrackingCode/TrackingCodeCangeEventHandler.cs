using MediatR;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Specifications;

namespace OnlineShop.Application.Events.User.TrackingCode
{
    public class TrackingCodeCangeEventHandler(IReadUnitOfWork readUnitOfWork) : INotificationHandler<TrackingCodeCangeEvent>
    {
        public async Task Handle(TrackingCodeCangeEvent notification, CancellationToken cancellationToken)
        {
            var specification = new GetUserWithNoTrackinCodeSpecification();
            var (_, entities) = await readUnitOfWork.UserRepository.GetListAsync(specification, cancellationToken);

            if (!entities.Any())
                return;



            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].SetTrackingCode(notification.TrackingCodes[i]);
            }

            readUnitOfWork.UserRepository.Update(entities);
            await readUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
