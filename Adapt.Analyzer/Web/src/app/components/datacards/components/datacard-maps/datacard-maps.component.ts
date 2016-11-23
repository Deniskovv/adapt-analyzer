import * as angular from 'angular';

import { DatacardMapsService } from './services/datacard-maps.service';
import { FieldBoundary, Point } from './models';

import './services/datacard-maps.service';
import './styles/datacard-maps.scss';
export class DatacardMapsComponent {
    static $inject = ['$state', 'DatacardMapsService']

    fieldBoundaries: FieldBoundary[];

    constructor(private $state: angular.ui.IStateService,
        private datacardMapsService: DatacardMapsService) {

    }

    $onInit() {
        let datacardId = this.$state.params['id'];
        this.datacardMapsService.getFieldBoundaries(datacardId)
            .then(data => this.fieldBoundaries = data);
    }

    convertPoint(point: Point): number[] {
        return [point.longitude, point.latitude];
    }
}
angular.module('adapt.analyzer')
    .component('datacardMaps', {
        controller: DatacardMapsComponent,
        template: require('./templates/datacard-maps.template')
    })