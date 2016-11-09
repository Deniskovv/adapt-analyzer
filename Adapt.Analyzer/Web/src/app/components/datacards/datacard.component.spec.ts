import * as angular from 'angular';

import { Plugin } from '../../shared';
import { DatacardComponent } from './datacard.component';

describe('DatacardComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let $httpBackend: angular.IHttpBackendService;
    let datacardDirective: angular.IDirective;
    let $state: angular.ui.IStateService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$injector_, _$httpBackend_, _$state_, _$rootScope_, _$compile_) => {
        $state = _$state_
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

    it('should include datacard maps component', () => {
        let datacardMapsDirective = $injector.get<angular.IDirective[]>('datacardMapsDirective');
        expect(datacardMapsDirective.length).toBe(1);
    });

    it('should include datacard metadata component', () => {
        let datacardMapsDirective = $injector.get<angular.IDirective[]>('datacardMetadataDirective');
        expect(datacardMapsDirective.length).toBe(1);
    })

    it('should include datacard totals component', () => {
        let datacardTotalsDirective = $injector.get<angular.IDirective[]>('datacardTotalsDirective');
        expect(datacardTotalsDirective.length).toBe(1);
    })
});