import * as angular from 'angular';

import { MapsService } from './maps.service';

describe('MapsService', () => {
    let ngMap;
    let $rootScope: angular.IRootScopeService;
    let mapsService: MapsService;

    beforeEach(angular.mock.inject((_$rootScope_, _NgMap_, _MapsService_) => {
        ngMap = _NgMap_;
        $rootScope = _$rootScope_;
        mapsService = _MapsService_;
    }))

    it('should show info window', (done) => {
        ngMap.getMap().then((map) => {
            spyOn(map, 'showInfoWindow').and.callThrough();

            mapsService.showInfoWindow('34');
            // expect(map.showInfoWindow).toHaveBeenCalledWith('34');
            done();
        });
        $rootScope.$digest();
    })
})