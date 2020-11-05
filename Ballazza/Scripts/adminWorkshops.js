$(document).ready(function () {
    $('#workshop-table').DataTable({
        "ajax": {
            "url": "GetWorkshopList",//should be changed later on
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
                    return (`
                            <div style="display:flex; justify-content:space-evenly;">
                               <a class="edit-button" href=/Workshops/Edit/${row.Id} style="display: block; border-radius: 10px; background-color: #524A4A;color: white;padding-bottom:1vh; padding-top: 1vh; min-width: 45%; text-align: center; text-decoration: none; text-transform: uppercase;">Edit</a>
                               <a class="delete-button" href=/Workshops/Delete/${row.Id} style="display: block; border-radius: 10px; background-color: #C12626;color: white;padding-bottom:1vh; padding-top: 1vh; min-width: 45%; text-align: center; text-decoration: none; text-transform: uppercase;">Delete</a>
                            </div>
                           `)
                },
                "autowidth": true
            }
        ]
    })
});

document.querySelector('.create-button').addEventListener('click', () => {
    window.location.href = "Create"
})



