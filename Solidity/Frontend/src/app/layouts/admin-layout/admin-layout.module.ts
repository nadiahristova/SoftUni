import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { HomeComponent } from '../../home/home.component';
import { CommunityComponent } from '../../community/community.component'
import {MatNativeDateModule, MatDatepickerModule} from '@angular/material';
import { MatSnackBarModule, MatDialogModule  } from '@angular/material';

import {
  MatButtonModule,
  MatInputModule,
  MatRippleModule,
  MatFormFieldModule,
  MatTooltipModule,
  MatSelectModule
} from '@angular/material';
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    MatButtonModule,
    MatRippleModule,
    MatDialogModule,
    MatSnackBarModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatDatepickerModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule
  ],
  declarations: [
    HomeComponent,
    UserProfileComponent,
    TableListComponent,
    CommunityComponent
  ]
})

export class AdminLayoutModule {
}
