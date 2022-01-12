using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace OpenseaCreatures.Contracts.ProxyRegistry.ContractDefinition
{


    public partial class ProxyRegistryDeployment : ProxyRegistryDeploymentBase
    {
        public ProxyRegistryDeployment() : base(BYTECODE) { }
        public ProxyRegistryDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProxyRegistryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5060d38061001f6000396000f3fe6080604052348015600f57600080fd5b506004361060285760003560e01c8063c455279114602d575b600080fd5b60536038366004606f565b6000602081905290815260409020546001600160a01b031681565b6040516001600160a01b03909116815260200160405180910390f35b600060208284031215608057600080fd5b81356001600160a01b0381168114609657600080fd5b939250505056fea2646970667358221220df05343db6278ef96e25876b44cc35d42395c0297deef7b8a71365b26199307b64736f6c634300080b0033";
        public ProxyRegistryDeploymentBase() : base(BYTECODE) { }
        public ProxyRegistryDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ProxiesFunction : ProxiesFunctionBase { }

    [Function("proxies", "address")]
    public class ProxiesFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ProxiesOutputDTO : ProxiesOutputDTOBase { }

    [FunctionOutput]
    public class ProxiesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
