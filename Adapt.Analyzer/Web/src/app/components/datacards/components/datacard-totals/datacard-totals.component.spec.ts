import * as angular from 'angular';

import { DatacardTotalsComponent } from './datacard-totals.component';
import { DatacardTotal } from '../../../../shared';

describe('DatacardTotalsComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let $scope: angular.IScope;
    let $state: angular.ui.IStateService;
    let $httpBackend: angular.IHttpBackendService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$injector_, _$httpBackend_, _$compile_, _$rootScope_, _$state_) => {
        $injector = _$injector_;
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

        expect(component.find('datacard-total').length).toBe(1);
    });

    it('should include datacard total component', () => {
        let directive = $injector.get<angular.IDirective[]>('datacardTotalDirective');
        expect(directive.length).toBe(1);
    });

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    })  

    function createTotals(): DatacardTotal {
        return {
            pluginTotals: []
        };
    }
});