using AutoFixture;
using ddd_assessment.Controllers;
using ddd_assessment.DataContracts;
using ddd_assessment.DataManager;
using ddd_assessment.Models;
using ddd_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ddd_trading_engine
{
    [TestFixture]
    public class TradingEngineTest
    {
        [SetUp]
        public void Setup()
        {
        }

        private readonly Mock<IUserAppService> _userAppServiceMock;
        private readonly Mock<IUserDatamanager> _userDataManagerMock;
        public TradingEngineTest()
        {
            _userAppServiceMock = new Mock<IUserAppService>();
            _userDataManagerMock = new Mock<IUserDatamanager>();

        }

        private UserAppService Create() =>
            new UserAppService(_userDataManagerMock.Object);

        [Test]
        public void Add_Money_Should_Return_Type_of_Bool()
        {
            var fixture = new Fixture();
            var inputData = fixture.Create<AddMoneyModel>();
            var addMoneyResult = fixture.Create<bool>();
            var userId = fixture.Create<int>();
            var currencyId = fixture.Create<int>();
            var amount = fixture.Create<decimal>();
            var balanceId = fixture.Create<int>();

            var service = Create();
            _userDataManagerMock.Setup(x => x.InsertNewbalance(userId, currencyId, amount)).Returns(addMoneyResult);
            _userDataManagerMock.Setup(x => x.Updatebalance(balanceId, amount)).Returns(addMoneyResult);
            var response = service.AddMoney(inputData);

            Assert.That(response, Is.AssignableFrom(typeof(bool)));

        }

        [Test]
        public void Add_Money_Should_Return_false()
        {
            var fixture = new Fixture();
            var inputData = fixture.Create<AddMoneyModel>();
            var addMoneyResult = fixture.Create<bool>();

            var service = Create();
            _userDataManagerMock.Setup(x => x.InsertNewbalance(null, null, null)).Returns(addMoneyResult);
            _userDataManagerMock.Setup(x => x.Updatebalance(null, null)).Returns(addMoneyResult);
            var response = service.AddMoney(inputData);

            Assert.AreEqual(response, false);

        }

        [Test]
        public void User_Balance_Should_Return_Type_Of_BalanceModel()
        {
            var fixture = new Fixture();
            var inputData = fixture.Create<int>();
            var userBalanceGetResult = fixture.Create<List<BalanceModel>>();

            var service = Create();
            _userDataManagerMock.Setup(x => x.userBalanceGet(inputData)).Returns(userBalanceGetResult);
            var response = service.userBalance(inputData);

            Assert.That(response, Is.AssignableFrom(typeof(BalanceModel)));

        }

        [Test]
        public void Add_New_User_Should_Return_Type_oF_Bool()
        {
            var fixture = new Fixture();
            var inputData = fixture.Create<User>();
            var addUserResult = fixture.Create<bool>();

            var service = Create();
            _userDataManagerMock.Setup(x => x.AddNewUser(inputData.UserName)).Returns(addUserResult);
            var response = service.AddNewUser(inputData);

            Assert.AreEqual(response.GetType(), typeof(bool));

        }

        [Test]
        public void Ex_change_Money_Should_Return_Type_oF_Bool()
        {
            var fixture = new Fixture();
            var inputData = fixture.Create<MoneyExchange>();

            var exchangeMoneyResult = fixture.Create<BalanceModel>();

            var service = Create();
            _userAppServiceMock.Setup(x => x.ExchangeMoney(inputData)).Returns(exchangeMoneyResult);
            var response = service.ExchangeMoney(inputData);

            Assert.AreEqual(response.GetType(), typeof(BalanceModel));
        }
    }
}