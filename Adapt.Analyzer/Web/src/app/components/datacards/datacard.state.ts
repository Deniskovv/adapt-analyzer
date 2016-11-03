import { IState } from 'angular-ui-router';

import './datacard.component';
export const datacardState: IState = {
    name: 'datacard',
    url: '/datacards/:id',
    template: '<datacard></datacard>'
}