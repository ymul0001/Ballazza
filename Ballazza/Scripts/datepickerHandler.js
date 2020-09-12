﻿$(document).ready(function () {
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
    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
    })
})
