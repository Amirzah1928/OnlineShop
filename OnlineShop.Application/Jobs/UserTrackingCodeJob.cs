using MediatR;
using OnlineShop.Application.Commands.User.SetTrackingCodes;

namespace OnlineShop.Application.Jobs
{
    public class UserTrackingCodeJob(IMediator mediator)
    {
        public async Task Get()
        {
            var command = new SetTrackingCodesCommand();
            await mediator.Send(command);
        }
    }
}
