import * as angular from 'angular';

import { MODULE_NAME } from '../module.constants';

export class WelcomeComponent {
    
}
angular.module(MODULE_NAME)
    .component('welcome', {
        controller: WelcomeComponent,
        template: require('./templates/welcome.template')
    })