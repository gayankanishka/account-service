using Dapper;
using System.Collections.Generic;
using System.Data;
using Account.Service.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Account.Service.Models;

namespace Account.Service.Data
{
    public class AccountRepository : IRepository<AccountDto>
    {
        #region Variables

        public string ConnectionString { get; set; }

        private const string InsertAccount = "Insert_Account";
        private const string UpdateAccount = "Update_Account";
        private const string DeleteAccount = "Delete_Account";
        private const string SelectAllAccounts = "Select_All";
        private const string SelectAccountById = "Select_Single";

        #endregion

        #region Constructor

        public AccountRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #endregion

        #region Methods

        public async Task<bool> InsertSingle(AccountDto entity)
        {
            using (IDbConnection connection = CreateDbConnection())
            {
                int count = await connection.ExecuteAsync(InsertAccount, entity, commandType: CommandType.StoredProcedure);
                return count > 0;
            }
        }

        public async Task<IEnumerable<AccountDto>> SelectAll()
        {
            using (IDbConnection connection = CreateDbConnection())
            {
                return await connection.QueryAsync<AccountDto>(SelectAllAccounts, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<AccountDto> SelectById(AccountDto entity)
        {
            using (IDbConnection connection = CreateDbConnection())
            {
                var result = await connection.QueryAsync<AccountDto>(SelectAccountById, entity, commandType: CommandType.StoredProcedure);
                return result?.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateSingle(AccountDto entity)
        {
            using (var ts = new TransactionScope())
            {
                using (IDbConnection connection = CreateDbConnection())
                {
                    int count = await connection.ExecuteAsync(UpdateAccount, entity, commandType: CommandType.StoredProcedure);
                    ts.Complete();
                    return count > 0;
                }
            }
        }

        public async Task<bool> DeleteSingle(AccountDto entity)
        {
            using (var ts = new TransactionScope())
            {
                using (IDbConnection connection = CreateDbConnection())
                {
                    int count = await connection.ExecuteAsync(DeleteAccount, entity, commandType: CommandType.StoredProcedure);
                    ts.Complete();
                    return count > 0;
                }
            }
        }

        private IDbConnection CreateDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        #endregion
    }
}
