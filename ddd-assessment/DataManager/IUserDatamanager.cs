using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.DataManager
{
    public interface IUserDatamanager
    {
        CurrencyModel CurrencyGet(int? currencyId);
        BalanceModel BalanceGet(int? userId, int? currencyId);
        bool InsertNewbalance(int? userId, int? currencyId, decimal? amount);
        bool Updatebalance(int? balanceId, decimal? amount);
        bool AddNewUser(string userName);
        List<BalanceModel> userBalanceGet(int userId);
        bool DeleteUserBalance(int? userId);
    }
}
