import * as angular from 'angular';

import { RepresentationTotalComponent } from './representation-total.component';

describe('RepresentationTotalComponent', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: angular.IDirective;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = _$injector_.get('representationTotalDirective')[0];
    }))

    it('should specify representation total component', () => {
        expect(directive.template).toBe(require('./templates/representation-total.template'));
        expect(directive.controller).toBe(RepresentationTotalComponent);
    });
})