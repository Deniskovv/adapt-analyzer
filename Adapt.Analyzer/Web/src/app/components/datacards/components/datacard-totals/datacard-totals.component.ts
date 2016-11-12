import * as angular from 'angular';

import { 
    ApiService,
    Totals 
} from '../../../../shared';

export class DatacardTotalsComponent {
    static $inject = ['$state', 'ApiService'];

    totals: Totals;

    constructor(private $state: angular.ui.IStateService,
        private apiService: ApiService) {

    }

    $onInit() {
        let datacardId = this.$state.params['id'];
        this.apiService.get<Totals>(`/datacards/${datacardId}/totals`)
            .then(res => this.totals = res.data);
    }
}
angular.module('adapt.analyzer')
    .component('datacardTotals', {
        controller: DatacardTotalsComponent,
        template: require('./templates/datacard-totals.template')
    });