using System.Threading.Tasks;
using Privy;
using UnityEngine;

public class ObtainCollectible : MonoBehaviour
{


    public async static Task<string> Send(IEmbeddedEthereumWallet embeddedWallet, string to, string value, string chainId, string data)
    {
        // Create transaction JSON
        string transactionJson = JsonUtility.ToJson(new
        {
            to,
            data, //Compiled code of a contract OR hash of the invoked method signature and encoded parameters.
            value, //Example: "0x186a0", // 100000 in hex
            chainId, // Example: "0x2105", // 8453 (Base) in hex
            from = embeddedWallet.Address
        });

        // Create RPC request
        var rpcRequest = new RpcRequest
        {
            Method = "eth_signTransaction",
            Params = new string[] { transactionJson }
        };

        // Sign transaction
        RpcResponse transactionResponse = await embeddedWallet.RpcProvider.Request(rpcRequest);

        // Transaction hash is in the response data
        string transactionHash = transactionResponse.Data;
        return transactionHash;
    }
}
