import * as angular from 'angular';

import '../representation-total/representation-total.component';
export class OperationTotalComponent {

}
angular.module('adapt.analyzer')
    .component('operationTotal', {
        controller: OperationTotalComponent,
        template: require('./templates/operation-total.template'),
        bindings: {
            total: '<'
        }
    });