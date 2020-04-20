using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public class Balance
    {
        public Balance()
        {
        }

        private Dictionary<CurrencyModel, decimal?> currency = new Dictionary<CurrencyModel, decimal?>();

        public virtual void AddMoney(MoneyModel money)
        {
            if (currency.ContainsKey(money.Currency))
            {
                currency[money.Currency] = (currency.GetValueOrDefault(money.Currency) + money.Amount);
            }
            else
            {
                currency.Add(money.Currency, money.Amount);
            }
        }
    }
}
