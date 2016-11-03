import * as angular from 'angular';

describe('DatacardState', () => {
    let $injector: angular.auto.IInjectorService;

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
    }));

    it('should include datacard component', () => {
        expect($injector.get<angular.IDirective[]>('datacardDirective').length).toBe(1);
    })
})