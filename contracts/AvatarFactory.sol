// SPDX-License-Identifier: MIT

pragma solidity ^0.8.0;

import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/utils/Strings.sol";
import "./IFactoryERC721.sol";
import "./Avatar.sol";

contract AvatarFactory is FactoryERC721, Ownable {
    using Strings for string;

    event Transfer(
        address indexed from,
        address indexed to,
        uint256 indexed tokenId
    );

    address public proxyRegistryAddress;
    address public nftAddress;
    string public baseURI = "https://ipfs.io/ipfs/factory/";

    
    uint256 AVATAR_SUPPLY = 100;

    /*
     * Three different options for minting Avatarss (basic, premium, and gold).
     */
    uint256 NUM_OPTIONS = 3;
    uint256 SINGLE_AVATAR_OPTION = 0;
    uint256 MULTIPLE_AVATAR_OPTION = 1;
    uint256 LOOTBOX_OPTION = 2;
    uint256 NUM_AVATARS_IN_MULTIPLE_AVATAR_OPTION = 4;

    constructor(address _proxyRegistryAddress, address _nftAddress) {
        proxyRegistryAddress = _proxyRegistryAddress;
        nftAddress = _nftAddress;

        fireTransferEvents(address(0), owner());
    }

    function name() override external pure returns (string memory) {
        return "Item Sale";
    }

    function symbol() override external pure returns (string memory) {
        return "MyC";
    }

    function supportsFactoryInterface() override public pure returns (bool) {
        return true;
    }

    function numOptions() override public view returns (uint256) {
        return NUM_OPTIONS;
    }

    function transferOwnership(address newOwner) override public onlyOwner {
        address _prevOwner = owner();
        super.transferOwnership(newOwner);
        fireTransferEvents(_prevOwner, newOwner);
    }

    function fireTransferEvents(address _from, address _to) private {
        for (uint256 i = 0; i < NUM_OPTIONS; i++) {
            emit Transfer(_from, _to, i);
        }
    }

    function mint(uint256 _optionId, address _toAddress) override public {
        // Must be sent from the owner proxy or owner.
        ProxyRegistry proxyRegistry = ProxyRegistry(proxyRegistryAddress);
        assert(
            address(proxyRegistry.proxies(owner()))
             == _msgSender() ||
                owner() == _msgSender()
        );
        require(canMint(_optionId));

        Avatar avatar = Avatar(nftAddress);
        if (_optionId == SINGLE_AVATAR_OPTION) {
            avatar.mintTo(_toAddress);
        } else if (_optionId == MULTIPLE_AVATAR_OPTION) {
            for (
                uint256 i = 0;
                i < NUM_AVATARS_IN_MULTIPLE_AVATAR_OPTION;
                i++
            ) {
                avatar.mintTo(_toAddress);
            }
        } 
    }

    function canMint(uint256 _optionId) override public view returns (bool) {
        if (_optionId >= NUM_OPTIONS) {
            return false;
        }

        Avatar avatar = Avatar(nftAddress);
        uint256 avatarSupply = avatar.totalSupply();

        uint256 numItemsAllocated = 0;
        if (_optionId == SINGLE_AVATAR_OPTION) {
            numItemsAllocated = 1;
        } else if (_optionId == MULTIPLE_AVATAR_OPTION) {
            numItemsAllocated = NUM_AVATARS_IN_MULTIPLE_AVATAR_OPTION;
        } 
        return avatarSupply < (AVATAR_SUPPLY - numItemsAllocated);
    }

    function tokenURI(uint256 _optionId) override external view returns (string memory) {
        return string(abi.encodePacked(baseURI, Strings.toString(_optionId)));
    }

    /**
     * Use transferFrom so the frontend doesn't have to worry about different method names.
     */
    function transferFrom(
        address,
        address _to,
        uint256 _tokenId
    ) public {
        mint(_tokenId, _to);
    }

    /**
     * Use isApprovedForAll so the frontend doesn't have to worry about different method names.
     */
    function isApprovedForAll(address _owner, address _operator)
        public
        view
        returns (bool)
    {
        if (owner() == _owner && _owner == _operator) {
            return true;
        }

        ProxyRegistry proxyRegistry = ProxyRegistry(proxyRegistryAddress);
        if (
            owner() == _owner &&
            address(proxyRegistry.proxies(_owner)) == _operator
        ) {
            return true;
        }

        return false;
    }

    /**
     * Use isApprovedForAll so the frontend doesn't have to worry about different method names.
     */
    function ownerOf(uint256) public view returns (address _owner) {
        return owner();
    }
}
