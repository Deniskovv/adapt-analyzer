import * as angular from 'angular';

import { DatacardMapsService } from './services/datacard-maps.service';
import { MapsService } from './services/maps.service';
import { FieldBoundary, Point } from './models';

import './components/datacard-field-boundary/datacard-field-boundary.component';
import './services/datacard-maps.service';
import './services/maps.service';
import './styles/datacard-maps.scss';
export class DatacardMapsComponent {
    static DefaultCenter: Point = { latitude: -93.563235, longitude: 41.363729 };
    static $inject = ['$state', 'NgMap', 'DatacardMapsService', 'MapsService']

    fieldBoundaries: FieldBoundary[];
    get hasboundaries(): boolean {
        return this.fieldBoundaries
            && this.fieldBoundaries.length > 0
    }
    
    get center(): number[] {
        return this.hasboundaries
            ? this.convertPoint(this.fieldBoundaries[0].centerPoint)
            : this.convertPoint(DatacardMapsComponent.DefaultCenter);
    }
    

    constructor(private $state: angular.ui.IStateService,
        private ngMap,
        private datacardMapsService: DatacardMapsService,
        private mapsService: MapsService) {

    }

    $onInit() {
        let datacardId = this.$state.params['id'];
        this.datacardMapsService.getFieldBoundaries(datacardId)
            .then(data => this.fieldBoundaries = data);
    }

    convertPoint(point: Point): number[] {
        return this.mapsService.convertPoint(point);
    }
}
angular.module('adapt.analyzer')
    .component('datacardMaps', {
        controller: DatacardMapsComponent,
        template: require('./templates/datacard-maps.template')
    })