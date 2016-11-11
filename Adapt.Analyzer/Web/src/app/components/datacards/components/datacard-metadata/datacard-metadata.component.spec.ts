import * as angular from 'angular';

import { DatacardMetadataComponent } from './datacard-metadata.component';

describe('DatacardMetadataComponent', () => {
    let $scope: angular.IScope;
    let $state: angular.ui.IStateService;
    let $httpBackend: angular.IHttpBackendService;
    let createComponent: () => angular.IAugmentedJQuery;

    beforeEach(angular.mock.inject((_$httpBackend_, _$rootScope_, _$compile_, _$state_) => {
        $state = _$state_;
        $scope = _$rootScope_.$new();
        $httpBackend = _$httpBackend_;

        createComponent = () => {
            let component = _$compile_('<datacard-metadata></datacard-metadata>')($scope);
            $scope.$digest();
            return component;
        }
    }));

    it('should get metadata from api', () => {
        $state.params['id'] = 'datacardId';

        let metadata = {dataModels: []};
        $httpBackend.expectGET('http://localhost:5000/datacards/datacardId/metadata')
            .respond(metadata);

        let component = createComponent();
        let jsonFormatter = component.find('json-formatter');
        
        $httpBackend.flush();

        expect(jsonFormatter.length).toBe(1);
        expect(jsonFormatter.attr('json')).toBe('metadata');
        expect(component.isolateScope()['$ctrl'].metadata).toEqual(metadata);
    });

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    })
});