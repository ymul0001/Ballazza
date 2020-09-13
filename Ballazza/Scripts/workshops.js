$(document).ready(function () {
    $('#workshop-table').DataTable({
        "ajax": {
            "url": "Workshops/GetWorkshopList",
            "type": "GET",
            "datatype": "JSON",
        },
        "columns": [
            { "data": "Id" ,"autowidth": true},
            { "data": "Name", "autowidth": true },
            { "data": "AgeGroup", "autowidth": true },
            {
                "data": "StartDate",
                "render": function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var month = date.getMonth() + 1;
                    var day = date.getDay();
                    if (month < 10) {
                        if (day < 10) {
                            return "0" + day + "/" + "0" + month + "/" + date.getFullYear();
                        }
                        else {
                            return day + "/" + "0" + month + "/" + date.getFullYear();
                        }
                    }
                    if (day < 10) {
                        return "0" + day + "/" + month + "/" + date.getFullYear();
                    }
                    return day + "/" + month + "/" + date.getFullYear();
                },
                "autowidth": true
            },
            {
                "data": "EndDate",
                "render": function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var month = date.getMonth() + 1;
                    var day = date.getDay();
                    if (month < 10) {
                        if (day < 10) {
                            return "0" + day + "/" + "0" + month + "/" + date.getFullYear();
                        }
                        else {
                            return day + "/" + "0" + month + "/" + date.getFullYear();
                        }
                    }
                    if (day < 10) {
                        return "0" + day + "/" + month + "/" + date.getFullYear();
                    }
                    return day + "/" + month + "/" + date.getFullYear();
                },
                "autowidth": true
            },
            { "data": "Quota", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return `<button class="book-button" style="border-radius: 20px;min-width: 5vw !important;border: none;background-color: #b53636 !important;color: #fff;min-height: 3vh !important;text-transform: uppercase;">Book</button>`
                },
                "autowidth": true
            }
        ]
    })
});




