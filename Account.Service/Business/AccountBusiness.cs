using System;
using System.Threading.Tasks;
using Account.Service.Core;
using Account.Service.Core.Enums;
using Account.Service.Core.Models;

namespace Account.Service.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        #region Variables

        private readonly IRepository<AccountDto> _accountRepository;

        #endregion

        #region Constructor

        public AccountBusiness(IRepository<AccountDto> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        #endregion

        #region Methods

        public async Task RoutOperation(AccountDto account)
        {
            switch (account.OperationType)
            {
                case OperationType.Create:
                    await _accountRepository.Add(account);
                    break;
                case OperationType.Update:
                    break;
                case OperationType.Delete:
                    break;
                case OperationType.Read:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
