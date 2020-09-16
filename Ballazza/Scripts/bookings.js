﻿$(document).ready(function () {
    $('#booking-table').DataTable({
        "ajax": {
            "url": "Bookings/GetBookingList",
            "type": "GET",
            "datatype": "JSON",
        },
        "columns": [
            { "data": "Id", "autowidth": true },
            { "data": "Name", "autowidth": true },
            { "data": "AgeGroup", "autowidth": true },
            {
                "data": "StartDate",
                "render": function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var month = date.getMonth() + 1;
                    if (month < 10) {
                        return date.getDate() + "/" + "0" + month + "/" + date.getFullYear();
                    }
                    return date.getDate() + "/" + month + "/" + date.getFullYear();
                },
                "autowidth": true
            },
            {
                "data": "EndDate",
                "render": function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var month = date.getMonth() + 1;
                    if (month < 10) {
                        return date.getDate() + "/" + "0" + month + "/" + date.getFullYear();
                    }
                    return date.getDate() + "/" + month + "/" + date.getFullYear();
                },
                "autowidth": true
            },
            {
                "render": function (data, type, row) {
                    return `<button class="book-button" style="border-radius: 20px;min-width: 5vw !important;border: none;background-color: #B86D00 !important;color: #fff;min-height: 3vh !important;">Cancel</button>`
                },
                "autowidth": true
            }
        ]
    })
});