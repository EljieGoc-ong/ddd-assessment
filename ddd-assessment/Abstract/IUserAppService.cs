using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.DataContracts
{
    public interface IUserAppService
    {
        bool AddMoney(AddMoneyModel money);
        bool AddNewUser(User uName);
        BalanceModel userBalance(int id);
        BalanceModel ExchangeMoney(MoneyExchange me);
        bool SendMoney(SendMoney sendMoney);
    }
}
