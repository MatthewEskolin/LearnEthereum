using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EthereumWeb.Models;

namespace EthereumWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {


            var web3 = new Nethereum.Web3.Web3("http://127.0.0.1:8545");


           // web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            var result = web3.Eth.Accounts.SendRequestAsync();


            var acc = web3.Eth.Accounts;

        

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
