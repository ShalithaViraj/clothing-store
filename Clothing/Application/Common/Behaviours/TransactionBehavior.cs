
using Clothing.Application.Common.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clothing.Application.Common.Behaviours
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IClothingDBContext _clothingDBContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IClothingDBContext FOFContext)
        {
            _logger = logger;
            _clothingDBContext = FOFContext;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                await _clothingDBContext.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin Transaction : {typeof(TRequest).Name}");
                    await _clothingDBContext.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _clothingDBContext.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation($"End transaction : {typeof(TRequest).Name}");
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");
                await _clothingDBContext.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex.Message, ex.StackTrace);

                throw;
            }

            return response;
        }
    }
}