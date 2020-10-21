$(document).ready(function () {
    $(function () {

        $("#rateYo").rateYo({
            starWidth: "50px",
            rating: 3,
            ratedFill: "#E74C3C",
            fullStar: true,
            maxValue: 5,
            onSet: function (rating, rateYoInstance) {
                $('.rating-value').val(rating)
            }
        });

    });
})