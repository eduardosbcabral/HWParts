using HWParts.Core.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Core.Commands
{
    public class TransactionalRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : CommandResponse
    {
        private readonly HWPartsDbContext _context;

        public TransactionalRequestBehavior(HWPartsDbContext context) =>
            _context = context;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var transaction = await BeginTransaction();

            try
            {
                var response = await next();
                if (response.Valid)
                {
                    await Commit(transaction);
                    return response;
                }

                await Rollback(transaction);
                return response;
            }
            catch (Exception)
            {
                await Rollback(transaction);
                var response = new ErrorCommandResponse();
                response.AddNotification(null, "Ocorreu um erro na requisição.");
                return (TResponse)(CommandResponse)response;
            }
        }

        private async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        private async Task Commit(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }

        private async Task Rollback(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }
    }
}
