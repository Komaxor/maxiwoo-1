using Mxc.IBSDiscountCard.Common;
using Mxc.WebApi.Abstractions.Controller.Attributes;

namespace Mxc.IBSDiscountCard.WebApi.Attributes
{
    public class ValidateModelAttribute : AbstractValidateModelAttribute
    {
        private readonly FunctionCodes _functionCode;

        public ValidateModelAttribute(FunctionCodes functionCode)
        {
            _functionCode = functionCode;
        }

        public override int GetFunctionCode()
        {
            return (int)_functionCode;
        }
    }
}
