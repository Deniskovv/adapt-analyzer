import * as angular from 'angular';

import { FieldTotalComponent } from './field-total.component';

describe('FieldTotalComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: angular.IDirective;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = _$injector_.get('fieldTotalDirective')[0];
    }))

    it('should specify field total component', () => {
        expect(directive.template).toBe(require('./templates/field-total.template'));
        expect(directive.controller).toBe(FieldTotalComponent);
    });

    it('should include operation total component', () => {
        let operationTotalDirective = $injector.get<angular.IDirective[]>('operationTotalDirective');
        expect(operationTotalDirective.length).toBe(1);
    });
})