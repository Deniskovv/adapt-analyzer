import * as angular from 'angular';

import { 
    ApiService,
    Plugin 
} from '../../shared';

export class PluginsComponent {
    static $inject = ['$state', 'ApiService'];

    plugins: Plugin[];

    constructor(private $state: angular.ui.IStateService,
        private apiService: ApiService) {

    }

    $onInit() {
        let id = this.$state.params['id'];
        this.apiService.get<Plugin[]>(`/datacards/${id}/plugins`)
            .then(res => this.plugins = res.data);
    }
}
angular.module('adapt.analyzer')
    .component('plugins', {
        controller: PluginsComponent,
        template: require('./templates/plugins.template')
    });