import * as angular from 'angular';

import { MODULE_NAME } from '../module.constants';
import { DownloadService } from './services/download.service';
import { Download } from './models/download';

import './services/download.service';
export class DownloadsComponent {
    static $inject = ['DownloadService'];

    latest: Download;

    previousVersions: Download[];

    constructor(private downloadService: DownloadService) {

    }

    $onInit() {
        this.downloadService.getLatest()
            .then(download => this.latest = download);

        this.downloadService.getPreviousVersions()
            .then(previousVersions => this.previousVersions = previousVersions);
    }
}
angular.module(MODULE_NAME)
    .component('downloads', {
        controller: DownloadsComponent,
        template: require('./templates/downloads.template')
    })