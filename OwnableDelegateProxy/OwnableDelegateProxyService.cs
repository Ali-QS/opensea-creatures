using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using OpenseaCreatures.Contracts.OwnableDelegateProxy.ContractDefinition;

namespace OpenseaCreatures.Contracts.OwnableDelegateProxy
{
    public partial class OwnableDelegateProxyService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, OwnableDelegateProxyDeployment ownableDelegateProxyDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<OwnableDelegateProxyDeployment>().SendRequestAndWaitForReceiptAsync(ownableDelegateProxyDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, OwnableDelegateProxyDeployment ownableDelegateProxyDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<OwnableDelegateProxyDeployment>().SendRequestAsync(ownableDelegateProxyDeployment);
        }

        public static async Task<OwnableDelegateProxyService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, OwnableDelegateProxyDeployment ownableDelegateProxyDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, ownableDelegateProxyDeployment, cancellationTokenSource);
            return new OwnableDelegateProxyService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public OwnableDelegateProxyService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
