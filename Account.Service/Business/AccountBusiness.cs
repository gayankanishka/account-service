using System;
using System.Threading.Tasks;
using Core.Enums;
using Account.Service.Models;
using Core.Helpers;

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
                case DataBaseOperationType.Create:
                    await _accountRepository.InsertSingle(account);
                    break;
                case DataBaseOperationType.Update:
                    await _accountRepository.UpdateSingle(account);
                    break;
                case DataBaseOperationType.Delete:
                    await _accountRepository.DeleteSingle(account);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
