using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OnlineShop.Application.Exceptions;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Behaviores
{
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var validationResult = await validators.First().ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = Serialize(validationResult.Errors);
                    throw new BadRequestException(Messages.BadRequest, errors);
                }
            }

            return await next(cancellationToken);
        }




        private static Dictionary<string, string[]> Serialize(List<ValidationFailure> validationFailures)
        {
            var errors = validationFailures
                .GroupBy(validationFailure => validationFailure.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(validationFailure => validationFailure.ErrorMessage).ToArray()
                );

            return errors;
        }
    }
}
