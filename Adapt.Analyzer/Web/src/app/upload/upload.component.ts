import { Component } from '@angular/core';
import { Http } from '@angular/http';

import { DatacardService } from '../datacard/services/datacard.service';

@Component({
    selector: 'upload',
    template: require('./templates/upload.component.html'),
    styles: [
        require('./styles/upload.component.scss')
    ]
})
export class UploadComponent {

    constructor(private datacardService: DatacardService) {}

    upload(target: any) {
        let files: FileList = target.files
        this.datacardService.upload(files[0]);
    }
}