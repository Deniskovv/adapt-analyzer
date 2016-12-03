export function LatLng(lat, lng) {

}

export function Map(obj) {
    this.obj = obj;
}
Map.prototype.setOptions = function(options) {
    this.options = options;
}

export function InfoWindow(options) {
    this.options = options;
}

export const maps = {
    OverlayView: () => {

    },
    Marker: () => {

    },
    LatLng: LatLng,
    Map: Map,
    InfoWindow: InfoWindow,
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