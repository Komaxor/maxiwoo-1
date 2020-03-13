using System.Collections.Generic;
using System.Linq;
using Mxc.Commands.Abstractions.Commands;

namespace Mxc.IBSDiscountCard.Common
{
    public class IBSDiscountCardExecutionError : ExecutionError<FunctionCodes>
    {
        public IBSDiscountCardExecutionError(string errorLabel, FunctionCodes functionCode) : base(errorLabel, functionCode)
        {
        }

        public IBSDiscountCardExecutionError(FunctionCodes functionCode, params string[] errorLabels) : base(functionCode, errorLabels)
        {
        }

        public IBSDiscountCardApiErrorException ToApiErrorException()
        {
            return new IBSDiscountCardApiErrorException(FunctionCode, ErrorLabels);
        }
    }
}
