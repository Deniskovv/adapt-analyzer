import * as angular from 'angular';

import { 
    ApiService,
    DatacardTotal 
} from '../../../../shared';

import './components/datacard-total/datacard-total.component';
export class DatacardTotalsComponent {
    static $inject = ['$state', 'ApiService'];

    totals: DatacardTotal;

    constructor(private $state: angular.ui.IStateService,
        private apiService: ApiService) {

    }

    $onInit() {
        let datacardId = this.$state.params['id'];
        this.apiService.get<DatacardTotal>(`/datacards/${datacardId}/totals`)
            .then(res => this.totals = res.data);
    }
}
angular.module('adapt.analyzer')
    .component('datacardTotals', {
        controller: DatacardTotalsComponent,
        template: require('./templates/datacard-totals.template')
    });