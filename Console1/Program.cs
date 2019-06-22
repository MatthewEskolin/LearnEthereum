using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Nethereum.ABI;
using Nethereum.ABI.Encoders;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Geth;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;


namespace Console1
{
    public class Program
    {

        public const string RPC_URL = "http://127.0.0.1:7545";

        static void Main(string[] args)
        {


            RunAsyncCode().Wait();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
            
        }

        public static async Task RunAsyncCode()
        {

            var web3 = new Nethereum.Web3.Web3(RPC_URL);
            var web3geth = new Web3Geth(RPC_URL);




            // web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            var myres = await web3.Eth.Accounts.SendRequestAsync();

            var listofCandidates = new string[] { "Matt", "James", "Mark", "Scott" };
            var hexListofCandidates = listofCandidates.Select(x => x.ToHex()).ToArray();

            var bytes32X = hexListofCandidates.Select(x => new Bytes32Type("test").Encode(x)).ToArray();




            //convert asci to hex
            //Bytes32TypeEncoder


            var bytecode = System.IO.File.ReadAllText(@"C:\src\EthereumResearch\EthereumWeb\SolidityCode\outpu1\Voting.bin");
            var abi = System.IO.File.ReadAllText(@"C:\src\EthereumResearch\EthereumWeb\SolidityCode\outpu1\Voting.abi");


            HexBigInteger gas = new HexBigInteger(3000000);
            HexBigInteger value = new HexBigInteger(0);



            var cands = new[] {"Matt".ToHex(), "James".ToHex(), "Mark".ToHex(), "Scott".ToHex()};



            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, bytecode, myres[0], gas, value, (object)cands);


            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            var contract = web3.Eth.GetContract(abi, receipt.ContractAddress);
            var voteFunction = contract.GetFunction("totalVotesFor");
            var voteForCandidateFunction = contract.GetFunction("voteForCandidate");



            //Call to get current vote count...
            var result = await voteFunction.CallAsync<int>("James".ToHex());//<int>()



            //Send Some Vote's to James

            //VOTE FOR SOME CANDIDATES
            transactionHash =await voteForCandidateFunction.SendTransactionAsync(myres[0], new HexBigInteger(900000), null,"James".ToHex());
            transactionHash = await voteForCandidateFunction.SendTransactionAsync(myres[0], new HexBigInteger(900000), null, "James".ToHex());

            // receipt = await MineAndGetReceiptAsync(web3, transactionHash);

            //var result = await documentsFunction.CallDeserializingToObjectAsync<Document>("key1", 0);
            //var result2 = await documentsFunction.CallDeserializingToObjectAsync<Document>("key1", 1);

            var resultAgain = await voteFunction.CallAsync<int>("James".ToHex());//<int>()



            //   var result0 = await voteForCandidateFunction.CallAsync<int>("James".ToHex());//<int>()
            //var result1 = await voteForCandidateFunction.CallAsync<int>("James".ToHex());//<int>()
            //var result2 = await voteForCandidateFunction.CallAsync<int>("James".ToHex());//<int>()

            //Call to get current vote count...
            var result3 = await voteFunction.CallAsync<int>("James".ToHex());//<int>()

            //var mineResult = await web3geth.Miner.Start.SendRequestAsync(6);

            //Assert.True(mineResult);


            //mineResult = await web3.Miner.Stop.SendRequestAsync();
            //Assert.True(mineResult);

            //var contractAddress = receipt.ContractAddress;

            //var contract = web3.Eth.GetContract(abi, contractAddress);

            //var multiplyFunction = contract.GetFunction("multiply");

            //var result = await multiplyFunction.CallAsync<int>(7);

            //Assert.Equal(49, result);


            int test = 34;

            test++;

        }




    }


    [FunctionOutput]
    public class TotalVotesFor
    {
        [Parameter("uint256",1)]
        public int TotalVotesForP { get; set; }

    }


    public static class StringExtensions
    {
        public static string ToHex(this string input)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0x");

            foreach (char c in input)
                sb.AppendFormat("{0:X2}", (int)c);
            return sb.ToString().Trim();
        }
    }


}
