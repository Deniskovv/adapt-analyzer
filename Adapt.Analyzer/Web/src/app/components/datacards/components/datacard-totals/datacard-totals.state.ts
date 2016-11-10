import * as angular from 'angular';

import './datacard-totals.component';
export const datacardTotalsState: angular.ui.IState = {
    name: 'datacard.totals',
    url: '/totals',
    template: '<datacard-totals layout-fill flex></datacard-totals>'
};
angular.module('adapt.analyzer')
    .config(['$stateProvider', ($stateProvider: angular.ui.IStateProvider) => {
        $stateProvider.state(datacardTotalsState);
    }])