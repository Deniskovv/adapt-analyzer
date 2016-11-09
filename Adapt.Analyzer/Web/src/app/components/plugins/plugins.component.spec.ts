import * as angular from 'angular';

import { Plugin } from '../../shared';
import { PluginsComponent } from './plugins.component';

describe('PluginsComponent', () => {
    let $httpBackend: angular.IHttpBackendService;
    let pluginsDirective: angular.IDirective;
    let $state: angular.ui.IStateService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$injector_, _$httpBackend_, _$state_, _$rootScope_, _$compile_) => {
        $state = _$state_
        $httpBackend = _$httpBackend_;
        pluginsDirective = _$injector_.get('pluginsDirective')[0];
        
        createComponent = () => {
            let $scope = _$rootScope_.$new();
            let component = _$compile_('<plugins></plugins>')($scope);
            $scope.$digest();
            return component;
        };
    }));

    it('should define datacard component', () => {
        expect(pluginsDirective.controller).toBe(PluginsComponent);
        expect(pluginsDirective.template).toBe(require('./templates/plugins.template'));
    });

    it('should display plugins', () => {
        let plugins = createPlugins();
        setupGetPlugins(plugins, 'someid');

        let component = createComponent();
        $httpBackend.flush();

        expectPlugins(plugins, angular.element(component[0].querySelectorAll('.plugin')))
    });

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    function createPlugins(): Plugin[] {
        return [
            { name: 'Plugin1', version: '4.3.1' }, 
            { name: 'Plugin3', version: '7.3.4' }
        ];
    }

    function setupGetPlugins(plugins, id) {
        $state.params['id'] = id;
        $httpBackend.expectGET(`http://localhost:5000/datacards/${id}/plugins`)
            .respond(plugins);
    }

    function expectPlugins(plugins: Plugin[], elements: angular.IAugmentedJQuery) {
        expect(elements.length).toBe(plugins.length);

        for (let i = 0; i < elements.length; i++) {
            let element = angular.element(elements[i]);
            expect(element.text()).toContain(plugins[i].name);
            expect(element.text()).toContain(plugins[i].version);
        }
    }
})