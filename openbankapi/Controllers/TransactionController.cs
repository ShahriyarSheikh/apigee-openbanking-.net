using Microsoft.AspNetCore.Mvc;
using openbankapi.service.IService;
using openbankapi.core.Models;
using openbankapi.Models;
using System.Collections.Generic;

namespace openbankapi.Controllers
{
    /// <summary>
    /// https://openbank.apigee.io/home
    /// v1.0.1
    /// reference : https://github.com/apigee/openbank
    /// </summary>

    [Route("api/v1.0.0/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet, Route("transactions")]
        public IEnumerable<Transaction> GetTransactions(long toBookingDateTime, long fromBookingDateTime)
        {
            string accountNumber = "100";
            return _transactionService.GetTransactions(accountNumber, toBookingDateTime, fromBookingDateTime);
        }

        [HttpPost, Route("send")]
        public string PerformTransaction(ProcessTransaction transactionToSend)
        {
            return _transactionService.SendTransaction(transactionToSend.To, transactionToSend.From, transactionToSend.Amount);
        }

        [HttpGet, Route("balance")]
        public int GetBalance(string accNo)
        {
            return _transactionService.GetBalance(accNo);
        }
    }

}
