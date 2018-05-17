/**
 * Global settings used to check for selected and booked seats.
 */
var settings = {
    bookedSeatCss: "bookedSeat",
    selectingSeatCss: "selectingSeat"
};

/**
 * Change seat status whenever the user clicks on a seat if seat is not reserved.
 * @param {string} className
 */
function selectSeat(className) {
    var seats = document.getElementsByClassName(className);

    // If clicked seat is already reserved it cannot be selected.
    if (seats[0].classList.contains(settings.bookedSeatCss)) {
        alert("This seat is already reserved");
    } else if (seats[0].classList.contains(settings.selectingSeatCss)) {
        return;  // Clicked seat is already selected
    } else {
        seats[0].classList += " " + settings.selectingSeatCss;
    }
}

/**
 * Register selected seats for the given movie.
 */
function registerSeats() {
    var seats = [];

    $.each($("#place li." + settings.selectingSeatCss + " a"), function () {
        seats.push($(this).attr("title"));
    });
    var movieId = window.location.href.split("=")[1];

    var postRequestMessage = JSON.stringify({ "movieId": movieId, "seats": seats });
    // Store the post request message in a cookie in case of failure when making the POST request to register the seats
    document.cookie = "Seats " + movieId + "=" + seats;

    /* Make a POST request to the Register Seats action method of the Transaction
       controller with the movieId and the selected seats as the body of the POST request. */
    $.ajax({
        url: "/Transactions/RegisterSeats",
        type: "POST",
        data: { "seats": postRequestMessage },
        error: function (response) {
            // Redirect the user to the Login action method of the Account controller if user is not logged in.
            if (response.status === 401) {
                window.location = "/Account/Login?returnurl=/Seats?id=" + movieId;
            }
        },
        /* Redirect the user to the Details action method of the Transaction controller.
           with the transactionId as the paramenter*/
        success: function (response) {
            window.location = "/Transactions/Details?id=" + response.id;
        }
    });
}