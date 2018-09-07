document.addEventListener("DOMContentLoaded", function (event) {
    var fromCoinsExc;
    var toCoinsExc;

    $('#from-exchange-coin li').click(function () {
        fromCoinsExc = $(this).text();
        $('#from-exchange-coin-value').text(fromCoinsExc);
        isCheckAllowed();
    });

    $('#to-exchange-coin li').click(function () {
        toCoinsExc = $(this).text();
        $('#to-exchange-coin-value').text(toCoinsExc);
        isCheckAllowed();
    });

    function isCheckAllowed() {
        if (fromCoinsExc != undefined && toCoinsExc != undefined) {
            $('#exchangeCoinsFormat').prop('disabled', false);
        }
    }

    function setSelected(id) {
        $('#' + id + ' li').removeClass('selected');
        $(this).addClass('selected');
    }

    $('#exchangeCoinsFormat').click(function () {
        $.ajax({
            url: "https://www.alphavantage.co/query",
            method: "GET",
            data: {
                function: "CURRENCY_EXCHANGE_RATE",
                from_currency: fromCoinsExc,
                to_currency: toCoinsExc,
                apikey: "MBBY8XQRGJKF68S6"
            }

        }).done(function (data) {
            var exchangeRate = data["Realtime Currency Exchange Rate"]["5. Exchange Rate"];
            $('#exchangeAnswerAlert').prop("hidden", false);
            $('#exchangeAnswer').text(exchangeRate);
        })
    });
});