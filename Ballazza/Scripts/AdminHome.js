const chartLabels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'] 
const months = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
const chartYear = document.querySelector("#chart-year")
let datasets = []
let chart = null
const ctx = document.getElementById('bar-chart').getContext('2d');

document.querySelector(".jumbotron-subs").addEventListener("click", () => {
    window.location.href = "Chat";
})

chartYear.addEventListener('change', () => {
    chart.destroy()
    const yearValue = chartYear.value;
    $.get(`../Bookings/GetMonthlyBookingReportByYear/?year=${yearValue}`, function (data) {
        // Map data to months, if there are no records then return 0, otherwise return the related number of data
        availableMonths = data.data.map(row => row.Month);
        maxValue = Math.max(...data.data.map(row => row.NumberOfBookings))
        datasets = months.map(month => {
            const indexOfFilledData = availableMonths.indexOf(month);
            if (indexOfFilledData != -1) {
                return data.data[indexOfFilledData].NumberOfBookings
            }
            else {
                return 0
            };
        })
        //console.log(datasets)
        chart = new Chart(ctx, {
            // The type of chart we want to create
            type: 'bar',
            // The data for our dataset
            data: {
                labels: chartLabels,
                datasets: [{
                    label: 'Number of Bookings',
                    backgroundColor: '#c41f10',
                    borderColor: '#8c150d',
                    data: datasets
                }]
            },

            // Configuration options go here
            options: {
                scales: {
                    xAxes: [{
                        gridLines: {
                            color: "rgba(0, 0, 0, 0)",
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            suggestedMin: 0,
                            suggestedMax: maxValue
                        }
                    }]

                }
            }
        });
    })
})

$(document).ready(function () {
    $.get("../Bookings/GetMonthlyBookingReport", function (data) {
        // Map data to months, if there are no records then return 0, otherwise return the related number of data
        availableMonths = data.data.map(row => row.Month);
        maxValue = Math.max(...data.data.map(row => row.NumberOfBookings))
        datasets = months.map(month => {
            const indexOfFilledData = availableMonths.indexOf(month);
            if (indexOfFilledData != -1) {
                return data.data[indexOfFilledData].NumberOfBookings
            }
            else {
                return 0
            };
        })
        //console.log(datasets)
        chart = new Chart(ctx, {
            // The type of chart we want to create
            type: 'bar',
            // The data for our dataset
            data: {
                labels: chartLabels,
                datasets: [{
                    label: 'Number of Bookings',
                    backgroundColor: '#c41f10',
                    borderColor: '#8c150d',
                    data: datasets
                }]
            },

            // Configuration options go here
            options: {
                title: {
                    display: true,
                    text: 'Total bookings per month',
                    fontSize: 40
                },

                scales: {
                    xAxes: [{
                        gridLines: {
                            color: "rgba(0, 0, 0, 0)",
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            suggestedMin: 0,
                            suggestedMax: maxValue
                        }
                    }]
                    
                }
             }
        });
    })
});


