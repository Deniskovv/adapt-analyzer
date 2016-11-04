import * as angular from 'angular';

describe('DatacardState', () => {
    let $injector: angular.auto.IInjectorService;
    let $state: angular.ui.IStateService;

    beforeEach(angular.mock.inject((_$injector_, _$state_) => {
        $state = _$state_;
        $injector = _$injector_;
    }));

    it('should include datacard component', () => {
        expect($injector.get<angular.IDirective[]>('datacardDirective').length).toBe(1);
    });

    it('should register state', () => {
        let state = $state.get('datacard');
        expect(state.url).toBe('/datacards/:id');
    })
})