import * as angular from 'angular';
import { IState } from 'angular-ui-router';

import './datacard.component';
export const datacardState: IState = {
    name: 'datacard',
    url: '/datacards/:id',
    template: '<datacard flex layout-fill ></datacard>'
}
angular.module('adapt.analyzer')
    .config(['$stateProvider', ($stateProvider: angular.ui.IStateProvider) => {
        $stateProvider.state(datacardState);
    }])