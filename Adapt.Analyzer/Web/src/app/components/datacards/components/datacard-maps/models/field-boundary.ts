import { Boundary } from './boundary';
import { Point } from './point';

export interface FieldBoundary {
    id?: number;
    description?: string;
    centerPoint?: Point;
    isOpen?: boolean;
    boundaries?: Boundary[];
}