import { Boundary } from './boundary';
import { Point } from './point';

export interface FieldBoundary {
    description?: string;
    centerPoint?: Point;
    boundaries?: Boundary[];
}