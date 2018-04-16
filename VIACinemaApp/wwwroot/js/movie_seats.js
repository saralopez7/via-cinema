var settings = {
    bookedSeatCss: 'bookedSeat',
    selectingSeatCss: 'selectingSeat'
};

function setClick(className) {
    var btns = document.getElementsByClassName(className);
    if (btns[0].classList.contains(settings.bookedSeatCss)) {
        alert('This seat is already reserved');
    } else if (btns[0].classList.contains(settings.selectingSeatCss)) {
        return;
    }
    else {
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

function showNewSeats() {
    var str = [], item;
    $.each($('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
        item = $(this).attr('title');
        str.push(item);
    });
    alert(str.join(','));
}