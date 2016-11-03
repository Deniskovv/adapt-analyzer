import './vendor';
import './main';
import * as angular from 'angular';

import 'angular-mocks';

let context = (<any>require).context('.', true, /\.spec\.ts$/);
describe('Adapt Analyzer', () => {
    beforeEach(angular.mock.module('adapt.analyzer'))

    beforeEach(angular.mock.inject((_$httpBackend_: angular.IHttpBackendService) => {
        _$httpBackend_.whenGET('/config.json')
            .respond({ api_url: 'http://localhost:5000' });
    }));

    context.keys().forEach(context);
});