import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from '@angular/material';

import { AppComponent } from './app.component';
import { UploadComponent } from './upload/upload.component';

@NgModule({
    imports: [
        BrowserModule,
        MaterialModule.forRoot()
    ],
    declarations: [
        AppComponent,
        UploadComponent
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {}