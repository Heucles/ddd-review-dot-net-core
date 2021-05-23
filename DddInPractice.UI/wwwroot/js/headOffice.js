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



    $('#unload-cash-snack-machine').click((event) => {

        // TODO: IMPLEMENT SELECTION ON TABLE
        const snackMachineId = parseInt($('#snackMachines tr td')[0].innerText);

        const snackMachineVal = $('#snackMachines tr td')[1];

        $.ajax({
            type: 'POST',
            // TODO: IMPLEMENT SELECTION ON TABLE
            url: `${BOX_URI}/command-head-office/take-money-from-snack-machine/${snackMachineId}`,
            success: function (data) {
                $(snackMachineVal).text(data.moneyInsideSnackMachine);
                $('#balance-lbl').text(data.balance);
                $('#cash-lbl').text(data.cash);
            },
            error: function (error) {
                var errorMessage = error.responseJSON.message;
                alert(errorMessage);
                alert(errorMessage);
            }
        });

    });

    $('#load-cash-atm').click((event) => {

        // TODO: IMPLEMENT SELECTION ON TABLE
        const atmId = parseInt($('#atms tr td')[0].innerText);

        const atmVal = $('#atms tr td')[1];

        $.ajax({
            type: 'POST',
            // TODO: IMPLEMENT SELECTION ON TABLE
            url: `${BOX_URI}/command-head-office/send-money-into-atm/${atmId}`,
            success: function (data) {
                $(atmVal).text(data.moneyInsideAtm);
                $('#balance-lbl').text(data.balance);
                $('#cash-lbl').text(data.cash);
            },
            error: function (error) {
                var errorMessage = error.responseJSON.message;
                alert(errorMessage);
                alert(errorMessage);
            }
        });

    });    
});