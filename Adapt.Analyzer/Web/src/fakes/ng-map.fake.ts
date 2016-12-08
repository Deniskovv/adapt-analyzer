import * as angular from 'angular';

import { Map } from './google-map.fake';

export class NgMapFake {
    private _$q: angular.IQService;
    private _map;    
    private _defaultOptions;

    get defaultOptions() {
        return this._defaultOptions;
    }

    constructor() {
        
    }

    initialize($q: angular.IQService) : void {
        this._$q = $q;
        this._map = new Map(null);
        this._defaultOptions = {
            marker: {}
        }
    }

    getMap() : angular.IPromise<any> {
        return this._$q.resolve(this._map);
    }

    setStyle(element) {

    }

    observeAndSet(attrName, obj) {
        return (val) => {

        };
    }

    showInfoWindow(id) {

    }
} 