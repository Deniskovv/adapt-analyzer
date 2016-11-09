import * as angular from 'angular';
import './styles/app.scss';
import './shared';
import './components/upload/upload.component';
import './components/plugins/plugins.component';

export class AppComponent {
    static $inject = ['$mdSidenav']

    constructor(private $mdSidenav: angular.material.ISidenavService) {
    }

    toggleSidenav() {
        this.$mdSidenav('left').open();
    }
}
angular.module('adapt.analyzer')
    .component('app', {
        controller: AppComponent,
        template: require('./templates/app.template')
    });
