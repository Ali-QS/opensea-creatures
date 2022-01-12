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
using OpenseaCreatures.Contracts.ProxyRegistry.ContractDefinition;

namespace OpenseaCreatures.Contracts.ProxyRegistry
{
    public partial class ProxyRegistryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProxyRegistryDeployment proxyRegistryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProxyRegistryDeployment>().SendRequestAndWaitForReceiptAsync(proxyRegistryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProxyRegistryDeployment proxyRegistryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProxyRegistryDeployment>().SendRequestAsync(proxyRegistryDeployment);
        }

        public static async Task<ProxyRegistryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProxyRegistryDeployment proxyRegistryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, proxyRegistryDeployment, cancellationTokenSource);
            return new ProxyRegistryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProxyRegistryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> ProxiesQueryAsync(ProxiesFunction proxiesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProxiesFunction, string>(proxiesFunction, blockParameter);
        }

        
        public Task<string> ProxiesQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var proxiesFunction = new ProxiesFunction();
                proxiesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<ProxiesFunction, string>(proxiesFunction, blockParameter);
        }
    }
}
