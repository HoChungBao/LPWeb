Vue.filter('formatDate', function (value) {
    if (value) {
        return moment(String(value)).format('MM/DD/YYYY hh:mm:ss')
    }
});

Vue.component('vue-map', {
    template: '#map',
    props: {
        locations: {
            type: Array,
            default: function () { return [] }
        },
        option: {
            type: Object,
            default: function () {
                return {
                    zoom: 3,
                    center: { lat: -34.397, lng: 150.644 },
                }
            }
        }
    },
   
    data: function () {
        return {
            map: {},            
            markers: [],
            infowindows: []
        }
    },    
    mounted: function () {
        let el = this.$el;
        this.map = new google.maps.Map(el, this.option);
        //console.log(this.map)
        let bounds = new google.maps.LatLngBounds();

         //console.log(this.locations);

        for (let i = 0; i < this.locations.length; i++) {   
            let position = new google.maps.LatLng(this.locations[i].Latitue, this.locations[i].Longtitue);
            bounds.extend(position);
            this.setMarker(this.locations[i]);
        }

        //this.map.fitBounds(bounds);

        this.$emit('input', {
            map: this.map,
            marker: this.markers,
            infowindow: this.infowindows,
            location: this.locations
        });
    },
    methods: {
        // set marker
        // @param {Object} pos
        setMarker(pos) {
            let latlng = new google.maps.LatLng(pos.Latitue, pos.Longtitue);
            let marker = new google.maps.Marker({
                position: latlng,
                map: this.map,
                title: pos.title
            });

            this.map.setZoom(3);
            

            this.markers.push(marker);

            let infoWindow = new google.maps.InfoWindow();
            infoWindow.setContent('<div class="map__info">' + '<b>Tên User:</b> ' + pos.UserName + '</div>' +
                '<div class="map__info">' + '<b>Địa Chỉ CheckIn:</b> ' + pos.Address + '</div>');

            this.infowindows.push(infoWindow);
            // Setup event for marker
            google.maps.event.addListener(marker, 'mouseover', () => {
                infoWindow.open(this.map, marker);
            });
            

            google.maps.event.addListener(marker, 'mouseout', () => {
                infoWindow.close(this.map, marker);
            });

            google.maps.event.addListener(marker, 'click', () => {
                console.log("abc");
            });
        },    
        clearMarker() {
            for (var i = 0; i < this.markers.length; i++) {
                this.markers[i].setMap(null);
            }
            this.marker = [];          
            
        }
    },
    watch: {
        locations: function () {
            this.clearMarker();
            let bounds = new google.maps.LatLngBounds();

            for (let i = 0; i < this.locations.length; i++) {
                let position = new google.maps.LatLng(this.locations[i].Latitue, this.locations[i].Longtitue);
                bounds.extend(position);
                this.setMarker(this.locations[i]);
                
            }

            this.map.fitBounds(bounds);
            this.$emit('input', {
                map: this.map,
                marker: this.markers,
                infowindow: this.infowindows,
                 location: this.locations
            });
        }
    }
})
let vm = new Vue({
    el: '#app',
    data: {
        locations:[],
        map: null
    },
    //created: async function () {       
    //    var data = await axios.get(window.location.origin + '/UserGoogleMap/GetDataUserAddress')                   
        //    this.locations = data.data.userAddress; 
    //},   
    //methods: {
    //    setCenterLocate(e) {
    //        var i = e.target.dataset.markerid;            
    //        this.map.map.panTo(this.map.marker[i].position);
    //        this.map.map.setZoom(10);
    //        this.hideAllInfoWindow(this.map.infowindow);
    //        this.map.infowindow[i].open(this.map, this.map.marker[i])
    //    },
    //    hide(e) {           
    //        this.hideAllInfoWindow(this.map.infowindow);
    //    },
    //    hideAllInfoWindow(infowindows) {
    //        infowindows.forEach(function (infowindow) {
    //            infowindow.close();
    //        })
    //    },
        
    //}
});

function getFakeLocation() {
    return new Promise((res, rej) => {
        setTimeout(() => {
            res(locations)
        }, 5000)
    })
}

