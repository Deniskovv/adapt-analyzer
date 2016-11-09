import * as angular from 'angular';

import { Config } from '../models';

export class ConfigService {
    static $inject = ['$http']

    constructor(private $http: angular.IHttpService) {

    }

    getConfig(): angular.IPromise<Config> {
        return this.$http.get<Config>('/config.json')   
            .then(res => res.data);
    }
}
angular.module('adapt.analyzer')
    .service('ConfigService', ConfigService);