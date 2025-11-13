//SPDX-License-Identifier: MIT
pragma solidity >=0.8.0 <0.9.0;

// Useful for debugging. Remove when deploying to a live network.
import "forge-std/console.sol";
import "@openzeppelin/contracts/token/ERC721/ERC721.sol";

// Use openzeppelin to inherit battle-tested implementations (ERC20, ERC721, etc)
// import "@openzeppelin/contracts/access/Ownable.sol";

/**
 * A smart contract that allows changing a state variable of the contract and tracking the changes
 * It also allows the owner to withdraw the Ether in the contract
 * @author BuidlGuidl
 */
contract YourContract is ERC721 {
    uint256 public totalSupply;

    constructor() ERC721("Unity 20th Anniversary Collectible", "U20AC") {}

    function mint() public {
        _mint(msg.sender, totalSupply);
        totalSupply++;
    }

    function tokenURI(
        uint256 tokenId
    ) public view override returns (string memory) {
        return
            "https://olive-capitalist-mule-825.mypinata.cloud/ipfs/bafkreibxu4uig6pqrjhbgpakm7w2xer2ehkvv5isjhqu3ehuvoks7mgfue";
    }
}
