$(document).ready(function () {

    
        $.ajax({
            url: "/CustomerService/MyDetails",
            type: "GET",
            
            success: function (result) {
                $("#myDetailContent").html(result);
            },
            error: function () {
                alert("error");
            },
        });

    $.ajax({
        url: "/CustomerService/MyAddresses",
        type: "GET",

        success: function (result) {
            $("#myAddressContent").html(result);
            $("#myAddressContent").append('<partial name="MyAddresses.cshtml" />')
            myAddressEvents();
        },
        error: function () {
            alert("error");
        },
    });
    
});

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

function myAddressEvents() {
    $(".editBtn").click(function () {
        $.ajax({
            url: "/CustomerService/editaddress",
            type: "GET",
            data: {
                id: $(this).attr("data-id"),
            },
            success: function (result) {
                $("#customerModal").html(result);
                $("#customerModal").modal("show");
            },
            error: function () {
                alert("error");
            },
        });
    });

    $(".deleteBtn").click(function () {
        $.ajax({
            url: "/CustomerService/deleteaddress",
            type: "POST",
            data: {
                id: $(this).attr("data-id"),
            },
            success: function (result) {
                $("#myAddressContent").load("/customerservice/myaddresses", function () {
                    myAddressEvents();
                });
            },
            error: function () {
                alert("error");
            },
        });
    });

    $("#newAddressBtn").click(function () {
        $.ajax({
            url: "/CustomerService/editaddress",
            type: "GET",
            success: function (result) {
                $("#customerModal").html(result);
                $("#customerModal").modal("show");
            },
            error: function () {
                alert("error");
            },
        });
    });
}

function editCompleted() {
    $("#customerModal").modal("hide");
    $("#myAddressContent").load("/CustomerService/myaddresses", function () {
        myAddressEvents();
    });
}
