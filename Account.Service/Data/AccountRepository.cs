using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Account.Service.Models;
using Core.Helpers;

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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", entity.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@Email", entity.Email, DbType.String, ParameterDirection.Input);

                int count = await connection.ExecuteAsync(InsertAccount, parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<AccountDto> SelectById(long id)
        {
            using (IDbConnection connection = CreateDbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

                var result = await connection.QueryAsync<AccountDto>(SelectAccountById, parameters, commandType: CommandType.StoredProcedure);
                return result?.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateSingle(AccountDto entity)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (IDbConnection connection = CreateDbConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", entity.Id, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Name", entity.Name, DbType.String, ParameterDirection.Input);
                    parameters.Add("@Email", entity.Email, DbType.String, ParameterDirection.Input);

                    int count = await connection.ExecuteAsync(UpdateAccount, parameters, commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);
                    ts.Complete();
                    return count > 0;
                }
            }
        }

        public async Task<bool> DeleteSingle(long id)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (IDbConnection connection = CreateDbConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

                    int count = await connection.ExecuteAsync(DeleteAccount, parameters, commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);
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
