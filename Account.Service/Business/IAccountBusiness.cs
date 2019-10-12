using System.Threading.Tasks;
using Account.Service.Models;

namespace Account.Service.Business
{
    public interface IAccountBusiness
    {
        Task RoutOperation(AccountDto account);
    }
}
