import * as angular from 'angular';
import { ApiService } from '../../../../../shared';
import { FieldBoundary } from '../models';

export class DatacardMapsService {
    static $inject = ['ApiService'];

    constructor(private apiService: ApiService) {

    }

    getFieldBoundaries(datacardId: string): angular.IPromise<FieldBoundary[]> {
        return this.apiService.get<FieldBoundary[]>(`/datacards/${datacardId}/fields/boundaries`)
        .then(res => res.data);
    }
}
angular.module('adapt.analyzer')
    .service('DatacardMapsService', DatacardMapsService);