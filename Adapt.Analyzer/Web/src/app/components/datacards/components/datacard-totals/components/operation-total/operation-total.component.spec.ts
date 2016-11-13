import * as angular from 'angular';

import { OperationTotalComponent } from './operation-total.component';

describe('OperationTotalComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: angular.IDirective;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = _$injector_.get('operationTotalDirective')[0];
    }))

    it('should specify operation total component', () => {
        expect(directive.template).toBe(require('./templates/operation-total.template'));
        expect(directive.controller).toBe(OperationTotalComponent);
    });

    it('should include representation total component', () => {
        let representationTotalDirective = $injector.get<angular.IDirective[]>('representationTotalDirective');
        expect(representationTotalDirective.length).toBe(1);
    });
})