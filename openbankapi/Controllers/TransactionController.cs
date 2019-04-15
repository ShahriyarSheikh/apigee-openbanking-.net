using Microsoft.AspNetCore.Mvc;
using openbankapi.service;
using openbankapi.service.Models;
using System;
using System.Collections;
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
        TransactionService txService;
        public TransactionController()
        {
            txService = new TransactionService();
        }
        [HttpGet,Route("transactions")]
        public IEnumerable<Transaction> GetTransactions(string toBookingDateTime, string fromBookingDateTime) {
            return txService.GetTransactions(Convert.ToDateTime(toBookingDateTime).Ticks, Convert.ToDateTime(fromBookingDateTime).Ticks,"100");
        }

        [HttpPost,Route("send")]
        public string PerformTransaction(ProcessTransaction transactionToSend) {
            return txService.SendTransaction(transactionToSend.To, transactionToSend.From, transactionToSend.Amount);
        }

        [HttpGet,Route("balance")]
        public int GetBalance(string accNo) {
            return txService.GetBalance(accNo);
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
