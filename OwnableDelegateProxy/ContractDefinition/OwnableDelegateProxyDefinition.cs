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

namespace OpenseaCreatures.Contracts.OwnableDelegateProxy.ContractDefinition
{


    public partial class OwnableDelegateProxyDeployment : OwnableDelegateProxyDeploymentBase
    {
        public OwnableDelegateProxyDeployment() : base(BYTECODE) { }
        public OwnableDelegateProxyDeployment(string byteCode) : base(byteCode) { }
    }

    public class OwnableDelegateProxyDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea2646970667358221220a857461b7295a5676b45d1c5dff3d6197391b7e036aa93605bfd6e850dfbb62b64736f6c634300080b0033";
        public OwnableDelegateProxyDeploymentBase() : base(BYTECODE) { }
        public OwnableDelegateProxyDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
