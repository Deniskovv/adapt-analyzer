import * as angular from 'angular';

import './datacard-maps.component';
export const datacardMapsState: angular.ui.IState = {
    name: 'datacard.maps',
    url: '/maps',
    template: '<datacard-maps layout-fill flex></datacard-maps>'
};
angular.module('adapt.analyzer')
    .config(['$stateProvider', ($stateProvider: angular.ui.IStateProvider) => {
        $stateProvider.state(datacardMapsState);
    }])