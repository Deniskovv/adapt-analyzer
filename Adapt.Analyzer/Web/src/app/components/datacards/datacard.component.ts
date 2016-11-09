import * as angular from 'angular';

import './components/datacard-maps/datacard-maps.component';
import './components/datacard-metadata/datacard-metadata.component';
import './components/datacard-totals/datacard-totals.component';
export class DatacardComponent {
}
angular.module('adapt.analyzer')
    .component('datacard', {
        controller: DatacardComponent,
        template: require('./templates/datacard.template')
    });