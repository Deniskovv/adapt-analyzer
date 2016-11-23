import * as angular from 'angular';

import { DatacardMapsComponent } from './datacard-maps.component';
import { FieldBoundary } from './models';

describe('DatacardMapsComponent', () => {
    let $scope: angular.IScope;
    let $state: angular.ui.IStateService;
    let $httpBackend: angular.IHttpBackendService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$httpBackend_, _$rootScope_, _$compile_, _$state_) => {
        $state = _$state_;
        $scope = _$rootScope_.$new();
        $httpBackend = _$httpBackend_;

        createComponent = () => {
            let component = _$compile_('<datacard-maps></datacard-maps>')($scope);
            $scope.$digest();
            return component;
        };
    }));

    it('should get field boundaries', () => {
        $state.params['id'] = 'some-datacard';

        let fieldBoundaries: FieldBoundary[] = [
            { centerPoint: { longitude: 1, latitude: 3 } }, 
            { centerPoint: { longitude: 4, latitude: 7 } }, 
            { centerPoint: { longitude: 5, latitude: 10 } }
        ];
        $httpBackend.expectGET('http://localhost:5000/datacards/some-datacard/fields/boundaries')
            .respond(fieldBoundaries);

        let component = createComponent();
        $httpBackend.flush();

        let markers = component.find('custom-marker');
        expect(markers.length).toBe(3);
        expect(angular.element(markers[0]).attr('position')).toBe('[1,3]')
        expect(angular.element(markers[1]).attr('position')).toBe('[4,7]')
        expect(angular.element(markers[2]).attr('position')).toBe('[5,10]')
    });
});