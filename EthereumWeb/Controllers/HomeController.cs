using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EthereumWeb.Models;
using Nethereum.ABI;
using Nethereum.ABI.Encoders;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Geth;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using System.Text;

namespace EthereumWeb.Controllers
{
    public class HomeController : Controller
    {

        public const string RPC_URL = "http://127.0.0.1:7545";

        public IActionResult Index()
        {


            // var web3 = new Nethereum.Web3.Web3("http://127.0.0.1:8545");


            //// web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            // var result = web3.Eth.Accounts.SendRequestAsync();


            // var acc = web3.Eth.Accounts;



            return View();
        }

        public IActionResult CreateContract()
        {

            AsyncContractCreation().Wait();

            // RedirectToAction("Index");
            ViewBag.message = "Contact Created";
            return View("Index");


        }

        private async Task  AsyncContractCreation()
        {
            var web3 = new Nethereum.Web3.Web3(RPC_URL);
            var web3geth = new Web3Geth(RPC_URL);


            // web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            var myres = await web3.Eth.Accounts.SendRequestAsync();

            var listofCandidates = new string[] { "Matt", "James", "Mark", "Scott" };
            var hexListofCandidates = listofCandidates.Select(x => x.ToHex()).ToArray();

            var bytes32X = hexListofCandidates.Select(x => new Bytes32Type("test").Encode(x)).ToArray();


            var bytecode = System.IO.File.ReadAllText(@"C:\src\EthereumResearch\EthereumWeb\SolidityCode\outpu1\Voting.bin");
            var abi = System.IO.File.ReadAllText(@"C:\src\EthereumResearch\EthereumWeb\SolidityCode\outpu1\Voting.abi");


            HexBigInteger gas = new HexBigInteger(3000000);
            HexBigInteger value = new HexBigInteger(0);

            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, bytecode, myres[0], gas, value, (object)bytes32X);


            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);



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



    public static class StringExtensions
    {
        public static string ToHex(this string input)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("0x");

            foreach (char c in input)
                sb.AppendFormat("{0:X2}", (int)c);
            return sb.ToString().Trim();
        }
    }
}
