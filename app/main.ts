import * as angular from 'angular';
import { MODULE_NAME } from './module.constants';

angular.module(MODULE_NAME, ['ngMaterial', 'ui.router']);

import './root';
import './welcome';
import './downloads';