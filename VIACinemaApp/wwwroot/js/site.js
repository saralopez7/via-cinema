var dates = new Array();

openDate();

// Open selected date window
function openDate() {
    getDates();

    var i = 0;

    //Create buttons
    dates.forEach(date => {
        var button = document.createElement("button");
        button.setAttribute("class", "tablinks");
        button.onclick = function () {
            var tabcontent = Array.prototype.slice.call(document.getElementsByClassName('tabcontent'));
            var links = Array.prototype.slice.call(document.getElementsByClassName("tablinks"));
            // display only one tab content at a time and change className to change focus on selected item
            tabcontent.forEach(tab => { // display only one date content at a time
                links.forEach(link => {
                    if (link.innerHTML === tab.id) link.className = "tablinks";
                });
                tab.style.display = "none";
            });
            sendDataToController(date);

            button.className += " active";
            createTabContents(date);
            document.getElementById(formatDate(date)).style.display = "block";
        };

        button.innerHTML = formatDate(date);

        if (i === 0) // set default open to the first button
            button.setAttribute("id", "defaultOpen");
        i += 1;
        document.getElementById("tab").appendChild(button);
    });
}

// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOpen").click();

function createTabContents(date) {
    var dateFormatted = formatDate(date);
    var division = document.createElement("div");
    division.setAttribute("class", "tabcontent");
    division.setAttribute("id", dateFormatted);
    document.getElementById("tabContainer").appendChild(division);
}

function getDates() {
    var startDate = new Date();

    for (var i = 0; i <= 7; i++) {
        var currentDate = new Date();
        currentDate.setDate(startDate.getDate() + i);
        dates.push(currentDate);
    }

    return dates;
}

function formatDate(date) {
    var monthNames = [
        "January", "February", "March", "April", "May", "June", "July",
        "August", "September", "October", "November", "December"];

    var weekdays = ["Sunday", "Monday", "Tuesday",
        "Wednesday", "Thursday", "Friday", "Saturday"];

    return weekdays[date.getDay()] + " " + date.getDate() + " " + monthNames[date.getMonth()];
}

function sendDataToController(date) {
    $.ajax({
        type: "post",
        url: "/AvailableMovies/GetMovies",
        datatype: "json",
        data: { id: formatDate(date) },
        error: function (result) {
            alert("Something went wrong: " + result.statusText);
        },
        success: function (response) {
            document.getElementById(formatDate(date)).innerHTML = response;
        }
    });
}

function getMovie() {
    var date = document.getElementById("date").value;
    $.ajax({
        type: "post",
        url: "/AvailableMovies/GetMovies",
        datatype: "json",
        data: { id: date },
        error: function (result) {
            alert("Error: " + result.statusText);
        },
        success: function (response) {
            document.getElementById("movies").innerHTML = response;
        }
    });
}