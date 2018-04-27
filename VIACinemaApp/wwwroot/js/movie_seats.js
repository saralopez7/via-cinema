var settings = {
    bookedSeatCss: "bookedSeat",
    selectingSeatCss: "selectingSeat"
}

function setClick(className) {
    var btns = document.getElementsByClassName(className);
    if (btns[0].classList.contains(settings.bookedSeatCss)) {
        alert("This seat is already reserved");
    } else if (btns[0].classList.contains(settings.selectingSeatCss)) {
        return;
    } else {
        btns[0].classList += " " + settings.selectingSeatCss;
    }
}

function showNewSeats() {
    var seats = [];

    $.each($("#place li." + settings.selectingSeatCss + " a"), function () {
        seats.push($(this).attr("title"));
    });
    var movieId = window.location.href.split("=")[1];

    var data = JSON.stringify({ "movieId": movieId, "seats": seats });
    document.cookie = "Seats " + movieId + "=" + seats;

    $.ajax({
        url: "/Transactions/RegisterSeats",
        type: "POST",
        data: { "seats": data },
        error: function (response) {
            if (response.status === 401) {
                window.location = "/Account/Login?returnurl=/Seats?id=" + movieId;
            }
        },
        success: function (response) {
            window.location = "/Transactions/Details?id=" + response.id;
        }
    });
}