$(document).ready(function () {
    

    $("#NewServiceId").addClass("active");

    $.ajax({
        url: "/ServiceProvider/NewServiceRequestTable",
        type: "GET",
        
        success: function (result) {
            $(".tableDiv").html(result);
            

        },
        error: function () {
            alert("error");
        },
    });
   
        
    
    


});
var check;

function petBoxfun(cid) {
   
    if ($(cid).prop('checked') == true) {
        check = true;
    }
    else {
        check =false;
    }
        $.ajax({
            url: "/ServiceProvider/NewServiceRequestTable",
            type: "GET",
            data: {
                checkVal: check ,
            },
            success: function (result) {
                $(".tableDiv").html(result);
                
            },
            error: function () {
                alert("error");
            },
        });
    
    clickEvents();
    
    
}

function clickEvents() {
    $(".Accept-Btn").click(function () {
        AcceptSerFun($(this).attr("data-id"));
    });
}

function petHasNOT() {
    var check;
    if ($("#petBoxId").hasClass("active-checkbox")) {
        check = true;
        $("#petBoxId").removeClass("active-checkbox");
    }
    else {
        check = false;
        $("#petBoxId").addClass("active-checkbox");
    }

    $.ajax({
        url: "/ServiceProvider/NewServiceRequests2",
        type: "GET",
        data: {
            checkVal: check,
        },
        success: function (result) {
            $("#upcomingHistoryTable").html(result);
            
        },
        error: function () {
            alert("error");
        },
    });
}

function ServiceDetailFun(id) {
    $.ajax({
        url: "/ServiceProvider/ServiceRequestDetail",
        type: "GET",
        data: {
            Id: id,
        },
        success: function (result) {
            $("#serviceProviderModel").html(result);
            $("#serviceProviderModel").modal("show");
           
        },
        error: function () {
            alert("error");
        },
    });
}

function AcceptSerFun(id) {
    $.ajax({
        url: "/ServiceProvider/AcceptBtnFun",
        type: "GET",
        data: {
            Id: id,
        },
        success: function (result) {
            popUpFun(result);
            //$("#serviceProviderModel").html(result);
            //$("#serviceProviderModel").modal("show");

        },
        error: function () {
            alert("error");
        },
    });
}


function popUpFun(result) {
    $.ajax({
        url: "/ServiceProvider/PopUpFun",
        type: "GET",
        data: {
            Message: result.message,
            ImgSrc: result.imgSrc,
        },
        success: function (result) {
            $("#serviceProviderModel").html(result);
            $("#serviceProviderModel").modal("show");
            /*location.reload();*/
            $(".AcceptBtnClose").click(function () {
                location.reload();
            });

        },
        error: function () {
            alert("error");
        },
    });
}