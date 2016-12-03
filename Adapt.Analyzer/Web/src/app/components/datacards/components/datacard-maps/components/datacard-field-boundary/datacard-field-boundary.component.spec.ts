import * as angular from 'angular';

import { FieldBoundary } from '../../models';
import { DatacardFieldBoundaryComponent } from './datacard-field-boundary.component';

describe('DatacardFieldBoundaryComponent', () => {
    let $scope: angular.IScope;
    let fieldBoundary: FieldBoundary;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$compile_, _$rootScope_) => {
        fieldBoundary = {};
        $scope = _$rootScope_.$new();

        createComponent = () => {
            let template = `<ng-map>
                <datacard-field-boundary field-boundary="fieldBoundary">
                </datacard-field-boundary>
            </ng-map>`;
            $scope['fieldBoundary'] = fieldBoundary;
            let component = _$compile_(template)($scope);
            $scope.$digest();
            return component; 
        };
    }));

    it('should have marker at field boundary center point', () => {
        fieldBoundary.centerPoint = { latitude: 23.23, longitude: 45.23 };

        let component = createComponent();
        expect(component.find('marker').attr('position')).toBe('[23.23,45.23]')
    });

    it('should have name for marker', () => {
        fieldBoundary.description = 'Field #1';

        let component = createComponent();
        expect(component.find('marker').attr('title')).toBe('Field #1');
    });
});