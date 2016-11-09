import * as angular from 'angular';

import { ConfigService } from './config.service';

export class ApiService {
    static $inject = ['$http', 'ConfigService']
    constructor(private $http: angular.IHttpService,
        private configService: ConfigService) {

    }

    get<T>(url): angular.IHttpPromise<T> {
        return this.configService.getConfig()
            .then(c => this.$http.get<T>(`${c.api_url}${url}`));
    }

    post<T>(url, data): angular.IHttpPromise<T>  {
        return this.configService.getConfig()
            .then(c => this.$http.post<T>(`${c.api_url}${url}`, data));
    }
}

angular.module('adapt.analyzer')
    .service('ApiService', ApiService);