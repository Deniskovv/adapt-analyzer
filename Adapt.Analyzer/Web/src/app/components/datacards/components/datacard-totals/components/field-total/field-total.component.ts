import * as angular from 'angular';

import '../operation-total/operation-total.component';
export class FieldTotalComponent {

}
angular.module('adapt.analyzer')
    .component('fieldTotal', {
        controller: FieldTotalComponent,
        template: require('./templates/field-total.template'),
        bindings: {
            total: '<'
        }
    });