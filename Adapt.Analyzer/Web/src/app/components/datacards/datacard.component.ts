import * as angular from 'angular';

import { datacardMapsState } from './components/datacard-maps/datacard-maps.state';
import { datacardMetadataState } from './components/datacard-metadata/datacard-metadata.state';
import { datacardTotalsState } from './components/datacard-totals/datacard-totals.state';

export class DatacardComponent {
    static $inject = ['$state'];
    selectedTab: number;

    constructor(private $state: angular.ui.IStateService) {

    }

    $onInit() {
        if (this.$state.includes(datacardMetadataState.name))
            this.selectedTab = 1;

        if (this.$state.includes(datacardTotalsState.name))
            this.selectedTab = 2;
    }

    goToMaps() {
        this.$state.go(datacardMapsState)
    }

    goToMetadata() {
        this.$state.go(datacardMetadataState)
    }

    goToTotals() {
        this.$state.go(datacardTotalsState)
    }
}

angular.module('adapt.analyzer')
    .component('datacard', {
        controller: DatacardComponent,
        template: require('./templates/datacard.template')
    });