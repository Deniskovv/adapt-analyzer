import * as angular from 'angular';

import { datacardMetadataState } from './datacard-metadata.state';

describe('DatacardMetadataState', () => {
    let $injector: angular.auto.IInjectorService;
    let $state: angular.ui.IStateService;

    beforeEach(angular.mock.inject((_$injector_, _$state_) => {
        $state = _$state_;
        $injector = _$injector_;
    }));

    it('should include datacard metadata component', () => {
        let directives = $injector.get<angular.IDirective[]>('datacardMetadataDirective');
        expect(directives.length).toBe(1);
    });

    it('should register state', () => {
        let state = $state.get(datacardMetadataState);
        expect(state.url).toBe('/metadata');
        expect(state.name).toBe('datacard.metadata');
        expect(state.template).toContain('layout-fill');
        expect(state.template).toContain('flex');
        expect(state.template).toContain('datacard-metadata');
    })
})