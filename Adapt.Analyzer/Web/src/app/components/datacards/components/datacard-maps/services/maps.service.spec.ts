import * as angular from 'angular';

import { MapsService } from './maps.service';
import { 
    Point,
    Ring
} from '../models';

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

            expect(map.showInfoWindow).toHaveBeenCalledWith('34');
            done();
        });
        $rootScope.$digest();
    });

    it('should convert point to latitude longitude array', () => {
        let point: Point = { latitude: 7.34, longitude: 85.23 };

        let latLng = mapsService.convertPoint(point);
        expect(latLng).toEqual([7.34, 85.23]);
    });

    it('should convert undefined/null point to empty array', () => {
        let point: Point = undefined;

        let latLng = mapsService.convertPoint(point);
        expect(latLng).toEqual([]);
    })

    it('should convert ring to an array of numbers', () => {
        let ring: Ring = {
            points: [
                { latitude: 34.4, longitude: 45.34 },
                { latitude: 74.4, longitude: 5.34 },
                { latitude: 9.4, longitude: 3.34 }
            ]
        };

        let array = mapsService.convertRing(ring);
        expect(array).toEqual([
            [34.4, 45.34],
            [74.4, 5.34],
            [9.4, 3.34]
        ]);
    });

    it('should convert undefined/null ring to empty array', () => {
        let ring: Ring = undefined;

        let array = mapsService.convertRing(ring);
        expect(array).toEqual([]);
    })
});