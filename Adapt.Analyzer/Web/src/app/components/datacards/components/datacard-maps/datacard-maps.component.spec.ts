import * as angular from 'angular';

import { DatacardMapsComponent } from './datacard-maps.component';
import { FieldBoundary } from './models';

describe('DatacardMapsComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let $scope: angular.IScope;
    let $state: angular.ui.IStateService;
    let $httpBackend: angular.IHttpBackendService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$injector_, _$httpBackend_, _$rootScope_, _$compile_, _$state_) => {
        $injector = _$injector_;
        $state = _$state_;
        $state.params['id'] = 'some-datacard';

        $scope = _$rootScope_.$new();
        $httpBackend = _$httpBackend_;

        createComponent = () => {
            let component = _$compile_('<datacard-maps></datacard-maps>')($scope);
            $scope.$digest();
            return component;
        };
    }));

    it('should set center of map to first point', () => {
        let fieldBoundaries = setupFieldBoundaries();

        let component = createComponent();
        $httpBackend.flush();

        let ngMap = component.find('ng-map');
        expect(ngMap.attr('center')).toBe('[3,1]');
    });

    it('should have default center', () => {
        let component = createComponent();
        let ngMap = component.find('ng-map');
        expect(ngMap.attr('center')).toBe('[-93.563235,41.363729]');
    });

    it('should show satellite map', () => {
        let component = createComponent();
        let ngMap = component.find('ng-map');
        expect(ngMap.attr('map-type-id')).toBe('satellite');
    });

    it('should show boundaries when marker clicked', () => {
        setupFieldBoundaries();
        let component = createComponent();
        $httpBackend.flush();

        expect(component.find('datacard-field-boundary').length).toBe(3);
    });

    it('should include datacard field boundary component', () => {
        let directive = $injector.get<angular.IDirective[]>('datacardFieldBoundaryDirective');
        expect(directive.length).toBe(1);
    })

    function setupFieldBoundaries() : FieldBoundary[] {
        let fieldBoundaries: FieldBoundary[] = [
            { centerPoint: { longitude: 1, latitude: 3 } }, 
            { centerPoint: { longitude: 4, latitude: 7 } }, 
            { centerPoint: { longitude: 5, latitude: 10 } }
        ];
        $httpBackend.expectGET('http://localhost:5000/datacards/some-datacard/fields/boundaries')
            .respond(fieldBoundaries);

        return fieldBoundaries;
    }
});