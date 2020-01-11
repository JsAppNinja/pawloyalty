// returns title of room types, room statuses and booking statuses
// collections were predefined on view:
//  var roomTypesCollection
//  var roomStatusesCollection
//  var bookingStatusesCollection
function pre_init() {
    function titleById(collection, id) {
        for (var i = 0; i < collection.length; i++) {
            if (collection[i].Id == id) {
                return collection[i].Title;
            }
        }

        return "";
    }

    function formatDateForEventBox(date) {
        var formatFunc = scheduler.date.date_to_str("%d %M %Y");
        return formatFunc(date);
    }


    scheduler.templates.Timeline_scale_label = function (key, label, room) {

        var indicatorClass = "room_status_indicator_" + room.status;

        var template = "<div class='timeline_item_separator'></div>" +
            "<div class='timeline_item_cell'>" + room.room_number + "</div>" +
            "<div class='timeline_item_separator'></div>" +
            "<div class='timeline_item_cell'>" + titleById(roomTypesCollection, room.type) + "</div>" +
            "<div class='timeline_item_separator'></div>" +
            "<div class='timeline_item_cell room_status'>" + "<span class='room_status_indicator " + indicatorClass + "'></span>" + "<span>" + titleById(roomStatusesCollection, room.status) + "</span>" + "</div>";

        return template;
    }


    scheduler.date.Timeline_start = scheduler.date.month_start;
    scheduler.date.add_Timeline = function (date, step) {
        return scheduler.date.add(date, step, "month");
    };

    //scheduler.attachEvent("onBeforeViewChange", function (old_mode, old_date, mode, date) {
    //    var year = date.getFullYear();
    //    var month = (date.getMonth() + 1);
    //    var d = new Date(year, month, 0);
    //    var daysInMonth = d.getDate();
    //    scheduler.matrix["Timeline"].x_size = daysInMonth;
    //    return true;
    //});

    scheduler.templates.event_class = function (start, end, event) {
        return "event_" + (event.status || "");
    }

    scheduler.templates.event_bar_text = function (start, end, event) {
        var paidStatus = event.is_paid == true ? "paid" : "not paid";
        var startDate = formatDateForEventBox(event.start_date);
        var endDate = formatDateForEventBox(event.end_date);
        var dates = startDate + " - " + endDate;
        var statusDiv = "<div class='booking_status booking-option'>" + (titleById(bookingStatusesCollection, event.status) || "") + "</div>";
        var paidDiv = "<div class='booking_paid booking-option'>" + paidStatus + "</div>";

        var output = event.text + "<br />" + dates + statusDiv + paidDiv;
        return output;
    };

    //scheduler.attachEvent("onEventCollision", function (ev, evs) {

    //    for (var i = 0; i < evs.length; i++) {

    //        if (ev.room_number != evs[i].room_number) continue;

    //        dhtmlx.message({
    //            type: "error",
    //            text: "This room is already booked for this date."
    //        });
    //    }

    //    return true;
    //});

    var element = document.getElementById("scheduler_here");
    var top = scheduler.xy.nav_height + 1 + 1; // first +1 -- blank space upper border, second +1 -- hardcoded border length
    var height = scheduler.xy.scale_height * 2;
    var width = scheduler.matrix.Timeline.dx;
    var descriptionHTML = "<div class='timeline_item_separator'></div>" +
        "<div class='timeline_item_cell'>Number</div>" +
        "<div class='timeline_item_separator'></div>" +
        "<div class='timeline_item_cell'>Type</div>" +
        "<div class='timeline_item_separator'></div>" +
        "<div class='timeline_item_cell room_status'>Status</div>";
    descriptionHTML = "<div style='position: absolute; top: " + top + "px; width: " + width + "px; height: " + height + "px;'>" + descriptionHTML + "</div><div style='clear: both;'></div>";
    element.innerHTML += descriptionHTML;

    scheduler.roomsBackup = [];
    var rooms = scheduler.serverList("Rooms");
    for (var i = 0; i < rooms.length; i++) {
        scheduler.roomsBackup.push(rooms[i]);
    }


}


//function post_init() {
//    scheduler.dataProcessor.attachEvent("onAfterUpdate", function (id, action, tid, response) {
//        if (action == "error") {

//            dhtmlx.message({
//                type: "error",
//                text: response.textContent || response.nodeValue
//            });

//            scheduler.clearAll();
//            scheduler.load(dataActionUrl, "json"); // dataActuionUrl receiving from ViewBag.DataAction in the beginning of the view
//        }
//    });

//    var roomFilterSelect = document.getElementById("room-filter");
//    roomFilterSelect.onchange = function () {
//        setTimeout(function () {
//            filterRooms();
//        }, 0);
//    }

//}

function filterRooms() {
    var selectValue = document.getElementById("room-filter").value;

    if (selectValue == "all") {
        scheduler.updateCollection("Rooms", scheduler.roomsBackup);
        return;
    }

    var newServerListArray = [];
    var oldRooms = scheduler.roomsBackup;
    for (var i = 0; i < oldRooms.length; i++) {

        if (oldRooms[i].type == selectValue) {
            newServerListArray.push(oldRooms[i]);
        }
    }

    scheduler.updateCollection("Rooms", newServerListArray);
}


