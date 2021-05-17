var inputedMoney = 0;
const BOX_URI = 'https://localhost:5001';

$(document).ready(function () {
    
    function addMoney(route) {
        $.ajax({
            type: 'POST',
            url: `${BOX_URI}/command/${route}`,
            success: function (moneyData) {
                console.log('')
                console.log('------------------ log while updating the box money ------------------')
                console.log('')
                console.log(moneyData);
                console.log('')
                console.log('------------------ log while updating the box money ------------------')
                console.log('')
                console.log('');
                messageBox("You added " + moneyData.Added);
                updateMoneyState(moneyData)
            },
            error: function (err) {
                console.error('')
                console.error('------------------ error while updating the box money ------------------')
                console.error('')
                console.error(err);
                console.error('')
                console.error('------------------ error while updating the box money ------------------')
                console.error('')
                console.error(err);
                alert("Failure Calling The Web Service. Please try again later.");
            }
        });
    }

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

    $('#purchase-button').click(function () {
        makePurchase();
    });

    $('#return-money-button').click(function () {
        $.ajax({
            type: 'POST',
            url: `${BOX_URI}/command/return-money`,
            success: function (moneyData) {
                console.log('')
                console.log('------------------ log while returning the money ------------------')
                console.log('')
                console.log(moneyData);
                console.log('')
                console.log('------------------ log while returning the money ------------------')
                console.log('')
                console.log('');
                updateMoneyState(moneyData)
                messageBox("The money was returned");
            },
            error: function (err) {
                console.error('')
                console.error('------------------ error while returning the money ------------------')
                console.error('')
                console.error(err);
                console.error('')
                console.error('------------------ error while returning the money ------------------')
                console.error('')
                console.error(err);
                alert("Failure Calling The Web Service. Please try again later.");
            }
        });

    });
})

function messageBox(message) {
    $('#vending-message').val(message);
}

function updateMoneyState(money) {
    $('#money-input').empty();
    $('#money-input').val(money.moneyInTransaction);
    $('#total-money-in').empty();
    $('#total-money-in').val(money.moneyInside.amount);
    $("#qt_1c").text(money.moneyInside.oneCentCount);
    $("#qt_10c").text(money.moneyInside.tenCentCount);
    $("#qt_25c").text(money.moneyInside.quarterCount);
    $("#qt_1d").text(money.moneyInside.oneDollarCount);
    $("#qt_5d").text(money.moneyInside.fiveDollarCount);
    $("#qt_20d").text(money.moneyInside.twentyDollarCount);
}

function updateSnacks(snackPiles){
    for (var pile of snackPiles){
        $('#'+pile.snack.name+"_qtleft").text(`left: ${pile.quantity}`);
    }

}

function makePurchase() {

    $.ajax({
        type: 'POST',
        url: `${BOX_URI}/command/buy-snack/${$("#selectedItem").text()}`,
        success: function (data) {
            messageBox(data.Message);
            updateMoneyState(data);
            updateSnacks(data.snackPiles);
        },
        error: function (error) {
            var errorMessage = error.responseJSON.message;
            alert(errorMessage);
            messageBox(errorMessage);
        }
    });
}