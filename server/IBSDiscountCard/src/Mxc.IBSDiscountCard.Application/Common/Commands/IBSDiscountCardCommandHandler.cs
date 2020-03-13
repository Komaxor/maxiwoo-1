using MediatR;
using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.Domain.Abstractions.Entities;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.Common.Commands
{
    /// <summary>
    /// IBSDiscountCard Command Handle
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class IBSDiscountCardCommandHandler<TCommand, TResult> : CommandHandler<TCommand, TResult, FunctionCodes, IBSDiscountCardExecutionError> 
        where TCommand : IRequest<IExecutionResult<TResult, FunctionCodes, IBSDiscountCardExecutionError>>
    {
        public IBSDiscountCardCommandHandler(ILogger<CommandHandler<TCommand, TResult, FunctionCodes, IBSDiscountCardExecutionError>> logger) : base(logger)
        {
        }
#pragma warning disable RCS1046
        public override async Task<IExecutionResult<TResult, FunctionCodes, IBSDiscountCardExecutionError>> Handle(TCommand request, CancellationToken cancellationToken)
#pragma warning disable RCS1046
        {
            try
            {
                return await base.Handle(request, cancellationToken);
            }
            catch (IBSDiscountCardDomainException dex)
            {
                Logger.LogWarning($"Domain exception during the handling of command {_commandName}");

                return IBSDiscountCardExecutionResult<TResult>.FromError(dex);
            }
        }
    }
}
