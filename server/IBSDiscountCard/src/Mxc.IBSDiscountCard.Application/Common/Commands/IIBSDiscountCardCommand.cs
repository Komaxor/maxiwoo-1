using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Common.Commands
{
    /// <summary>
    /// IBSDiscountCard command interface
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class IIBSDiscountCardCommand<TCommand> : ICommand<TCommand, FunctionCodes, IBSDiscountCardExecutionError>
    {
    }
}
