$(document).ready(function () {
    jQuery.validator.methods.date = function (value, element) {
        var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
        if (isChrome) {
            var d = new Date();
            return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
        } else {
            return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
        }
    };
});

$(function () {
    $(".datepicker-1").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
    }).on('changeDate', function (selected) {
        ms = selected.date.valueOf() + 7 * 24 * 60 * 60 * 1000;
        var minDate = new Date(ms);
        $('.datepicker-2').datepicker('setStartDate', minDate);
        $('.datepicker-2').datepicker('setEndDate', minDate);
    });

})

$(function () {
    $(".datepicker-2").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
    })
})



