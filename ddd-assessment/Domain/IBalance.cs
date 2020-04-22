using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public interface IBalance
    {
        decimal MoneyRatioConvert(List<BalanceModel> data, decimal firstCurrencyRatio);
        decimal? ExchangeMoney(List<BalanceModel> data, int? toCurrency, int? fromCurrency);
    }
}
