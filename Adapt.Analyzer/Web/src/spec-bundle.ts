import { maps } from './fakes/google-maps.fake';
window['google'] ={
    maps: maps
};

import './vendor';
import './main';
import * as angular from 'angular';

import 'angular-mocks';


let context = (<any>require).context('.', true, /\.spec\.ts$/);
describe('Adapt Analyzer', () => {
    beforeEach(angular.mock.module('adapt.analyzer'))

    beforeEach(angular.mock.inject((_$window_, _$httpBackend_: angular.IHttpBackendService) => {
        _$window_.google = window['google'];
        _$httpBackend_.whenGET('/config.json')
            .respond({ api_url: 'http://localhost:5000' });
    }));

    context.keys().forEach(context);
});