import * as angular from 'angular';

import './styles/plugin-total';
import '../field-total/field-total.component';
export class PluginTotalComponent {

}
angular.module('adapt.analyzer')
    .component('pluginTotal', {
        controller: PluginTotalComponent,
        template: require('./templates/plugin-total.template'),
        bindings: {
            total: '<'
        }
    });
