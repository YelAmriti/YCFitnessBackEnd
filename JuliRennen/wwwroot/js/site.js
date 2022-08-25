let map;
let pos1 = { lat: 0, lng: 0 };
let markers = [];
let lastKnown = { lat: 0, lng: 0 };
let request;
let running = false;
let directionsService;
let directionsRenderer;
let locationButton;

function initMap() {
    var infoWindow = new google.maps.InfoWindow();
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer({
        draggable: true
    });

    // Kaart instantieren 
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
        center: { lat: 50, lng: 5 },
        mapTypeId: "terrain",
    });

    // Data voor route
    request = {
        origin: { lat: 0, lng: 0 },
        destination: { lat: 52.0938257748394, lng: 5.083128396831896 },
        travelMode: 'WALKING'
    };

    locationButton1 = document.createElement("button");
    locationButton2 = document.createElement("button");

    // Voeg knop toe voor pannen naar locatie.
    locationButton1.textContent = "Pan to Current Location";
    locationButton1.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton1);
    locationButton2.textContent = "Set route from current location";
    locationButton2.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton2);

    // START POS
    getStartLocation();

    // WATCH POS
    watchLocation();

    // CLICK POS
    getUserLoc();

    console.log("nu route neerzetten");

    //Route maken & tekenen
    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
            console.log("nu route neerzetten1");
        }
    });

    directionsRenderer.setMap(map);
}

function setRoute() {
    const coordArray = document.getElementById("selectList").value.split(" ");

    request.origin = {
        lat: parseFloat(coordArray[1].trim().replace(",", ".")),
        lng: parseFloat(coordArray[2].trim().replace(",", ".")),
    };
    request.destination = {
        lat: parseFloat(coordArray[3].trim().replace(",", ".")),
        lng: parseFloat(coordArray[4].trim().replace(",", ".")),
    };

    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
        }
    });
}

// Huidige locatie als start route nemen, einde is huidige locatie +0.01
function getStartLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                request.origin = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };
                request.destination = {
                    lat: (position.coords.latitude + 0.01),
                    lng: (position.coords.longitude + 0.01),
                };
                directionsService.route(request, function (result, status) {
                    if (status == 'OK') {
                        directionsRenderer.setDirections(result);
                    }
                });
            },
            () => {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

// Marker plaatsen op huidige locatie, volgende marker pas plaatsen wanneer 5 m bewogen
function watchLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(
            (position) => {
                var p = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };
                if (
                    markers.length == 0 ||
                    checkDistance(markers[markers.length - 1], p) >= 0.005
                ) {
                    markers.push(new google.maps.Marker({
                        position: p,
                        map: map,
                        icon: {
                            path: google.maps.SymbolPath.CIRCLE,
                            scale: 10,
                            fillOpacity: 1,
                            strokeWeight: 0,
                            fillColor: '#5384ED',
                        },
                    }));
                    map.setCenter(p);
                } else {
                    lastKnown = p;
                }
            }
        );
    }
}

// Naar huidige locatie pannen bij klikken knop
function getUserLoc() {
    locationButton1.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    pos1 = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };

                    map.setCenter(pos1);
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });
    locationButton2.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    request.origin = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };
                    request.destination = {
                        lat: (position.coords.latitude + 0.01),
                        lng: (position.coords.longitude + 0.01),
                    };
                    directionsService.route(request, function (result, status) {
                        if (status == 'OK') {
                            directionsRenderer.setDirections(result);
                        }
                    });
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });
}

window.initMap = initMap;

//Afstand huidige locatie en laatste marker checken
function checkDistance(mk1, mk2) {
    var R = 6371.0710; // Radius of the Earth in km
    var rlat1 = mk1.position.lat() * (Math.PI / 180); // Convert degrees to radians
    var rlat2 = mk2.lat * (Math.PI / 180); // Convert degrees to radians
    var difflat = rlat2 - rlat1; // Radian difference (latitudes)
    var difflon = (mk2.lng - mk1.position.lng()) * (Math.PI / 180); // Radian difference (longitudes)

    var d = 2 * R * Math.asin(Math.sqrt(Math.sin(difflat / 2) * Math.sin(difflat / 2) + Math.cos(rlat1) * Math.cos(rlat2) * Math.sin(difflon / 2) * Math.sin(difflon / 2)));
    console.log(d);
    return d;
}

function distanceFromStart(mk2) {
    var mk1 = request.origin;
    var R = 6371.0710; // Radius of the Earth in km
    var rlat1 = mk1.lat * (Math.PI / 180); // Convert degrees to radians
    var rlat2 = mk2.lat * (Math.PI / 180); // Convert degrees to radians
    var difflat = rlat2 - rlat1; // Radian difference (latitudes)
    var difflon = (mk2.lng - mk1.lng) * (Math.PI / 180); // Radian difference (longitudes)

    var d = 2 * R * Math.asin(Math.sqrt(Math.sin(difflat / 2) * Math.sin(difflat / 2) + Math.cos(rlat1) * Math.cos(rlat2) * Math.sin(difflon / 2) * Math.sin(difflon / 2)));
    console.log(d);
    return d;
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos1);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}

// Variabelen voor stopwatch
const time = document.querySelector('.stopwatch')
const mainButton = document.querySelector('#main-button')
const clearButton = document.querySelector('#clear-button')
const stopwatch = { elapsedTime: 0 }

// Evelisteners stopwatch knoppen
mainButton.addEventListener('click', () => {
    if (mainButton.innerHTML == 'Start' && distanceFromStart(lastKnown) <= 0.005) {
        startStopwatch();
        running = true;
        mainButton.innerHTML = 'Stop'
    } else if (mainButton.innerHTML == 'Stop') {
        stopwatch.elapsedTime += Date.now() - stopwatch.startTime
        clearInterval(stopwatch.intervalId)
        running = true;
        mainButton.innerHTML = 'Start'
    } else {
        alert("Get within 5m of starting point to start run");
    }
})

clearButton.addEventListener('click', () => {
    stopwatch.elapsedTime = 0
    stopwatch.startTime = Date.now()
    displayTime(0, 0, 0, 0)
})

// Functies stopwatch
function startStopwatch() {
    //reset start time
    stopwatch.startTime = Date.now();
    //run `setInterval()` and save id
    stopwatch.intervalId = setInterval(() => {
        //calculate elapsed time
        const elapsedTime = Date.now() - stopwatch.startTime + stopwatch.elapsedTime
        //calculate different time measurements based on elapsed time
        const milliseconds = parseInt((elapsedTime % 1000) / 10)
        const seconds = parseInt((elapsedTime / 1000) % 60)
        const minutes = parseInt((elapsedTime / (1000 * 60)) % 60)
        const hour = parseInt((elapsedTime / (1000 * 60 * 60)) % 24);
        //display time
        displayTime(hour, minutes, seconds, milliseconds)
    }, 100);
}

function displayTime(hour, minutes, seconds, milliseconds) {
    const leadZeroTime = [hour, minutes, seconds, milliseconds].map(time => time < 10 ? `0${time}` : time)
    time.innerHTML = leadZeroTime.join(':')
}


