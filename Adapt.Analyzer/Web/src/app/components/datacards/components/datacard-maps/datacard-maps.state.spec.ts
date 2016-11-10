import * as angular from 'angular';

import { datacardMapsState } from './datacard-maps.state';

describe('DatacardMapsState', () => {
    let $injector: angular.auto.IInjectorService;
    let $state: angular.ui.IStateService;

    beforeEach(angular.mock.inject((_$injector_, _$state_) => {
        $state = _$state_;
        $injector = _$injector_;
    }));

    it('should include datacard maps component', () => {
        let directives = $injector.get<angular.IDirective[]>('datacardMapsDirective');
        expect(directives.length).toBe(1);
    });

    it('should register state', () => {
        let state = $state.get(datacardMapsState);
        expect(state.url).toBe('/maps');
        expect(state.name).toBe('datacard.maps');
        expect(state.template).toContain('layout-fill');
        expect(state.template).toContain('flex');
        expect(state.template).toContain('datacard-maps');
    })
})