import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from '@angular/material';

import { AppComponent } from './app.component';
import { UploadComponent } from './upload/upload.component';
import { DatacardService } from './datacard/services/datacard.service';

@NgModule({
    imports: [
        BrowserModule,
        MaterialModule.forRoot()
    ],
    declarations: [
        AppComponent,
        UploadComponent
    ],
    providers: [
        DatacardService
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {}