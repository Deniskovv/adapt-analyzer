import * as angular from 'angular';

import { datacardState } from '../datacards/datacard.state';
import { UploadData } from './models';
import { ApiService } from '../../shared';

import './styles/upload.scss';
export class UploadComponent {
    static $inject = ['$q', '$state', 'ApiService']

    datacardName: string;

    constructor(private $q: angular.IQService,
        private $state: angular.ui.IStateService,
        private apiService: ApiService) {

    }

    handleFileChanged(files) {
        this.getUploadData(files[0])
            .then(data => this.apiService.post<string>('/datacards/upload', data))
            .then(res => this.$state.go(datacardState, { id: res.data }))
            .then(() => this.datacardName = files[0].name);
    }

    private getUploadData(file: File): angular.IPromise<UploadData> {
        return this.$q<UploadData>((resolve, reject) => {
            let reader = new FileReader();
            reader.onload = (evt) => {
                resolve({
                    name: file.name,
                    file: btoa((<any>evt.target).result)
                });
            };
            reader.readAsBinaryString(file);
        });
    }
}
angular.module('adapt.analyzer')
    .component('upload', {
        controller: UploadComponent,
        template: require('./templates/upload.template')
    });