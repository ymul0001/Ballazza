mapboxgl.accessToken = 'pk.eyJ1IjoieW9uczIyMjIiLCJhIjoiY2tmZGhhMmd2MDZ6MjJ5cDl2a2pnM2c0ciJ9.azj1qrJYfR2iaXXH4GQUSQ';


/*
 * Generating the map
 * 
 * */
const map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v11',
    center: [144.970312785, -37.8068717725], // starting position
    zoom: 11, // starting zoom
});
map.addControl(new mapboxgl.NavigationControl());


/**
 * Retrieving the location data from the back-end
 * */
$(document).ready(function () {
    $.ajax({
        url: "/Venues/getVenuesList",
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            //console.log(res.data[1]);
            for (let i = 0; i < res.data.length; i++) {
                const data = res.data[i]
                url = "https://api.mapbox.com/geocoding/v5/mapbox.places/" + data.VenueAddress + ".json?access_token=pk.eyJ1IjoieW9uczIyMjIiLCJhIjoiY2tmZGhhMmd2MDZ6MjJ5cDl2a2pnM2c0ciJ9.azj1qrJYfR2iaXXH4GQUSQ"
                $.ajax({
                    url: url,
                    type: 'GET',
                    dataType: 'json', // added data type
                    success: function (res) {
                        //console.log(res.features[0].geometry.coordinates)

                        // create the popup information for each marker
                        var popup = new mapboxgl.Popup({ offset: 25 }).setHTML(
                            '<h3>' + data.VenueName + '</h3><p>' + data.VenueAddress + '</p>'
                        );

                  
                        // Create the marker
                        marker = new mapboxgl.Marker({
                            color: 'red'
                        }).setLngLat(res.features[0].geometry.coordinates)
                            .setPopup(popup)
                            .addTo(map);
                    }
                });
            }
        }
    });
})

//implement options to change the layer styles
const switchLayer = (layer) => {
    const layerId = layer.target.id;
    map.setStyle('mapbox://styles/mapbox/' + layerId);
}

const menus = document.getElementById('menu').getElementsByTagName('input');

for (let i = 0; i < menus.length; i++) {
    menus[i].onclick = switchLayer;
}


document.getElementById('fly').addEventListener('click', function () {
    map.flyTo({
        center: [144.970312785, -37.8068717725],
        essential: true 
    });
});