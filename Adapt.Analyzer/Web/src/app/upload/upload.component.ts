import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'upload',
    template: require('./templates/upload.component.html')
})
export class UploadComponent {

    constructor(private http: Http) {}

    upload(target: any) {
        let files: FileList = target.files
        this.http.post('/upload', files[0]);
    }
}