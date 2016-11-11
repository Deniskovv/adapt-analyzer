import * as angular from 'angular';

import { 
    ApiService,
    Metadata
} from '../../../../shared';

export class DatacardMetadataComponent {
    static $inject = ['$state', 'ApiService'];

    metadata: Metadata;

    constructor(private $state: angular.ui.IStateService,
        private apiService: ApiService) {

    }

    $onInit() {
        let datacardId = this.$state.params['id'];
        this.apiService.get<Metadata>(`/datacards/${datacardId}/metadata`)
            .then(res => this.metadata = res.data);
    }
}
angular.module('adapt.analyzer')
    .component('datacardMetadata', {
        controller: DatacardMetadataComponent,
        template: require('./templates/datacard-metadata.template')
    });