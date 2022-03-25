$(document).ready(function () {

    $("#myDetailContent").load("/serviceprovider/SPDetail", function () {
        myDetailsEvents();
    });

    
});


function myDetailsEvents() {
    $(".avtar-list").each(function () {
        if ($(this).attr("src") == $("#mainProfileImg").attr("src")) {
            $(this).addClass("active-avtar");
            $("#mainProfileImg").removeClass("active-avtar");
        }
    });

    $(".avtar-list").click(function () {
        $(".avtar-list").removeClass("active-avtar");
        $(this).addClass("active-avtar");
        $("#mainProfileImg").attr("src", $(this).attr("src"));
        $("#mainProfileImgValue").val($(this).attr("src"));
    });
}

function MyAccountPrograssbar(divMain, text) {


    if (divMain == "#MyDetailTab") {
        $(divMain).addClass("active-tab")
        $(text).addClass("service-text-active")
        $("#myAddressTab").removeClass("active-tab")
        $("#changePpassTab").removeClass("active-tab")
        $("#myAddressTextId").removeClass("service-text-active")
        $("#changePassTextId").removeClass("service-text-active")
        $("#myAddressContent").removeClass("active-content")
        $("#changePassContent").removeClass("active-content")
        $("#myDetailContent").addClass("active-content")


    }
    else if (divMain == "#myAddressTab") {
        $(divMain).addClass("active-tab")
        $(text).addClass("service-text-active")
        $("#MyDetailTab").removeClass("active-tab")
        $("#changePpassTab").removeClass("active-tab")
        $("#myDatailTextId").removeClass("service-text-active")
        $("#changePassTextId").removeClass("service-text-active")
        $("#myDetailContent").removeClass("active-content")
        $("#changePassContent").removeClass("active-content")
        $("#myAddressContent").addClass("active-content")


    }
    else {
        $(divMain).addClass("active-tab");
        $(text).addClass("service-text-active")
        $("#MyDetailTab").removeClass("active-tab")
        $("#myAddressTab").removeClass("active-tab")
        $("#myAddressTextId").removeClass("service-text-active")
        $("#myDatailTextId").removeClass("service-text-active")
        $("#myAddressContent").removeClass("active-content")
        $("#myDetailContent").removeClass("active-content")
        $("#changePassContent").addClass("active-content")

    }

}


    

    


