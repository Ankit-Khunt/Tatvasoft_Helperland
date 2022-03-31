$(document).ready(function () {
    ValidationCheckBox();
});

function ValidationCheckBox() {



    $("#SubNewUserBtnId").css("background-color", "#6DA9B5");
    var fields = "#firstnameId, #LastnameId, #EmailId, #MobileID, #PasswordId, #ConformPasswordId, #check1, #check2,#check3";

    $(fields).on('change', function () {
        if (allFilled()) {
            $('#SubNewUserBtnId').removeAttr('disabled');
            $("#SubNewUserBtnId").css("background-color", "#1D7A8C");
        } else {
            $('#SubNewUserBtnId').attr('disabled', 'disabled');
            $("#SubNewUserBtnId").css("background-color", "#6DA9B5");
        }
    });

    function allFilled() {
        var filled = true;
        $(fields).each(function () {
            if ($(this).val() == '') {
                filled = false;
            }
            if (!$("#check1").is(':checked') || !$("#check2").is(':checked') || !$("#check3").is(':checked')) {
                filled = false;
            }
        });
        return filled;
    }
}