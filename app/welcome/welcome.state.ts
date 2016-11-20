import * as angular from 'angular';

import { MODULE_NAME } from '../module.constants';

import './welcome.component';
export const WelcomeState: angular.ui.IState = {
    url: '/',
    template: '<welcome></welcome>'
}

angular.module(MODULE_NAME)
    .config([
        '$stateProvider',
        ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state(WelcomeState);
        }
    ])