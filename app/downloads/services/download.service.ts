import * as angular from 'angular';

import { MODULE_NAME } from '../../module.constants';
import { Download } from '../models/download';

export class DownloadService {
    static $inject = ['$http'];

    constructor(private $http: angular.IHttpService) {

    }

    getLatest(): angular.IPromise<Download> {
        return this.$http.get<Download>('');
    }

    getPreviousVersions(): angular.IPromise<Download[]> {
        return this.$http.get<Download[]>('');
    }
}

angular.module(MODULE_NAME)
    .service('DownloadService', DownloadService);