$(document).ready(function () {
    $(window).scroll(function () {
        var sticky = $("#header1"),
            scroll = $(window).scrollTop();

        if (scroll > 100) {
            sticky.addClass("fixed");
            $(".navbar-brand img").height(54).width(73);
            $(".navbar").css("background", "rgba(82, 82, 82, 0.8)");
            $(".bl").css("background", "#29626d");



        }
        else {
            sticky.removeClass("fixed");
            $(".navbar-brand img").height(130).width(175);
            $(".navbar").css("background", "transparent");
            $(".bl").css("background", "transparent");

        }
    });
});