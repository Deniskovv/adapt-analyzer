import { module } from 'angular';

import './styles/root';
export class RootComponent {
    static $inject = ['$state', '$mdSidenav']

    constructor(private $state: angular.ui.IStateService,
        private $mdSidenav: angular.material.ISidenavService) {
    }

    navigate(name: string): void {
        this.$state.go(name);
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