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
                    return `<button class="book-button" style="border-radius: 20px;min-width: 5vw !important;border: none;background-color: #b53636 !important;color: #fff;min-height: 3vh !important;">Book</button>`
                },
                "autowidth": true
            }
        ]
    })
});




