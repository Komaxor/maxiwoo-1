using System.Linq;
using Mxc.Commands.Abstractions.Commands;

namespace Mxc.IBSDiscountCard.Common
{
    public class IBSDiscountCardExecutionResult<TResult> : ExecutionResult<TResult,FunctionCodes,IBSDiscountCardExecutionError>
    {
        public IBSDiscountCardExecutionResult(bool isSuccess, TResult result) : base(isSuccess, result)
        {
        }

        public IBSDiscountCardExecutionResult(IBSDiscountCardExecutionError error) : base(error)
        {
        }

        public static IExecutionResult<TResult, FunctionCodes, IBSDiscountCardExecutionError> FromResult(TResult result)
        {
            return new ExecutionResult<TResult, FunctionCodes, IBSDiscountCardExecutionError>(true, result);
        }
        
        public static IExecutionResult<TResult, FunctionCodes, IBSDiscountCardExecutionError> FromError(IBSDiscountCardDomainException exception)
        {
            return new ExecutionResult<TResult, FunctionCodes, IBSDiscountCardExecutionError>(new IBSDiscountCardExecutionError(exception.FunctionCode, exception.ErrorLabels.ToArray()));
        }
    }
}
