$(document).ready(function () {
    $('#venue-table').DataTable({
        "ajax": {
            "url": "GetVenuesListAdmin",//should be changed later on
            "type": "GET",
            "datatype": "JSON",
        },
        "columns": [
            { "data": "VenueId", "autowidth": true },
            { "data": "VenueName", "autowidth": true },
            { "data": "VenueStreet", "autowidth": true },
            { "data": "VenueSuburb", "autowidth": true },
            { "data": "VenueState", "autowidth": true },
            { "data": "VenuePostcode", "autowidth": true },
            { "data": "VenuePhoneno", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return (`
                            <div style="display:flex; justify-content:space-evenly;">
                               <a class="edit-button" href="/Venues/Edit/${row.VenueId}" style="display: block; border-radius: 10px; background-color: #524A4A;color: white;padding-bottom:1vh; padding-top: 1vh; min-width: 60%; text-align: center; text-decoration: none; text-transform: uppercase;">Edit</a>
                               <a class="delete-button" href="/Venues/Delete/${row.VenueId}" style="display: block; border-radius: 10px; background-color: #C12626;color: white;padding-bottom:1vh; padding-top: 1vh;  min-width: 60%; text-align: center; text-decoration: none; text-transform: uppercase;">Delete</a>
                            </div>
                           `)
                },
                "autowidth": true
            }
        ]
    })
});

$(".create-button").click(function () {
    window.location.href = "/Venues/Create";
});
