using System;
using System.Threading.Tasks;


namespace Console1
{
    class Program
    {
        static void Main(string[] args)
        {
          
   

            RunAsyncCode();



            Console.WriteLine("Hello World!");
            Console.ReadLine();
            
        }

        public static async Task RunAsyncCode()
        {
                    
            var web3 = new Nethereum.Web3.Web3("http://127.0.0.1:8545");


           // web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            var myres = await web3.Eth.Accounts.SendRequestAsync();



            var bytecode = System.IO.File.ReadAllText(@"C:\src\EthereumResearch\EthereumWeb\SolidityCode\outpu1\Voting.bin");
            var abi = System.IO.File.ReadAllText(@"C:\src\EthereumResearch\EthereumWeb\SolidityCode\outpu1\Voting.abi");

            
           

            int test = 34;

            test++;

        }





    }

}
