import * as angular from 'angular';

import { Point } from '../models';

export class MapsService {
    convertPoint(point: Point): number[] {
        return [point.latitude, point.longitude];
    }
}
angular.module('adapt.analyzer')
    .service('MapsService', MapsService);