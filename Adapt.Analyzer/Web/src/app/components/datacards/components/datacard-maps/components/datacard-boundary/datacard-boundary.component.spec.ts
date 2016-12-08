import * as angular from 'angular';

import { Boundary } from '../../models';

describe('DatacardBoundaryComponent', () => {
    let boundary: Boundary;
    let $scope: angular.IScope;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$compile_, _$rootScope_) => {
        boundary = {};
        $scope = _$rootScope_.$new();

        createComponent = () => {
            let template = `
                <ng-map>
                    <datacard-boundary boundary="boundary"></datacard-boundary>
                </ng-map>`
            $scope['boundary'] = boundary;
            let component = _$compile_(template)($scope);
            $scope.$digest();
            return component;
        }
    }));

    it('should have a polygon for each exterior', () => {
        boundary.exteriors = [
            { 
                points: [{latitude: 19.0, longitude: 20.0}]
            }, 
            {
                points: [{latitude: 19.0, longitude: 20.0}]
            }, 
            {
                points: [{latitude: 19.0, longitude: 20.0}]
            }
        ];
        let component = createComponent();

        expect(component.find('shape').length).toBe(3);
    });

    it('should have points of the exterior', () => {
        boundary.exteriors = [
            {
                points: [
                    { latitude: 54.12, longitude: 65.23}, 
                    { latitude: 51.12, longitude: 65.7}, 
                    { latitude: 53.0, longitude: 65.23} 
                ]
            }
        ];

        let component = createComponent();
        let shape = angular.element(component.find('shape')[0])
        expect(shape.attr('paths')).toBe('[[54.12,65.23],[51.12,65.7],[53,65.23]]');
    });

    it('should have a polygon for each interior', () => {
        boundary.interiors = [
            {
                points: [{ latitude: 6.3, longitude: 6.1 }],
            },
            {
                points: [{ latitude: 6.3, longitude: 6.1 }]
            },
            {
                points: [{ latitude: 6.3, longitude: 6.1 }]
            }
        ];

        let component = createComponent();
        expect(component.find('shape').length).toBe(3);
    });

    it('should have points of the interior', () => {
        boundary.interiors = [
            {
                points: [
                    { latitude: 54.12, longitude: 65.23}, 
                    { latitude: 51.12, longitude: 65.7}, 
                    { latitude: 53.0, longitude: 65.23} 
                ]
            }
        ];

        let component = createComponent();
        let shape = angular.element(component.find('shape')[0])
        expect(shape.attr('paths')).toBe('[[54.12,65.23],[51.12,65.7],[53,65.23]]');
    });

    it('should have style of exteriors', () => {
        boundary.exteriors = [{ points: [] }];

        let component = createComponent();
        let shape = angular.element(component.find('shape')[0]);
        expect(shape.attr('stroke-color')).toBe('#36840f');
        expect(shape.attr('stroke-opacity')).toBe('0.8');
        expect(shape.attr('stroke-weight')).toBe('2');
        expect(shape.attr('fill-color')).toBe('#36840f');
        expect(shape.attr('fill-opacity')).toBe('0.3');
    });

    it('should have style of interiors', () => {
        boundary.interiors = [{ points: [] }];

        let component = createComponent();
        let shape = angular.element(component.find('shape')[0]);
        expect(shape.attr('stroke-color')).toBe('#ce1c1c');
        expect(shape.attr('stroke-opacity')).toBe('0.8');
        expect(shape.attr('stroke-weight')).toBe('1');
        expect(shape.attr('fill-color')).toBe('#ce1c1c');
        expect(shape.attr('fill-opacity')).toBe('0.3');
    })
})