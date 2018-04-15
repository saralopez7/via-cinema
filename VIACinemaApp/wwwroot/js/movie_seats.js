var settings = {
    rows: 5,
    cols: 15,
    rowCssPrefix: 'row-',
    colCssPrefix: 'col-',
    seatWidth: 35,
    seatHeight: 35,
    seatCss: 'seat',
    bookedSeatCss: 'bookedSeat',
    selectingSeatCss: 'selectingSeat'
};
sendSeatsToController();

init();
function init() {
    var reservedSeats = [5,12];


    var str = [], seatNo, className;
    for (i = 0; i < settings.rows; i++) {
        for (j = 0; j < settings.cols; j++) {
            seatNo = (i + j * settings.rows + 1);
            className = settings.seatCss + ' ' + settings.rowCssPrefix + i.toString() + ' ' + settings.colCssPrefix + j.toString();
            if (reservedSeats.includes(seatNo)) {
                className += ' ' + settings.bookedSeatCss;
            }
            str.push('<li class="' + className + '"' +
                'style="top:' + (i * settings.seatHeight).toString() + 'px;left:' + (j * settings.seatWidth).toString() + 'px" onClick="setClick(\'' + className + '\')" />' +
                '<a title="' + seatNo + '">' + seatNo + '</a>' +
                '</li>');
        }
    }
    document.getElementById("place").innerHTML = str.join('');
};

function setClick(className) {
    if (className.includes(settings.bookedSeatCss)) {
        alert('This seat is already reserved');
    }
    else {
        var btns = document.getElementsByClassName(className);
        btns[0].classList += ' ' + settings.selectingSeatCss;
    }
}

function showSeats() {
    var str = [];
    $.each($('#place li.' + settings.bookedSeatCss + ' a, #place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
        str.push($(this).attr('title'));
    });
    alert(str.join(','));
}

function showNewSeats () {
    var str = [], item;
    $.each($('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
        item = $(this).attr('title');
        str.push(item);
    });
    alert(str.join(','));
}

function sendSeatsToController() {

    $.ajax({
        type: 'post',
        url: '/Seats/GetSeats',
        datatype: "json",
        error: function (result) {
            alert('Something went wrong: ' + result.statusText);
        },
        success: function (response) {
            receiveAjaxResponse(response);
        }
    });
}

function receiveAjaxResponse(response) {
    alert(response);
}
