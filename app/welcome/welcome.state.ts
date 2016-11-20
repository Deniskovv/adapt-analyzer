import * as angular from 'angular';

import { MODULE_NAME } from '../module.constants';

import './welcome.component';
export const WelcomeState: angular.ui.IState = {
    name: 'welcome',
    url: '/welcome',
    template: '<welcome></welcome>'
}

angular.module(MODULE_NAME)
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        ($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider) => {
            $stateProvider.state(WelcomeState);
            $urlRouterProvider.otherwise(<string>WelcomeState.url);
        }
    ])