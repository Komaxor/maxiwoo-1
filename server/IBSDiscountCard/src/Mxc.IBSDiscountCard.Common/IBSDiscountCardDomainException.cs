using System;
using Mxc.Domain.Abstractions.Exceptions;

namespace Mxc.IBSDiscountCard.Common
{
    public class IBSDiscountCardDomainException : DomainException<FunctionCodes>
    {
        public IBSDiscountCardDomainException(FunctionCodes functionCode, params string[] errorLabels) : base(functionCode, errorLabels)
        {
        }

        public IBSDiscountCardDomainException(FunctionCodes functionCode, string[] errorLabels, string message) : base(functionCode, errorLabels, message)
        {
        }

        public IBSDiscountCardDomainException(FunctionCodes functionCode, string[] errorLabels, string message, Exception innerException) : base(functionCode, errorLabels, message, innerException)
        {
        }

        public IBSDiscountCardDomainException(FunctionCodes functionCode, System.Collections.Generic.IEnumerable<string> errorLabels) : base(functionCode, errorLabels)
        {
        }

        public IBSDiscountCardDomainException(FunctionCodes functionCode, System.Collections.Generic.IEnumerable<string> errorLabels, string message) : base(functionCode, errorLabels, message)
        {
        }

        public IBSDiscountCardDomainException(FunctionCodes functionCode, System.Collections.Generic.IEnumerable<string> errorLabels, string message, Exception innerException) : base(functionCode, errorLabels, message, innerException)
        {
        }
    }
}
