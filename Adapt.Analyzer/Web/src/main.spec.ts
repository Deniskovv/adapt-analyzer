import * as angular from 'angular';

describe('main', () => {
    let $injector: angular.auto.IInjectorService;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
    }))

    it('should declare adapt analyzer module', () => {
        expect(angular.module('adapt.analyzer')).toBeDefined();
    });

    it('should include app component', () => {
        expect($injector.get<angular.IDirective[]>('appDirective').length).toBe(1)
    });
});