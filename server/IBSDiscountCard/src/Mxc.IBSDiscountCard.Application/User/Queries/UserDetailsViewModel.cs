using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Queries
{
    public class UserDetailsViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string InstituteId { get; set; }
        public string InstituteName { get; set; }
        public string ProfilePhoto { get; set; }
        public SubscriptionViewModel Subscription { get; set; }
    }
}
