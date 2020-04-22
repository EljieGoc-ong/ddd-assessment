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
using ddd_assessment.Domain;

namespace ddd_assessment.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserDatamanager _dataManager;
        private readonly IMoney _money;
        private readonly IBalance _balance;
        
        public UserAppService
        (
            IUserDatamanager datamanager,
            IMoney money,
            IBalance balance
        )
        {
            _dataManager = datamanager;
            _money = money;
            _balance = balance;
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
                        result = _dataManager.Updatebalance(balanceData.BalanceId, _money.AddMoneyToBalance(balanceData.Amount, money.Amount));
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
            var balance = new Balance();

            try
            {
                data = _dataManager.userBalanceGet(id);
                if (data != null && data.Count > 0)
                {
                    var firstCurrency = data.FirstOrDefault();
                    decimal amountValue = 0;
                    if (firstCurrency.Ratio > 0) {

                        amountValue = _balance.MoneyRatioConvert(data, firstCurrency.Ratio);

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


            try
            {
                data = _dataManager.userBalanceGet(me.UserId);
                fromCurrency = _dataManager.CurrencyGet(me.FromCurrencyId);
                toCurrency = _dataManager.CurrencyGet(me.ToCurrencyId);

                if (data != null && data.Count > 0)
                {
                    var amountValue = _balance.ExchangeMoney(data, toCurrency.Id, fromCurrency.Id);
                    _dataManager.DeleteUserBalance(me.UserId);
                    _dataManager.InsertNewbalance(me.UserId, toCurrency.Id, amountValue);
                    remainingbalanceResult = _dataManager.userBalanceGet(me.UserId).FirstOrDefault();
                }

            }
            catch
            {
                throw;
            }
            return remainingbalanceResult;
        }

        public bool SendMoney(SendMoney sendMoney)
        {

            decimal remainingAmount = 0;
            decimal fromUserAmountValue = 0;
            decimal toUserAmountValue = 0;
            var result = false;

            try
            {
                var fromUser = _dataManager.userBalanceGet(sendMoney.FromUserId);
                var toUser = _dataManager.userBalanceGet(sendMoney.ToUserId);
                var fromUserRatio = _dataManager.CurrencyGet(sendMoney.CurrencyId)?.Ratio;
                var fromUserCount = fromUser != null ? fromUser.Count() : 0;


                if (fromUserRatio > 0)
                {
                    fromUserAmountValue = _balance.MoneyRatioConvert(fromUser, (decimal)fromUserRatio);
                    toUserAmountValue = _balance.MoneyRatioConvert(toUser, (decimal)fromUserRatio);
                }

                if (sendMoney.Amount >= fromUserAmountValue && sendMoney.Amount != 0)
                {
                    remainingAmount = fromUserAmountValue - sendMoney.Amount;

                    if (fromUserCount > 1)
                    {
                        _dataManager.DeleteUserBalance(sendMoney.FromUserId);
                        _dataManager.InsertNewbalance(sendMoney.FromUserId, sendMoney.CurrencyId, remainingAmount);
                        result = true;
                    }
                    else if (remainingAmount > 0)
                    {
                        _dataManager.Updatebalance(fromUser.FirstOrDefault().BalanceId, remainingAmount);
                        result = true;
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
