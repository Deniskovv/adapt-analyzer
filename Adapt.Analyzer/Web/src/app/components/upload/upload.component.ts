import * as angular from 'angular';

import { datacardState } from '../datacards/datacard.state';
import { ApiService } from '../../shared';

import './styles/upload.scss';
export class UploadComponent {
    static $inject = ['$state', 'ApiService']

    datacardName: string;

    constructor(private $state: angular.ui.IStateService,
        private apiService: ApiService) {

    }

    handleFileChanged(files) {
        this.apiService.post<string>('/datacards/upload', files[0])
            .then(res => this.$state.go(datacardState, { id: res.data }))
            .then(() => this.datacardName = files[0].name);
    }
}
angular.module('adapt.analyzer')
    .component('upload', {
        controller: UploadComponent,
        template: require('./templates/upload.template')
    });