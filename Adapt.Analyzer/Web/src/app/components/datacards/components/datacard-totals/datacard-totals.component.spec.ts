import * as angular from 'angular';

import { DatacardTotalsComponent } from './datacard-totals.component';
import { Totals } from '../../../../shared';

describe('DatacardTotalsComponent', () => {
    let $scope: angular.IScope;
    let $state: angular.ui.IStateService;
    let $httpBackend: angular.IHttpBackendService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$httpBackend_, _$compile_, _$rootScope_, _$state_) => {
        $state = _$state_;
        $scope = _$rootScope_.$new();
        $httpBackend = _$httpBackend_;

        createComponent = () => {
            let component = _$compile_('<datacard-totals></datacard-totals>')($scope);
            $scope.$digest();
            return component;
        }
    }));

    it('should get totals for datacard', () => {
        $state.params['id'] = 'this_is_totals';

        let totals = createTotals();
        $httpBackend.expectGET('http://localhost:5000/datacards/this_is_totals/totals')
            .respond(totals);

        let component = createComponent();
        $httpBackend.flush();

        expect(component.find('operation').length).toBe(1);
        expect(component.find('total').length).toBe(2);
    })  

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    })  

    function createTotals(): Totals {
        return {
            operationTotals: [
                {
                    operationType: 'Harvest',
                    representationTotals: [
                        { representation: 'Yield', unitOfMeasure: 'bu', total: 34.12 },
                        { representation: 'Yield', unitOfMeasure: 'bu', total: 34.12 }
                    ] 
                }
            ]
        };
    }
});