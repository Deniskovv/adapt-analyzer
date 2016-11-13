import * as angular from 'angular';

import { DatacardTotal } from '../../../../../../shared';

import '../plugin-total/plugin-total.component';
export class DatacardTotalComponent {
    total: DatacardTotal;
}
angular.module('adapt.analyzer')
    .component('datacardTotal', {
        controller: DatacardTotalComponent,
        template: require('./templates/datacard-total.template'),
        bindings: {
            total: '<'
        }
    })