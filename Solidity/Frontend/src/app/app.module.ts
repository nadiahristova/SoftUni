import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';


import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import {MatNativeDateModule, MatDatepickerModule} from '@angular/material';

import { DialogComponent } from './components/dialog/dialog.component';

import { MatSnackBarModule, MatDialogModule  } from '@angular/material';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import {
  AgmCoreModule
} from '@agm/core';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    MatSnackBarModule,
    MatDatepickerModule,
    MatDialogModule,
    MatNativeDateModule,
    //UtilModule,
    AgmCoreModule.forRoot({
      apiKey: 'YOUR_GOOGLE_MAPS_API_KEY'
    })
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
  ],
  providers: [  ],
  entryComponents: [ DialogComponent ],
  bootstrap: [AppComponent]
})
export class AppModule { }
