using Mxc.Domain.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    public class AuditLogEntry : ValueObject
    {
        public DateTimeOffset CreateDate { get; }
        public string OperationType { get; }
        public string OperationDescription { get; }

        public AuditLogEntry(DateTimeOffset createDate, string operationType, string operationDescription)
        {
            CreateDate = createDate;
            OperationType = operationType;
            OperationDescription = operationDescription;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CreateDate;
            yield return OperationType;
            yield return OperationDescription;
        }
    }
}
