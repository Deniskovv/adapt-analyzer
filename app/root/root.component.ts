import { module } from 'angular';

import './styles/root';
export class RootComponent {
    static $inject = ['$mdSidenav']

    constructor(private $mdSidenav: angular.material.ISidenavService) {
    }

    toggleSidenav() {
        this.$mdSidenav('left').open();
    }
}
module('adapt.analyzer.docs')
    .component('root', {
        controller: RootComponent,
        template: require('./templates/root.template')
    })