window.mapInstance = null;
window.markers = {};

window.initMap = (elementId) => {
    if (window.mapInstance) return;

    window.mapInstance = L.map(elementId).setView([40.7128, -74.0060], 13); // NYC

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap'
    }).addTo(window.mapInstance);
};

window.addMarker = (id, lat, lon, title) => {
    if (!window.mapInstance) return;

    if (window.markers[id]) {
        window.markers[id].setLatLng([lat, lon]);
    } else {
        var marker = L.marker([lat, lon]).addTo(window.mapInstance)
            .bindPopup(title);
        window.markers[id] = marker;
    }
};
