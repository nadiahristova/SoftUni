# Avoiding Common Attacks


1. ***Reentrancy & race conditions*** - storage is updated before transaction/ communication with outer contract(Checks-Effects-Interactions)
2. ***Integer overflows/underflows*** - using SafeMath lib for basic mathematical operations
3. ***Denial of service*** - withdraw funds function is independent from other app logic