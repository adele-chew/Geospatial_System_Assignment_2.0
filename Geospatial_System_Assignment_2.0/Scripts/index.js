let map;

function initMap() {                                                        //function to initialize the map
    const map = new google.maps.Map(document.getElementById("map"), {       //new map variable
        center: { lat: 1.3521, lng: 103.8198 },                             //map is centered to singapore
        zoom: 13,                                                           //zoom-level at 13 on load
    });

    $.ajax({                                                                //using ajax
        type: "GET",                                                        //GET method
        async: true,                                                        //asynchronous load is true
        url: "collaborator.xml",                                            //url to collaborator file
        dataType: "xml",                                                    //datatype is xml
        success:                                                            //if successful do the following
            function (xml) {
                var info;                                                   //string to containt info window content
                var collab = xml.documentElement.getElementsByTagName("Collaborator");  //gets all elements w tag name collaborator
                    
                for (var i = 0; i < collab.length; i++) {                   //for-loop to loop through all collaborator elements in xml
                    var cat = "collaborator";
                    var name = collab[i].getAttribute('name');              //get attribute 'name' from xml containing collaborator's name
                    var type = collab[i].getAttribute('type');              //get attribute 'type' from xml containing the collaborator's support type
                    var time = collab[i].getAttribute('time');              //get attribute 'time' from xml containing the collaborator's available times
                    var lat = collab[i].getAttribute('lat');                //get attribute 'lat' from xml containing latitude numbers
                    var lon = collab[i].getAttribute('lon');                //get attribute 'lon' from xml containing longitude numbers
                    var contact = collab[i].getAttribute('contact');        //get attribute 'contact' from xml containing contact information
                    var pic = {
                        url: collab[i].getAttribute('pic'),                 //get attribute 'pic' from xml containing the relative link to the picture
                        scaledSize: new google.maps.Size(50, 50),           // scaled size
                        origin: new google.maps.Point(0, 0),                // origin
                        anchor: new google.maps.Point(0, 0)                 // anchor
                    }

                    var latLng = new google.maps.LatLng(lat, lon);          //creates a new gmap latitude and longitude point
                    var marker = new google.maps.Marker({                   //creates a new gmap marker
                        position: latLng,                                   //sets the position
                        map: map,                                           //defines the map
                        category: cat,                                      //category of marker
                        icon: pic,                                          //custom marker icon
                        title: name                                         //hover tooltip text
                    });

                    info = "<p> Name: " + name + " </p>";                   //strings containing collaborator information for info window
                    info += "<p> Type: " + type + " </p>";
                    info += "<p> Time: " + time + " </p>";
                    info += "<p> Contact: " + contact + " </p>";

                    var infowindow = new google.maps.InfoWindow({           //creates a new gmap info window for the marker
                        content: info                                       //sets the content of the info window    
                    });

                    marker.addListener('click', function () {               //event listener for when mouse clicks on the marker
                        infowindow.open({                                   //opens info window
                            anchor: marker,                                 //anchored to the marker and map
                            map,
                            shouldFocus: false,                             //specifies whether or not focus should move into the window when clicked
                        });
                    });

                    map.addListener("click", (mapsMouseEvent) => {                     //event listener for when the mouse clicks on the map
                        var point = mapsMouseEvent.latLng;                              //on mouse click, assign coordinates to variable point 
                            
                        document.getElementById("addCollabPt").value = point;           //set hidden field input to coordinates value
                    });
                }
            }
    });
}
