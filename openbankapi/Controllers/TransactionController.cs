using Microsoft.AspNetCore.Mvc;
using openbankapi.core.IService;
using openbankapi.core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet,Route("transactions")]
        public IEnumerable<Transaction> GetTransactions(string toBookingDateTime, string fromBookingDateTime) {
            return _transactionService.GetTransactions(Convert.ToDateTime(toBookingDateTime).Ticks, Convert.ToDateTime(fromBookingDateTime).Ticks,"100");
        }

        [HttpPost,Route("send")]
        public string PerformTransaction(ProcessTransaction transactionToSend) {
            return _transactionService.SendTransaction(transactionToSend.To, transactionToSend.From, transactionToSend.Amount);
        }

        [HttpGet,Route("balance")]
        public int GetBalance(string accNo) {
            return _transactionService.GetBalance(accNo);
        }
    }

    public class ProcessTransaction {
        [Required]
        public string To { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
