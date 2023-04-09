﻿using FluentValidation;
using MediatR;

namespace Shared.PipelineBehaviors
{

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(s => s.Validate(context))
                .SelectMany(s => s.Errors)
                .Where(w => w != null).ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }

    ///// <summary>
    ///// Mediatr behavior pipeline.This will execute before Mediatr calls any handler.
    ///// </summary>
    ///// <typeparam name="TRequest">The type of the request.</typeparam>
    ///// <typeparam name="TResponse">The type of the response.</typeparam>
    ///// <seealso cref="MediatR.IPipelineBehavior&lt;TRequest, TResponse&gt;" />
    //public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //    where TRequest : IRequest<ApiResponse<TResponse>>
    //    where TResponse : ApiResponse<TResponse>
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;
    //    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        if (!_validators.Any())
    //        {
    //            return await next();
    //        }
    //        var context = new ValidationContext<TRequest>(request);

    //        var failures = _validators
    //            .Select(s => s.Validate(context))
    //            .SelectMany(s => s.Errors)
    //            .Where(w => w != null).ToList();

    //        if (failures.Any())
    //        {
    //            throw new ValidationException(failures);
    //        }

    //        return await next();
    //    }
    //}
}
