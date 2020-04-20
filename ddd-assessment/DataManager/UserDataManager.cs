using Dapper;
using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.DataManager
{
    public class IUserDataManager : IUserDatamanager
    {
        private decimal Money = 0;
        private string Name = "usd";
        private string dataUpdate = $"UPDATE dbo.Balance SET Amount = @amount WHERE BalanceId = @balanceId";
        private string dataInsert = $"INSERT INTO [Balance] (UserId,CurrencyId,Amount)VALUES(@userId,@currencyId,@amount)";
        private string currencySql = $"SELECT CurrencyId, Name, Ratio FROM dbo.Currency WHERE CurrencyId = @CurrencyId";
        private string balanceSQL = $"SELECT BalanceId, UserId, CurrencyId, Amount FROM dbo.Balance WHERE UserId = @userId AND CurrencyId = @currencyId";
        private string addNewUserSQL = $"INSERT INTO dbo.[User] ( Username ) VALUES ( @userName )";
        private string userBalanceSQL = @"SELECT b.BalanceId,
                                                   b.UserId,
                                                   u.Username,
                                                   b.CurrencyId,
                                                   c.Name,
                                                   b.Amount,
                                                   c.Ratio
                                            FROM dbo.Balance b
                                                INNER JOIN dbo.Currency c
                                                    ON b.CurrencyId = c.CurrencyId
                                                INNER JOIN dbo.[User] u
                                                    ON b.UserId = u.UserId
                                            WHERE u.UserId = @userId;";
        private string deleteUserBalanceSQL = $"DELETE FROM dbo.Balance WHERE UserId = @userId";

        protected IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(Utils.DBInitializer.DbConnection);
            conn.Open();
            return conn;
        }

        public CurrencyModel CurrencyGet(int? currencyId)
        {
            var currencyData = new CurrencyModel();
            using (var connection = CreateConnection())
            {
                currencyData = connection.Query<CurrencyModel>(currencySql, new { currencyId = currencyId }).FirstOrDefault();
            }

            return currencyData;

        }

        public BalanceModel BalanceGet(int? userId, int? currencyId)
        {
            var balanceData = new BalanceModel();
            using (var connection = CreateConnection())
            {
                balanceData = connection.Query<BalanceModel>(balanceSQL, new { userId = userId, currencyId = currencyId }).FirstOrDefault();
            }

            return balanceData;
        }

        public bool InsertNewbalance(int? userId , int? currencyId ,decimal? amount )
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Execute(dataInsert, new { UserId = userId, CurrencyId = currencyId, Amount = amount });
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            
            }
        }

        public bool Updatebalance(int? balanceId, decimal? amount)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Execute(dataUpdate, new { BalanceId = balanceId, Amount = amount });
                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public bool AddNewUser(string userName)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Execute(addNewUserSQL, new { userName = userName });
                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public List<BalanceModel> userBalanceGet(int userId)
        {
            try
            {
                var data = new List<BalanceModel>();
                using (var connection = CreateConnection())
                {
                    data = connection.Query<BalanceModel>(userBalanceSQL, new { UserId = userId }).ToList();
                }
                return data;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public bool DeleteUserBalance(int? userId)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Execute(deleteUserBalanceSQL, new { UserId = userId });
                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }


    }
}
