using System;
using System.Threading.Tasks;
using Account.Service.Core;
using Account.Service.Core.Enums;
using Account.Service.Models;

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
                    await _accountRepository.InsertSingle(account);
                    break;
                case OperationType.Update:
                    await _accountRepository.UpdateSingle(account);
                    break;
                case OperationType.Delete:
                    await _accountRepository.DeleteSingle(account);
                    break;
                case OperationType.Read:
                    await _accountRepository.SelectById(account);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
