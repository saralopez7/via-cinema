var dates = new Array();

createNextSevenDates();

/**
 * Create 7 tabs with the next seven dates to get the available movies for the next 7 days.
 * Set on click event for each of the tabs.
 * Get available movies for each of the tabs depending on the tab date.
 */
function createNextSevenDates() {
    getNextSevenDates();

    var i = 0; // used to check for default button.

    //Create date tabs.
    dates.forEach(date => {
        var button = document.createElement("button");
        button.setAttribute("class", "tablinks");

        button.onclick = function () {
            var tabcontent = Array.prototype.slice.call(document.getElementsByClassName("tabcontent"));
            var links = Array.prototype.slice.call(document.getElementsByClassName("tablinks"));
            // display only one tab content at a time and change className to change focus on selected item.
            tabcontent.forEach(tab => { // display only one date content at a time.
                links.forEach(link => {
                    if (link.innerHTML === tab.id) link.className = "tablinks";
                });
                tab.style.display = "none";
            });

            // Get available movies for the given tab date.
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

/**
 * Create tab content to place the available movies for the given date.
 * @param {date} date
 */
function createTabContents(date) {
    var dateFormatted = formatDate(date);
    var division = document.createElement("div");
    division.setAttribute("class", "tabcontent");
    division.setAttribute("id", dateFormatted);
    document.getElementById("tabContainer").appendChild(division);
}

/**
 * Get next seven dates starting from current date.
 * Used to display available movies for the next 7 days.
 */
function getNextSevenDates() {
    var startDate = new Date();

    for (var i = 0; i <= 7; i++) {
        var currentDate = new Date();
        currentDate.setDate(startDate.getDate() + i);
        dates.push(currentDate);
    }

    return dates;
}

/**
 * Format tab date in the format: Weekday Day Month (Tuesday 12 April)
 * @param {date} date
 */
function formatDate(date) {
    var monthNames = [
        "January", "February", "March", "April", "May", "June", "July",
        "August", "September", "October", "November", "December"];

    var weekdays = ["Sunday", "Monday", "Tuesday",
        "Wednesday", "Thursday", "Friday", "Saturday"];

    return weekdays[date.getDay()] + " " + date.getDate() + " " + monthNames[date.getMonth()];
}

/**
 * Get available movies in a given date.
 * Make POST request to the GetMovies action method from the AvailableMovies controller
 * passing the parameter date as the request parameter.
 * @param {date} date
 */
function sendDataToController(date) {
    var loadingImage = document.getElementById("loading-image");
    displayLoadingImage(loadingImage);

    $.ajax({
        type: "post",
        url: "/AvailableMovies/GetMovies",
        datatype: "json",
        data: { id: formatDate(date) },
        error: function (result) {
            hideLoadingImage(loadingImage);
            alert("Something went wrong: " + result.statusText);
        },
        success: function (response) {
            hideLoadingImage(loadingImage);
            // set the html of the date tab to the response returned by the controller
            document.getElementById(formatDate(date)).innerHTML = response;
        }
    });
}

/**
 * Helper method used to hide loading Image when content has been returned or an error has ocurred.
 * @param {string} loadingImage
 */
function hideLoadingImage(loadingImage) {
    loadingImage.style.display = "none";
    loadingImage.style.marginBottom = "0";
    loadingImage.style.marginTop = "0";
}

/**
 * Helper method used to diaplay loading Image before ajax POST request until ajax request has completed.
 * @param { string } loadingImage
 */
function displayLoadingImage(loadingImage) {
    loadingImage.style.display = "block";
    loadingImage.style.marginBottom = "5%";
    loadingImage.style.marginTop = "5%";
}

/**
 * Get movie by date selected in the input element.
 * Allows the user to query for available movies at any date.
 * Make POST request to the GetMovies action method from the AvailableMovies controller
 * passing the parameter date as the request parameter.
 */
function getMovieByDate() {
    var date = document.getElementById("date").value;
    var loadingImage = document.getElementById("loading-image");
    loadingImage.style.display = "block";
    displayLoadingImage(loadingImage);

    $.ajax({
        type: "post",
        url: "/AvailableMovies/GetMovies",
        datatype: "json",
        data: { id: date },
        error: function (result) {
            hideLoadingImage(loadingImage);
            alert("Error: " + result.statusText);
        },
        success: function (response) {
            hideLoadingImage(loadingImage);
            // set the html of the movies div to the response returned by the controller
            document.getElementById("movies").innerHTML = response;
        }
    });
}