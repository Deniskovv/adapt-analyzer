import * as angular from 'angular';

export class RepresentationTotalComponent {

}
angular.module('adapt.analyzer')
    .component('representationTotal', {
        controller: RepresentationTotalComponent,
        template: require('./templates/representation-total.template'),
        bindings: {
            total: '<'
        }
    });