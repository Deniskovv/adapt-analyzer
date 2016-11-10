import * as angular from 'angular';

import './styles/datacard-maps.scss';
export class DatacardMapsComponent {
    
}
angular.module('adapt.analyzer')
    .component('datacardMaps', {
        template: require('./templates/datacard-maps.template')
    })