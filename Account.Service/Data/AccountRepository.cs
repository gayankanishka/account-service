using Dapper;
using System.Collections.Generic;
using System.Data;
using Account.Service.Core;
using Account.Service.Core.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Account.Service.Data
{
    public class AccountRepository : IRepository<AccountDto>
    {
        #region Variables

        private readonly IDbConnection _dbConnection;

        #endregion

        #region Constructor

        public AccountRepository(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        #endregion

        #region Methods

        public async Task<bool> Add(AccountDto entity)
        {
            
            const string sql = "INSERT INTO [dbo].[Accounts] ([Name], [Email]) VALUES (@Name ,@Email)";
            int count = await _dbConnection.ExecuteAsync(sql, entity);
            return count > 0;
        }

        public async Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AccountDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<AccountDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(AccountDto entity)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
