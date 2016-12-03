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

    get id(): number {
        return this.fieldBoundary
            && this.fieldBoundary.id;
    }

    get markerId(): string {
        return this.fieldBoundary
            && `${this.fieldBoundary.id}-marker`
    }

    constructor(private mapsService: MapsService) {
        this.toggleInfoWindow = this.toggleInfoWindow.bind(this);
    }

    toggleInfoWindow(): void {
        this.mapsService.showInfoWindow(`${this.fieldBoundary.id}`);
    }
}
angular.module('adapt.analyzer')
    .component('datacardFieldBoundary', {
        template: require('./templates/datacard-field-boundary.template'),
        controller: DatacardFieldBoundaryComponent,
        bindings: {
            fieldBoundary: '<'
        }
    })