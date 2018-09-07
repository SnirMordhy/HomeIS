$(document).ready(function () {

    //$.getJSON('../content/json.json', function (data) {

    //    window.apartments = data;
    //    updateApartmentList(data);

    //});

    $.ajax({
        dataType: "json",
        url: "/Apartments/AllApartmentsJSON",
        success: function (data) { updateApartmentList(data) }
    });

});

function updateApartmentList(data)
{
        $.each(data.apartments, function (index,apartment) {

            $('#apartment-grid').append(

                '<div class="col-sm-6 col-md-4">'
                + '<div class="thumbnail">'
                + '    <img alt="100%x200" src="' + apartment.photo + '" style="height: 200px; width: 100%; display: block;">'
                + '        <div class="caption ">'
                + '            <h4 class="thumbnail-caption-header">' + apartment.Location.City + ', <small>' + apartment.Location.Neighborhood + '</small></h4>'
                + '            <div class="row">'
                + '                <div class="col-md-6">'
                + '                    <ul class="list-group">'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + apartment.Size + '</span>'
                + '                            Sqr Meters'
                + '                        </li>'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + apartment.NumberOfRooms + '</span>'
                + '                            # Rooms'
                + '                        </li>'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + (apartment.Balcony ? "Yes" : "No") + '</span>'
                + '                            Balcony'
                + '                        </li>'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + apartment.FloorNumber + '</span>'
                + '                            Floor #'
                + '                        </li>'
                + '                    </ul>'
                + '                </div>'
                + '                <div class="col-md-6">'
                + '                    <div style="min-height: 100%"><iframe width="100%" height="100%" src="https://maps.google.com/maps?width=100%&amp;height=100&amp;hl=en&amp;q=' + encodeURIComponent(apartment.Location.Address) + '+(My%20Business%20Name)&amp;ie=UTF8&amp;t=&amp;z=14&amp;iwloc=B&amp;output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe></div><br />'
                + '                </div>'
                + '            </div>'
                + '        </div>'
                + '    </div>'
                + '</div>'
         

            );

        });
}