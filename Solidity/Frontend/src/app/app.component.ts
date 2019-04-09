import { Component} from '@angular/core';

//import { Web3Service } from './service-proxies/web3.service'
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogComponent } from './components/dialog/dialog.component';

export interface DialogData {
  title: string;
  lable: string;
  account: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  //providers: [Web3Service]
})
export class AppComponent {
  title: string;
  label: string;
  account: string;

  constructor(public dialog: MatDialog) {}

  // openDialog(): void {
  //   const dialogRef = this.dialog.open(DialogComponent, {
  //     width: '250px',
  //     data: {title: this.title, label: this.label, account: this.account}
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.account = result;
  //   });
  // }
}
