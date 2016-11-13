import { FieldTotal } from './field-total';

export interface PluginTotal {
    pluginName?: string;
    pluginVersion?: string;
    fieldTotals?: FieldTotal[];
}