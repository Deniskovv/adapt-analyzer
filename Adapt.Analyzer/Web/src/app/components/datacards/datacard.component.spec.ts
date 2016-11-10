import * as angular from 'angular';

import { Plugin } from '../../shared';
import { DatacardComponent } from './datacard.component';
import { datacardMapsState } from './components/datacard-maps/datacard-maps.state';
import { datacardMetadataState } from './components/datacard-metadata/datacard-metadata.state';
import { datacardTotalsState } from './components/datacard-totals/datacard-totals.state';

describe('DatacardComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let $httpBackend: angular.IHttpBackendService;
    let datacardDirective: angular.IDirective;
    let $state: angular.ui.IStateService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$injector_, _$httpBackend_, _$state_, _$rootScope_, _$compile_) => {
        $state = _$state_
        spyOn($state, 'go').and.callFake(() => {});

        $injector = _$injector_;
        $httpBackend = _$httpBackend_;
        datacardDirective = _$injector_.get('datacardDirective')[0];
        
        createComponent = () => {
            let $scope = _$rootScope_.$new();
            let component = _$compile_('<datacard></datacard>')($scope);
            $scope.$digest();
            return component;
        };
    }));

    it('should define datacard component', () => {
        expect(datacardDirective.controller).toBe(DatacardComponent);
        expect(datacardDirective.template).toBe(require('./templates/datacard.template'));
    });

    it('should have tab for maps', () => {
        let component = createComponent();
        let tabElement = angular.element(component.find('md-tab')[0]);
        expect(tabElement.attr('label')).toBe('Maps');
    });

    it('should have tab for metadata', () => {
        let component = createComponent();
        let tabElement = angular.element(component.find('md-tab')[1]);
        expect(tabElement.attr('label')).toBe('Metadata');
    });

    it('should have tab for totals', () => {
        let component = createComponent();
        let tabElement = angular.element(component.find('md-tab')[2]);
        expect(tabElement.attr('label')).toBe('Totals');
    });

    it('should go to maps', () => {
        let component = createComponent();
        angular.element(component.find('md-tab-item')[1]).triggerHandler('click');
        angular.element(component.find('md-tab-item')[0]).triggerHandler('click');

        expect($state.go).toHaveBeenCalledWith(datacardMapsState);
    });

    it('should go to metadata', () => {
        let component = createComponent();
        angular.element(component.find('md-tab-item')[1]).triggerHandler('click');

        expect($state.go).toHaveBeenCalledWith(datacardMetadataState);
    });

    it('should go to totals', () => {
        let component = createComponent();
        angular.element(component.find('md-tab-item')[2]).triggerHandler('click');

        expect($state.go).toHaveBeenCalledWith(datacardTotalsState);
    });

    it('should select totals tab', () => {
        spyOn($state, 'includes').and.callFake(name => name === datacardTotalsState.name);

        let component = createComponent();
        expect($state.includes).toHaveBeenCalledWith(datacardTotalsState.name);
        expect(component.isolateScope()['$ctrl'].selectedTab).toBe(2);
    });

    it('should select metadata tab', () => {
        spyOn($state, 'includes').and.callFake(name => name === datacardMetadataState.name);

        let component = createComponent();
        expect($state.includes).toHaveBeenCalledWith(datacardMetadataState.name);
        expect(component.isolateScope()['$ctrl'].selectedTab).toBe(1);
    })
});