var inputedMoney = 0;
const BOX_URI = 'https://localhost:5001';

$(document).ready(function () {

    // selectable item to purchase
    $(".buyableItem").click((event) => {
        for (item of $(".buyableItem")) {
            $(item).css("border", "none");
        }
        $(event.currentTarget).css("border", "5px solid red");
        $("#selectedItem").text(event.currentTarget.id);

        $("#purchase-button").css("display", "block");
    });



    $('#add-cent-button').on('click', function () {
        addMoney('add-cent');

    });

    $('#add-ten-cent-button').on('click', function () {
        addMoney('add-ten-cent');
    });

    $('#add-quarter-button').on('click', function () {
        addMoney('add-quarter');
    });

    $('#add-dollar-button').on('click', function () {
        addMoney('add-dollar');
    });

    $('#add-five-dollar-button').on('click', function () {
        addMoney('add-five-dollar');
    });

    $('#add-twenty-dollar-button').on('click', function () {
        addMoney('add-twenty-dollar');
    });

    $('#take-money-button').click(function () {
        takeMoney();
    });

    function updateMoneyState(money) {
        $("#money-inside-lbl").text(money.moneyInside.amount);
        $("#money-charged-lbl").text(money.moneyCharged);
        $("#qt_1c").text(money.moneyInside.oneCentCount);
        $("#qt_10c").text(money.moneyInside.tenCentCount);
        $("#qt_25c").text(money.moneyInside.quarterCount);
        $("#qt_1d").text(money.moneyInside.oneDollarCount);
        $("#qt_5d").text(money.moneyInside.fiveDollarCount);
        $("#qt_20d").text(money.moneyInside.twentyDollarCount);
    }

    function takeMoney() {

        $.ajax({
            type: 'POST',
            url: `${BOX_URI}/command-atm/take-money/${$("#money-input").val()}`,
            success: function (data) {
                alert(data.message);
                updateMoneyState(data);
            },
            error: function (error) {
                var errorMessage = error.responseJSON.message;
                alert(errorMessage);
                alert(errorMessage);
            }
        });
    }
});