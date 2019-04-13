# Installation notes 

## I. To deploy contracts in local blockchain
1. Navigate to ***MarketChain*** folder and open command prompt 
2. Execute: **npm install**
3. Fire **Ganache**
4. ***truffle compile***
etc...

## II. Run frontend application(Angular)
1. Navigate to ***Frontend*** folder and open command prompt 
2. Execute: **npm install @angular/cli**
3. Execute: **npm install**
4. Execute: **ng build** - at this point compilation errors might occur. This is due to a incompatibility between two node packages. To fix this you need to go to:
*node_modules/@angular-devkit/build-angular/src/angular-cli-files/models/webpack-configs/browser.js*    
and manually change a line in the file
// old:
***node: false,***
// new:
***node: { crypto: true, stream: true },***
Reference: https://github.com/tensorflow/tfjs/issues/494
5. Execute: **ng serve** - the application should run on http://localhost:4200/

