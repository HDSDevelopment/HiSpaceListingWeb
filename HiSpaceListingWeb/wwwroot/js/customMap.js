// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
//$(document).ready(function () {

	//function mapData() {
	var searchInput = 'pac-input';
	function initMap() {
		var placeSearch, autocomplete;

		var componentForm = {
			street_number: 'long_name',
			route: 'long_name',
			locality: 'long_name',
			administrative_area_level_1: 'long_name',
			country: 'long_name',
			postal_code: 'long_name'
		};
		var map = new google.maps.Map(document.getElementById('map'), {
			center: { lat: -33.8688, lng: 151.2195 },
			zoom: 13
		});
		var card = document.getElementById('pac-card');
		var input = document.getElementById('pac-input');
		var types = document.getElementById('type-selector');
		var strictBounds = document.getElementById('strict-bounds-selector');


		map.controls[google.maps.ControlPosition.TOP_RIGHT].push(card);

		var autocomplete = new google.maps.places.Autocomplete(input);

		// Avoid paying for data that you don't need by restricting the set of
		// place fields that are returned to just the address components.
		autocomplete.setFields(['address_component']);

		// When the user selects an address from the drop-down, populate the
		// address fields in the form.
		autocomplete.addListener('place_changed', fillInAddress);

		google.maps.event.addListener(autocomplete, 'place_changed', function () {
			var near_place = autocomplete.getPlace();
			console.log(near_place.geometry);
			document.getElementById('loc_lat').value = near_place.geometry.location.lat();
			document.getElementById('loc_long').value = near_place.geometry.location.lng();
			//console.log($('#loc_lat').val());
			$('#latitude_view').val($('#loc_lat').val()).trigger("change");
			$('#longitude_view').val($('#loc_long').val()).trigger("change");
			$('#disp_lat').html($('#loc_lat').val());
			$('#disp_lan').html($('#loc_long').val());
			//console.log($('#latitude_view').val());
			//document.getElementById('latitude_view').value = near_place.geometry.location.lat();
			//document.getElementById('longitude_view').value = near_place.geometry.location.lng();
		});


		// Bind the map's bounds (viewport) property to the autocomplete object,
		// so that the autocomplete requests use the current map bounds for the
		// bounds option in the request.
		autocomplete.bindTo('bounds', map);

		// Set the data fields to return when the user selects a place.
		autocomplete.setFields(
			['address_components', 'geometry', 'icon', 'name']);

		var infowindow = new google.maps.InfoWindow();
		var infowindowContent = document.getElementById('infowindow-content');
		infowindow.setContent(infowindowContent);
		var marker = new google.maps.Marker({
			map: map,
			anchorPoint: new google.maps.Point(0, -29)
		});

		autocomplete.addListener('place_changed', function () {
			infowindow.close();
			marker.setVisible(false);
			var place = autocomplete.getPlace();
			if (!place.geometry) {
				// User entered the name of a Place that was not suggested and
				// pressed the Enter key, or the Place Details request failed.
				window.alert("No details available for input: '" + place.name + "'");
				return;
			}

			// If the place has a geometry, then present it on a map.
			if (place.geometry.viewport) {
				map.fitBounds(place.geometry.viewport);
			} else {
				map.setCenter(place.geometry.location);
				map.setZoom(17);  // Why 17? Because it looks good.
			}
			marker.setPosition(place.geometry.location);
			marker.setVisible(true);

			var address = '';
			if (place.address_components) {
				address = [
					(place.address_components[0] && place.address_components[0].short_name || ''),
					(place.address_components[1] && place.address_components[1].short_name || ''),
					(place.address_components[2] && place.address_components[2].short_name || '')
				].join(' ');
			}

			infowindowContent.children['place-icon'].src = place.icon;
			infowindowContent.children['place-name'].textContent = place.name;
			infowindowContent.children['place-address'].textContent = address;
			infowindow.open(map, marker);
		});

		// Sets a listener on a radio button to change the filter type on Places
		// Autocomplete.
		function setupClickListener(id, types) {
			var radioButton = document.getElementById(id);
			radioButton.addEventListener('click', function () {
				autocomplete.setTypes(types);
			});
		}

		setupClickListener('changetype-all', []);
		setupClickListener('changetype-address', ['address']);
		setupClickListener('changetype-establishment', ['establishment']);
		setupClickListener('changetype-geocode', ['geocode']);

		document.getElementById('use-strict-bounds')
			.addEventListener('click', function () {
				console.log('Checkbox clicked! New state=' + this.checked);
				autocomplete.setOptions({ strictBounds: this.checked });
			});


		function fillInAddress() {
			// Get the place details from the autocomplete object.
			var place = autocomplete.getPlace();

			for (var component in componentForm) {
				document.getElementById(component).value = '';
				document.getElementById(component).disabled = false;
			}

			// Get each component of the address from the place details,
			// and then fill-in the corresponding field on the form.
			for (var i = 0; i < place.address_components.length; i++) {
				var addressType = place.address_components[i].types[0];
				if (componentForm[addressType]) {
					var val = place.address_components[i][componentForm[addressType]];
					document.getElementById(addressType).value = val;
				}
			}
		}

		// Bias the autocomplete object to the user's geographical location,
		// as supplied by the browser's 'navigator.geolocation' object.
		function geolocate() {
			if (navigator.geolocation) {
				navigator.geolocation.getCurrentPosition(function (position) {
					var geolocation = {
						lat: position.coords.latitude,
						lng: position.coords.longitude
					};
					var circle = new google.maps.Circle(
						{ center: geolocation, radius: position.coords.accuracy });
					autocomplete.setBounds(circle.getBounds());
				});
			}
		}
	}
	initMap();	
	//};
//});
$(document).on('change', '#pac-input', function () {
	//document.getElementById('latitude_input').value = "";
	//document.getElementById('longitude_input').value = "";

	document.getElementById('latitude_view').value;
	document.getElementById('longitude_view').value;
	//initMap();
});