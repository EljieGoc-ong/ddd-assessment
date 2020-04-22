using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ddd_assessment.DataContracts;
using ddd_assessment.Models;


namespace ddd_assessment.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [Route("add-money")]
        [HttpPost]
        public bool AddMoney([FromBody]AddMoneyModel money)
        {
            var data = _userAppService.AddMoney(money);
            return data;
        }

        [Route("add-new-user")]
        [HttpPost]
        public bool AddNewUser([FromBody]User uName)
        {
            var data = _userAppService.AddNewUser(uName);
            return data;
        }

        [Route("user-balance")]
        [HttpGet]
        public BalanceModel userBalance([FromQuery]int id)
        {
            var data = _userAppService.userBalance(id);
            return data;
        }

        [Route("user-exchange-money")]
        [HttpPost]
        public BalanceModel ExchangeMoney(MoneyExchange me)
        {
            var data = _userAppService.ExchangeMoney(me);
            return data;
        }

        [Route("user-send-money")]
        [HttpPost]
        public bool SendMoney(SendMoney sendMoney)
        {
            var data = _userAppService.SendMoney(sendMoney);
            return data;
        }
    }
}
