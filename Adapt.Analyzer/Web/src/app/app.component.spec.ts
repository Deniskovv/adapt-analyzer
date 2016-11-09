import * as angular from 'angular';

import { AppComponent } from './app.component';

describe('AppComponent', () => {
    let appDirective: angular.IDirective;
    let $scope: angular.IScope;
    let createComponent: () => angular.IAugmentedJQuery; 

    beforeEach(angular.mock.inject((_$injector_, _$compile_: angular.ICompileService, _$rootScope_) => {
        $scope = _$rootScope_.$new();
        appDirective = _$injector_.get('appDirective')[0];

        createComponent = () => {
            return _$compile_('<app></app>')($scope);
        }
    }));

    it('should define app component', () => {
        expect(appDirective.controller).toBe(AppComponent);
        expect(appDirective.template).toBe(require('./templates/app.template'))
    });

    it('should show sidenav', () => {
        let component = createComponent();
        let sidenavElement = angular.element(component[0].querySelector('md-sidenav'));
        let element = angular.element(component[0].querySelector('.toggle-sidenav'));
        element.triggerHandler('click');
        expect(sidenavElement.hasClass('md-closed')).toBeFalsy();
    });

    it('should use upload component', () => {
        let component = createComponent();

        let uploadElement = angular.element(component[0].querySelectorAll('upload'));
        expect(uploadElement.length).toBe(1);
    });
});