import * as angular from 'angular';

import { PluginTotalComponent } from './plugin-total.component';

describe('PluginTotalComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: angular.IDirective;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = _$injector_.get('pluginTotalDirective')[0];
    }))

    it('should specify plugin total component', () => {
        expect(directive.template).toBe(require('./templates/plugin-total.template'));
        expect(directive.controller).toBe(PluginTotalComponent);
    });

    it('should include field total component', () => {
        let fieldTotalDirective = $injector.get<angular.IDirective[]>('fieldTotalDirective');
        expect(fieldTotalDirective.length).toBe(1);
    });
})