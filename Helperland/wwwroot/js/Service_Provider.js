$(document).ready(function () {
    $(window).scroll(function () {
        var sticky = $("#header"),
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

    $(".dropdown-item").click(function () {
        var src = $(this).children().eq(0).attr('src');
        $('#flagImage').attr('src', src);
    });

    
    ValidationCheckBox();
});


function ValidationCheckBox() {
    
    
    
    $("#subNewSPBtn").css("background-color", "#6DA9B5");
    var fields = "#firstnameID, #LastNameID, #EmailID, #MobileID, #PasswordID, #ConfirmPasswordID, #check1, #check2";

    $(fields).on('change', function () {
        if (allFilled()) {
            $('#subNewSPBtn').removeAttr('disabled');
            $("#subNewSPBtn").css("background-color", "#1D7A8C");
        } else {
            $('#subNewSPBtn').attr('disabled', 'disabled');
            $("#subNewSPBtn").css("background-color", "#6DA9B5");
        }
    });

    function allFilled() {
        var filled = true;
        $(fields).each(function () {
            if ($(this).val() == '') {
                filled = false;
            }
            if (!$("#check1").is(':checked') || !$("#check2").is(':checked')) {
                filled = false;
            }
        });
        return filled;
    }
}