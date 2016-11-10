import * as angular from 'angular';

import './datacard-metadata.component';
export const datacardMetadataState: angular.ui.IState = {
    name: 'datacard.metadata',
    url: '/metadata',
    template: '<datacard-metadata layout-fill flex></datacard-metadata>'
};
angular.module('adapt.analyzer')
    .config(['$stateProvider', ($stateProvider: angular.ui.IStateProvider) => {
        $stateProvider.state(datacardMetadataState);
    }])