import * as angular from 'angular';

import { MODULE_NAME } from '../module.constants';

export const DownloadsState: angular.ui.IState = {
    name: 'downloads',
    url: '/downloads',
    template: '<downloads></downloads>'
}

angular.module(MODULE_NAME)
    .config([
        '$stateProvider', 
        ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state(DownloadsState);
        }
    ])