# Used Patterns


1. ***Guard Check*** - valid address, uint >= 0, bytes32 != 0x0
2. ***Access Restriction*** - most of the functionality provided by the contracts is restricted to either members, admins or owner
3. ***Memory Array Building*** - use of public/external view functions  - fetches data from the local node
4. ***Tight Variable Packing*** - when encountering structs with bool or address property (MemberInfo, Location etc.)
5. ***Mapping Iterator*** - for keeping track of entity data(Products, Admins etc.)
6. ***Withdrawal Pattern/Checks-effects-interactions*** - user is responsible for retrieving funding or profit from market sales, the contracts do not automatically send weis
7. ***Emergency Stop*** - by adding Pausable contract. Triggered by the owner it blocks any further membership requests and donations