import * as angular from 'angular';

export class DatacardTotalsComponent {

}
angular.module('adapt.analyzer')
    .component('datacardTotals', {
        template: require('./templates/datacard-totals.template')
    });