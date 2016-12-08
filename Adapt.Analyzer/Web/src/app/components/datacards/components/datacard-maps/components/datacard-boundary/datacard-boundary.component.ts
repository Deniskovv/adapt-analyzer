import * as angular from 'angular';

import { MapsService } from '../../services/maps.service';
import { 
    Boundary,
    Ring
} from '../../models';

export class DatacardBoundaryComponent {
    static $inject = ['MapsService'];
    boundary: Boundary;
    get exteriors(): Ring[] {
        return this.boundary
            && this.boundary.exteriors;
    }

    get interiors(): Ring[] {
        return this.boundary
            && this.boundary.interiors;
    }

    constructor(private mapsService: MapsService) {

    }

    toPaths(ring: Ring): Array<number[]> {
        return this.mapsService.convertRing(ring);
    } 
}
angular.module('adapt.analyzer')
    .component('datacardBoundary', {
        controller: DatacardBoundaryComponent,
        template: require('./templates/datacard-boundary.template'),
        bindings: {
            boundary: '<'
        }
    })