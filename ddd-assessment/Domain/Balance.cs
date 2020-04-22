using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public class Balance : IBalance
    {
        private Dictionary<CurrencyModel, decimal?> currency = new Dictionary<CurrencyModel, decimal?>();

        public decimal MoneyRatioConvert(List<BalanceModel> data, decimal firstCurrencyRatio)
        {
            decimal amountValue = 0; 
            foreach (var d in data)
            {
                amountValue += d.Amount * (d.Ratio / firstCurrencyRatio);
            }

            return amountValue;

        }

        public decimal? ExchangeMoney(List<BalanceModel> data, int? toCurrency, int? fromCurrency)
        {
            decimal? amountValue = 0;
            foreach (var d in data)
            {
                amountValue += d.Amount * (toCurrency.GetValueOrDefault(1) / fromCurrency.GetValueOrDefault(1));
            }
            return amountValue;
        }
    }
}
