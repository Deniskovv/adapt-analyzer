import * as angular from 'angular';

import { DatacardTotalComponent } from './datacard-total.component';

describe('DatacardTotalComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: angular.IDirective;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = _$injector_.get('datacardTotalDirective')[0];
    }))

    it('should specify datacard total component', () => {
        expect(directive.template).toBe(require('./templates/datacard-total.template'));
        expect(directive.controller).toBe(DatacardTotalComponent);
    });

    it('should include plugin total component', () => {
        let pluginDirective = $injector.get<angular.IDirective[]>('pluginTotalDirective');
        expect(pluginDirective.length).toBe(1);
    });
})