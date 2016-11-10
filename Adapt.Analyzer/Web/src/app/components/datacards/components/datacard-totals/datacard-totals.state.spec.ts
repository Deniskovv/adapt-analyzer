import * as angular from 'angular';

import { datacardTotalsState } from './datacard-totals.state';

describe('DatacardTotalsState', () => {
    let $injector: angular.auto.IInjectorService;
    let $state: angular.ui.IStateService;

    beforeEach(angular.mock.inject((_$injector_, _$state_) => {
        $state = _$state_;
        $injector = _$injector_;
    }));

    it('should include datacard totals component', () => {
        let directives = $injector.get<angular.IDirective[]>('datacardTotalsDirective');
        expect(directives.length).toBe(1);
    });

    it('should register state', () => {
        let state = $state.get(datacardTotalsState);
        expect(state.url).toBe('/totals');
        expect(state.name).toBe('datacard.totals');
        expect(state.template).toContain('layout-fill');
        expect(state.template).toContain('flex');
        expect(state.template).toContain('datacard-totals');
    })
})