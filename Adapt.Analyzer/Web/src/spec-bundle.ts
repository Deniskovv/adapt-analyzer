import { maps } from './fakes/google-maps.fake';
window['google'] ={
    maps: maps
};

import './vendor';
import './main';
import * as angular from 'angular';

import 'angular-mocks';
import { NgMapFake } from './fakes/ng-map.fake';

let context = (<any>require).context('.', true, /\.spec\.ts$/);
describe('Adapt Analyzer', () => {
    beforeEach(angular.mock.module('adapt.analyzer', ($provide: angular.auto.IProvideService) => {
        let ngMapFake = new NgMapFake();
        $provide.value('NgMap', ngMapFake);
    }))

    beforeEach(angular.mock.inject((_$window_, _$q_, _$httpBackend_: angular.IHttpBackendService, _NgMap_) => {
        _NgMap_.initialize(_$q_);
        _$window_.google = window['google'];

        _$httpBackend_.whenGET('/config.json')
            .respond({ api_url: 'http://localhost:5000' });
    }));

    context.keys().forEach(context);
});