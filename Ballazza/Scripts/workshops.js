$(document).ready(function () {
    $('#workshop-table').DataTable({
        "ajax": {
            "url": "Workshops/GetWorkshopList",
            "type": "GET",
            "datatype": "JSON",
        },
        "columns": [
            /*{ "data": "Id" ,"autowidth": true},*/
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
            { "data": "Quota", "autowidth": true },
            {
                "render": function (data, type, row) {
                    if (row.Id === 9999) {
                        return `<p class="booked" style="color: #1c8013; text-align: center !important; text-transform: uppercase; font-weight: 700;">Booked!</p>`
                    }
                    else if (row.Id === 999999) {
                        return `<a class="book-button" style="display:block; border-radius: 20px;min-width: 5vw !important; text-align: center; border:none; padding-top: .5vh; background-color: #b3ad15 !important; opacity: .5; color: #fff;min-height: 3vh !important;text-transform: uppercase;">Clashed</a>`
                    }
                    return `<a class="book-button" href="/Bookings/GetWorkshopDetails/?WorkshopId=${row.Id}" style="display:block;  text-align: center; padding-top: .5vh; border-radius: 20px;min-width: 5vw !important;border: none;background-color: #b53636 !important;color: #fff;min-height: 3vh !important;text-transform: uppercase;">Book</a>`
                },
                "autowidth": true
            }
        ]
    })
});




