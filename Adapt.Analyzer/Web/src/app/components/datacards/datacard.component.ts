import * as angular from 'angular';

import { Plugin } from './models/plugin';
import { ConfigService } from '../../services/config.service';

export class DatacardComponent {
    static $inject = ['$http', '$state', 'ConfigService'];

    plugins: Plugin[];

    constructor(private $http: angular.IHttpService, 
        private $state: angular.ui.IStateService,
        private configService: ConfigService) {

    }

    $onInit() {
        let id = this.$state.params['id'];
        this.configService.getConfig()
            .then(c => this.$http.get<Plugin[]>(`${c.api_url}/datacards/${id}/plugins`))
            .then(res => this.plugins = res.data);
    }
}
angular.module('adapt.analyzer')
    .component('datacard', {
        controller: DatacardComponent,
        template: require('./templates/datacard.template')
    });