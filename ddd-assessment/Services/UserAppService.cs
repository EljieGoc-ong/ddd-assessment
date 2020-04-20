using Dapper;
using ddd_assessment.DataContracts;
using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ddd_assessment.DataManager;


namespace ddd_assessment.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserDatamanager _dataManager;
        public UserAppService
        (
            IUserDatamanager datamanager
        )
        {
            _dataManager = datamanager;
        }

        CurrencyModel currency = new CurrencyModel();
        MoneyModel money = new MoneyModel();
        private IUserAppService object1;
        private IUserDataManager object2;

        public bool AddMoney(AddMoneyModel moneyData)
        {
            var currencyData = new CurrencyModel();
            var balanceData = new BalanceModel();
            var result = false;
            money.Amount = moneyData.Money;

            currencyData = _dataManager.CurrencyGet(moneyData.CurrencyId);
            balanceData = _dataManager.BalanceGet(moneyData.UserId, moneyData.CurrencyId);

            try
            {
                
                if (moneyData.UserId == null)
                {
                    if (currencyData != null && !string.IsNullOrEmpty(currencyData.Name) && balanceData.BalanceId > 0)
                    {
                        money.Amount = balanceData.Amount + money.Amount;
                        result = _dataManager.Updatebalance(balanceData.BalanceId, money.Amount);
                    }
                    else
                    {
                        result = _dataManager.InsertNewbalance(moneyData.UserId, (currencyData != null ? currencyData.CurrencyId : moneyData.CurrencyId), money.Amount);
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool AddNewUser(User uName)
        {
            var result = false;
            try
            {
                result = _dataManager.AddNewUser(uName.UserName);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public BalanceModel userBalance(int id)
        {
            var data = new List<BalanceModel>();
            var result = new BalanceModel();

            try
            {
                data = _dataManager.userBalanceGet(id);
                if (data != null && data.Count > 0)
                {
                    var firstCurrency = data.FirstOrDefault();
                    decimal amountValue = 0;
                    if (firstCurrency.Ratio > 0) {
                        foreach (var d in data)
                        {
                            amountValue += d.Amount * (d.Ratio / firstCurrency.Ratio);
                        }
                        result.Amount = amountValue;
                        result.CurrencyId = firstCurrency.CurrencyId;
                        result.CurrencyName = firstCurrency.CurrencyName;
                        result.Ratio = firstCurrency.Ratio;
                        result.UserId = firstCurrency.UserId;
                        result.BalanceId = firstCurrency.BalanceId;
                    }
                }

            }
            catch(Exception)
            {
                throw;
            }
            return result;
        }

        public BalanceModel ExchangeMoney(MoneyExchange me)
        {
            var data = new List<BalanceModel>();
            var fromCurrency = new CurrencyModel();
            var toCurrency = new CurrencyModel();
            var remainingbalanceResult = new BalanceModel();


            data = _dataManager.userBalanceGet(me.UserId);
            fromCurrency = _dataManager.CurrencyGet(me.FromCurrencyId);
            toCurrency = _dataManager.CurrencyGet(me.ToCurrencyId);

            if (data != null && data.Count > 0)
            {
                decimal amountValue = 0;
                foreach (var d in data)
                {
                    amountValue += d.Amount * (toCurrency.Id / fromCurrency.Id);
                }

                _dataManager.DeleteUserBalance(me.UserId);
                _dataManager.InsertNewbalance(me.UserId, toCurrency.Id, amountValue);
                remainingbalanceResult = _dataManager.userBalanceGet(me.UserId).FirstOrDefault();
            }
            return remainingbalanceResult;
        }

        //public bool ChargeMoney(ChargeMoney moneyData)
        //{
        //    currency.Name = !string.IsNullOrEmpty(moneyData.CurrencyName) ? moneyData.CurrencyName : Name;

        //    try
        //    {
        //        if (currencies.ContainsKey(money.getCurrency()))
        //            currencies[money.getCurrency()] = currencies[money.getCurrency()] - money.getAmount();
        //        else
        //            currencies[money.getCurrency()] = currencies[money.getCurrency()] * (-1);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
