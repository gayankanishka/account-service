using System.Threading.Tasks;
using Account.Service.Core.Models;

namespace Account.Service.Business
{
    public interface IAccountBusiness
    {
        Task RoutOperation(AccountDto account);
    }
}
