// SPDX-License-Identifier: MIT

pragma solidity ^0.8.0;

import "./ERC721Tradable.sol";

/**
 * @title Avatar
 * Avatar - a contract for my non-fungible avatars.
 */
contract Avatar is ERC721Tradable {
    constructor(address _proxyRegistryAddress)
        ERC721Tradable("Avatar", "MyC", _proxyRegistryAddress)
    {}

    function baseTokenURI() override public pure returns (string memory) {
        return "https://ipfs.io/ipfs/";
    }
}
