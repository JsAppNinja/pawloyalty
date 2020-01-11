
// Constants
DAYS = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

MONTHS = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December"
];

DAY_COUNT_IN_MONTH = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

// Utils
Date.prototype.addDays = function (days) {
    var result = new Date(this.valueOf());
    result.setDate(result.getDate() + days);
    return result;
}

Date.prototype.firstDay = function () {
    var result = new Date(this.valueOf().getFullYear(), this.valueOf().getMonth(), 1);
    return result;
}

current_date = new Date();

function Calendar(start, days) {
    this.start = (isNaN(start) || start === null) ? current_date.getMonth() : start;
    this.days = (isNaN(days) || days === null) ? current_date.getFullYear() : days;

}

Calendar.prototype.generateHTML = function () {

    
    var monthHtml = '';
    var dayHtml = '';

    // Step 1. get start and end date
    let cal_start_date = new Date(this.start.getFullYear(), this.start.getMonth(), this.start.getDate());
    let cal_end_date = this.start.addDays(this.days);

    // Step 2. get month html
    for (let i = cal_start_date.getMonth(); i < cal_end_date.getMonth() + 1; i++) {
        let cal_month = 0;
        monthHtml += "<div class='rc-month'>" + MONTHS[i] + "</div>";
    }

    // Step 1. month row
    //for (let i = 0; i < this.days; i++) {

    //    // Step 1. current date
    //    let cal_month = 0;
    //    let cal_day = 0;
    //    let cal_date = this.start.addDays(i);

    //    // Step 2. current month
    //    if (cal_month !== cal_date.getMonth()) {

    //        // Step 1. end month
    //        if (cal_month !== 0)
    //        {
    //            html += '</div>'
    //        }

    //        // Step 2. set current month
    //        cal_month = cal_date.getMonth();

    //        // Step 3. start month
    //        html += '<div>' + MONTHS[cal_month]
    //        // Step 2. start month
    //        if(cal_month !== )
    //        html += '<div>' + MONTHS[cal_month] + '</div>';
    //        html += '<div>' + cal_month + '</div>';
    //    }
        
    //    html += '<div>' + cal_date + '</div>'
    //}

    $('#cal').html(monthHtml);
}

Calendar.prototype.drawCalendar = function () {

    this.generateHTML();
}


