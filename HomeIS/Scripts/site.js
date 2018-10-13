$(document).ready(function () {
    $.ajax({
        dataType: "json",
        url: "/Apartments/AllApartmentsJSON",
        success: function (data) {
            window.apartments = data;
            updateApartmentList(data);
        }
    });

    $(document).ready(function () {

        $('.apartment-view-image-delete-button').click(function () {
            if ($(".apartment-view-image-container").length <= 1) {
                alert("There needs to be at least one photo!")
            } else {
                $('#Photos')[0].value = $('#Photos')[0].value.replace(this.dataset['img'] + ',', "");
                $('#Photos')[0].value = $('#Photos')[0].value.replace(',' + this.dataset['img'], "");
                $('#Photos')[0].value = $('#Photos')[0].value.replace(this.dataset['img'], "");
                this.parentElement.parentElement.remove();
            }
        });
    });
    
    $("#apartment-filter-form").submit(function (e) {

        var showall = $("#show-all-filter").is(":checked");

        if (showall) {
            var els = $("#apartment-filter");
            var len = els.length;

            for (var i = 0; i <= len; i++) {
                els[i].dsabled = true;
            }

            getAllApartmentsJSON();
        }
        else {
            var minprice = $("#min-price-filter").val();

            if (minprice == "") {
                $("#min-price-filter").val(0);
            }

            var maxprice = $("#max-price-filter").val();

            if (maxprice != null) {
                if (maxprice == "") {
                    getSizeBalconyMinOrMaxPriceJSON();
                }
                else {
                    getSizeBalconyPriceRangeJSON();
                }
            }
        }

        e.preventDefault();
    });
});

function updateModalData(apartment) {

    $.get("/Apartments/PredictApartmentSale",
        {
            "size": apartment.Size,
            "value": apartment.PropertyValue,
            "floor": apartment.FloorNumber
        },
        function(data) {
            if (data === "True") {

                $("#predict-label")[0].className = "label label-success";
                $("#predict-label")[0].innerText = "Predicted to be sold!";

            } else {

                $("#predict-label")[0].className = "label label-danger";
                $("#predict-label")[0].innerText = "Not predicted to be sold";
            }
        }
    );

    $('.carousel-indicators').empty();
    $('.carousel-inner').empty();
    $('.modal-caption').empty();

    $.each(apartment.PhotoList, function (index, photo) {

        $('.carousel-indicators').append('<li data-target="#apartment-photos-carousel" class="' + (index == 0 ? "active" : "") + '" data-slide-to="' + index + '"></li>');
        $('.carousel-inner').append('<div style="background-image:url(' + "\'" + photo + "\'" + ')" class="item ' + (index == 0 ? "active" : "") + '"><img style="display: none" src="' + photo + '"></div>');
    });

    $('.modal-caption').append(
        '            <h4 class="thumbnail-caption-header">' + apartment.Location.City + ', <small>' + apartment.Location.Neighborhood + '</small><span id="predict-label" class="label label-default">Checking...</span></h4>'
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

    );
};

function updateApartmentList(data) {
    $('#apartment-grid div').hide();
    if (!$.trim(data)) {
        $('#apartment-grid').append(
            '<div class="alert alert-danger" id="alertTransError" role="alert">No results found</div>'
        );
    } else {
        $.each(data, function (index, apartment) {
            $('#apartment-grid').append(
                '<div class="col-sm-6 col-md-4">'
                + '<div class="thumbnail">'
                + '    <a href="#"><img data-apartmentid=' + index + ' src = "' + apartment.PhotoList[0] + '" class= "apartment-image" style = "height: 200px; width: 100%; display: block;"></a> '
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
                + '        <div class="buy-button-div"><button type="submit" data-apartmentid=' + index + ' class= "btn btn-success buyProperty">Buy Now!</button><div>'
                + '        <br /><br />'
                + '    </div>'
                + '</div>'
            );
        });
    }
    
    $('.apartment-image').click(function () {
        var apartmentData = window.apartments[parseInt(this.dataset['apartmentid'])];
        updateModalData(apartmentData)

        $('#apartment-modal').modal('toggle')
    });

    $('.buyProperty').click(function () {
        var apartmentData = window.apartments[parseInt(this.dataset['apartmentid'])];
        updateTransactionModalData(apartmentData);
        $('#transaction-modal').modal('toggle');
    });

}

function updateTransactionModalData(apartmentData) {
    $('#transactionModalTitle').text(apartmentData.Description);
    $('#transactionPayment').prop("min", apartmentData.PropertyValue);
    $('#transactionPayment').prop("placeholder", apartmentData.PropertyValue);

    $('.form-control').keypress(function () {
        isCheckAllowed();
    });

    $('#createTransaction').click(function () {
        $.ajax({
            url: "/Transactions/CreateTransaction",
            method: "POST",
            data: {
                apartment: apartmentData,
                buyingPrice: parseInt($('#transactionPayment').val())
            }
        })
            .done(function () {
                $('#alertTransSuccess').prop('hidden', false);
                $('#alertTransError').prop('hidden', true);
                setTimeout(function () {
                    $('#transaction-modal').modal('hide');
                }, 3000);
            })
            .fail(function (error) {

                if (error.status == 409) {
                    $('#alertTransError').text("דירה זו שייכת לך בטאבו, אנא בחר דירה אחרת");
                }
                $('#alertTransError').prop('hidden', false);
                $('#alertTransSuccess').prop('hidden', true);
            })
    });

}

function isCheckAllowed() {
    if ($('#transactionPayment').val() !== '' &&
        $('#cardNumber').val() !== '' &&
        $('#cardExpiry').val() !== '' &&
        $('#cardCVC').val() !== '') {
        $('#createTransaction').prop('disabled', false);
    }
}

function getAllApartmentsJSON() {
    $.ajax({
        dataType: "json",
        url: "/Apartments/AllApartmentsJSON",
        success: function (data) {
            updateApartmentList(data);
        }
    });
}

function getSizeBalconyMinOrMaxPriceJSON() {
    $.ajax({
        dataType: "json",
        url: "/Apartments/SizeBalconyMinOrMaxPriceJSON",
        data: {
            Balcony: $("#balcony-filter").is(":checked"),
            Size: $("#size-filter").val(),
            MinimumPrice: $("#min-price-filter").val()
        },
        success: function (data) {
            updateApartmentList(data);
        }
    });
}

function getSizeBalconyPriceRangeJSON() {
    $.ajax({
        dataType: "json",
        url: "/Apartments/SizeBalconyPriceRangeJSON",
        data: {
            Balcony: $("#balcony-filter").is(":checked"),
            Size: $("#size-filter").val(),
            MinimumPrice: $("#min-price-filter").val(),
            MaximumPrice: $("#max-price-filter").val()
        },
        success: function (data) {
            updateApartmentList(data);
        }
    });
}