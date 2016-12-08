import * as angular from 'angular';

import { 
    Ring,
    Point
} from '../models';

export class MapsService {
    static $inject = ['NgMap'];
    private _map;

    constructor(private ngMap) {
        ngMap.getMap().then(map => this._map = map);
    }

    convertPoint(point: Point): number[] {

        return point 
            ?  [point.latitude, point.longitude] 
            : [];
    }

    convertRing(ring: Ring): Array<number[]> {
        return ring
            ? ring.points.map(p => this.convertPoint(p))
            : [];
    }

    showInfoWindow(id: string): void {
        this._map.showInfoWindow(id);
    }
}
angular.module('adapt.analyzer')
    .service('MapsService', MapsService);