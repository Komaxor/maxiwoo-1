using System.Threading.Tasks;
using Mxc.IBSDiscountCard.Domain.UserAggregate;

namespace Mxc.IBSDiscountCard.Application.User.Queries
{
    public interface IUserQueries
    {
        Task<UserDetailsViewModel> GetMyProfileAsync();
    }
}