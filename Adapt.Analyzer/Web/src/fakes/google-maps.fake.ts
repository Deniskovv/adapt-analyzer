import { LatLng } from './google-latlng.fake';
import { InfoWindow } from './google-info-window.fake';
import { Map } from './google-map.fake';
import { Polygon } from './google-polygon.fake';

export const maps = {
    OverlayView: () => {

    },
    Marker: () => {

    },
    LatLng: LatLng,
    Map: Map,
    InfoWindow: InfoWindow,
    Polygon: Polygon,
    MapTypeId: { ROADMAP: true },
    event: {
        addListener: (marker, eventName, event) => {

        },
        addListenerOnce: () => {

        },
        trigger: () => {

        }
    },
    places: {
        AutocompleteService: () => {

        },
        PlacesService: (obj) => {
            return {
                PlacesServiceStatus: {
                    OK: true
                },
                textSearch: (query) => {
                    return [];
                },
                nearbySearch: (query) => {
                    return [];
                }
            }
        }
    }
};

export default maps;