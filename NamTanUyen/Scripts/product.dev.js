/// <reference path="jquery-2.0.0.intellisense.js" />
/// <reference path="jquery-2.0.0.min.js" />

$(document).ready(function () {
    $('.selectfilter').change(function () {
        if ($(this).val().indexOf("/") >= 0) {
            location.href = $(this).val();
        }
    })
    $(".btnfilter").click(function () {
        $('.nav-filter').slideToggle();
    })
});
