using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class Subscribe : IIBSDiscountCardCommand<object>
    {
        public string Description { get; set; }

        public string DeviceData { get; set; }

        public bool IsDefault { get; set; }

        public string Nonce { get; set; }

        public string Type { get; set; }
    }
}
