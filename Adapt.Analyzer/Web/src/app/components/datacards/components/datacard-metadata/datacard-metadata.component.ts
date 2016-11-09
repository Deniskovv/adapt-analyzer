import * as angular from 'angular';

export class DatacardMetadataComponent {

}
angular.module('adapt.analyzer')
    .component('datacardMetadata', {
        template: require('./templates/datacard-metadata.template')
    });