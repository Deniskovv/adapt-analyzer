import * as angular from 'angular';

import { FieldBoundary } from '../../models';
import { MapsService } from '../../services/maps.service';

export class DatacardFieldBoundaryComponent {
    static $inject = ['MapsService'];

    fieldBoundary: FieldBoundary;

    get center(): number[] {
        return this.fieldBoundary 
        && this.fieldBoundary.centerPoint
        && this.mapsService.convertPoint(this.fieldBoundary.centerPoint);
    }

    get title(): string {
        return this.fieldBoundary
            && this.fieldBoundary.description;
    }

    constructor(private mapsService: MapsService) {}
}
angular.module('adapt.analyzer')
    .component('datacardFieldBoundary', {
        template: require('./templates/datacard-field-boundary.template'),
        controller: DatacardFieldBoundaryComponent,
        bindings: {
            fieldBoundary: '<'
        }
    })