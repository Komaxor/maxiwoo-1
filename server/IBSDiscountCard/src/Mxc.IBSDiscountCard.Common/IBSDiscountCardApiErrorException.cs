using System;
using Mxc.Helpers.Errors;

namespace Mxc.IBSDiscountCard.Common
{
    public class IBSDiscountCardApiErrorException : ApiErrorException<FunctionCodes>
    {
        public FunctionCodes FunctionCode { get; }

        public IBSDiscountCardApiErrorException(FunctionCodes functionCode) : base(functionCode)
        {
            FunctionCode = functionCode;
        }

        public IBSDiscountCardApiErrorException(FunctionCodes functionCode, params string[] messages) : base(functionCode, messages)
        {
            FunctionCode = functionCode;
        }

        public IBSDiscountCardApiErrorException(FunctionCodes functionCode, params ApiErrorLabel[] labels) : base(functionCode, labels)
        {
            FunctionCode = functionCode;
        }

        public IBSDiscountCardApiErrorException(string message, Exception exception, FunctionCodes functionCode, params ApiErrorLabel[] apiMessages) : base(message, exception, functionCode, apiMessages)
        {
            FunctionCode = functionCode;
        }
    }
}
