import * as angular from 'angular';

import { datacardState } from '../datacards/datacard.state';
import { ConfigService } from '../../services/config.service';

export class UploadComponent {
    static $inject = ['$http', '$state', 'ConfigService']

    constructor(private $http: angular.IHttpService,
        private $state: angular.ui.IStateService,
        private configService: ConfigService) {

    }

    handleFileChanged(files) {
        this.configService.getConfig()
            .then(c => this.$http.post<string>(`${c.api_url}/datacards/upload`, files[0]))
            .then(res => this.$state.go(datacardState, { id: res.data }));
    }
}
angular.module('adapt.analyzer')
    .component('upload', {
        controller: UploadComponent,
        template: require('./templates/upload.template')
    });