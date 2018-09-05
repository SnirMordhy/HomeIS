$(document).ready(function () {

    $.getJSON('../content/json.json', function (data) {

        updateAppartmentList(data);

    });

});

function updateAppartmentList(data)
{
        $.each(data, function (index,appartment) {

            $('#appartment-grid').append(

                '<div class="col-sm-6 col-md-4">'
                + '<div class="thumbnail">'
                + '    <img alt="100%x200" src="' + appartment.photo + '" style="height: 200px; width: 100%; display: block;">'
                + '        <div class="caption ">'
                + '            <h4 class="thumbnail-caption-header">' + appartment.location.city + ', <small>' + appartment.location.neighborhood + '</small></h4>'
                + '            <div class="row">'
                + '                <div class="col-md-6">'
                + '                    <ul class="list-group">'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + appartment.size + '</span>'
                + '                            Sqr Meters'
                + '                        </li>'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + appartment.numberOfRooms + '</span>'
                + '                            # Rooms'
                + '                        </li>'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + appartment.balcony + '</span>'
                + '                            Balcony'
                + '                        </li>'
                + '                        <li class="list-group-item">'
                + '                            <span class="badge">' + appartment.floorNumber + '</span>'
                + '                            Floor #'
                + '                        </li>'
                + '                    </ul>'
                + '                </div>'
                + '                <div class="col-md-6">'
                + '                    <div style="min-height: 100%"><iframe width="100%" height="100%" src="https://maps.google.com/maps?width=100%&amp;height=100&amp;hl=en&amp;q=' + encodeURIComponent(appartment.location.address) + '+(My%20Business%20Name)&amp;ie=UTF8&amp;t=&amp;z=14&amp;iwloc=B&amp;output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe></div><br />'
                + '                </div>'
                + '            </div>'
                + '        </div>'
                + '    </div>'
                + '</div>'
         

            );

        });
}