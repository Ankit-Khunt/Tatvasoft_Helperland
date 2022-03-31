$(document).ready(function () {
    


    $("#UpcomindSPId").addClass("active");

    CallUPServiceTable();
   
        $('#upcomingHistoryTable').DataTable({
            "dom": '<"top"i>rt<"bottom"flp><"clear">',
            //"columnDefs": [
            //    { "orderable": false, "targets": 4 }
            //],
            'responsive': true,

            "bFilter": false, //hide Search bar
            "pagingType": "full_numbers",
            paging: true,
            "pagingType": "full_numbers",
            // bFilter: false,
            ordering: true,
            searching: false,
            info: true,

            language: {
                paginate: {
                    first: "<img src='/images/firstPage.png' alt='first' />",
                    previous: "<img src='/images/previous.png' alt='previous' />",
                    next: '<img src="/images/previous.png" alt="next" style="transform: rotate(180deg)" />',
                    last: "<img src='/images/firstPage.png' alt='first' style='transform: rotate(180deg)' />",
                },
            },
            "buttons": ["excel"],
            "columnDefs": [{ orderable: false, targets: 5 }],
            //"columnDefs": [
            //    { "orderable": false, "targets": 4 }
            //],
            "oLanguage": {
                "sInfo": "Total Records: _TOTAL_"
            },
            "dom": '<"top">rt<"bottom"lip><"clear">',
            responsive: true,
            "order": []
        });

    $("Cancle-btn").on("Click", function () {
        CancleClickFun($(this).attr("data-id"));
    });
    

});





function ServiceDetailFun(id) {
    $("#TableIDCol").addClass("Active-ServiceDetail-Dilog");
    $.ajax({
        url: "/ServiceProvider/ServiceRequestDetail",
        type: "GET",
        data: {
            Id: id,
            UPService: true,
        },
        success: function (result) {
            $("#serviceProviderModel").html(result);
            $("#serviceProviderModel").modal("show");

        },
        error: function () {
            alert("error ServiceRequestDetail");
        },
    });
}


function CallUPServiceTable() {
    $.ajax({
        url: "/ServiceProvider/SPUpcomingServiceTable",
        type: "GET",
       
        success: function (result) {
            //$("#tableDivID").append(' <partial name="~/Views/ServiceProvider/SPUpcomingServiceTable.cshtml" />');
            /*$("#upcomingHistoryTable").html(result);*/
           

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}

function CancleClickFun(id) {
    $.ajax({
        url: "/ServiceProvider/CancleServiceSP",
        type: "GET",
        data: {
            Id :id,
        },

        success: function (result) {
            //$("#tableDivID").append(' <partial name="~/Views/ServiceProvider/SPUpcomingServiceTable.cshtml" />');
            popUpFun(result);


        },
        error: function () {
            alert("error SPServiceHistoryStaus");
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

            if ($("#TableIDCol").hasClass("Active-ServiceDetail-Dilog")) {
                $('#serviceProviderModel').modal('toggle');
                $("#TableIDCol").removeClass("Active-ServiceDetail-Dilog");
            }
            
            $("#serviceProviderModelPopUp").html(result);
            $("#serviceProviderModelPopUp").modal("show");
            
            $(".SPDetailModalCloseId").click(function () {
                CallUPServiceTable();
            });

            
        },
        error: function () {
            alert("error");
        },
    });
}

function CompleteClickFun(id) {
    $.ajax({
        url: "/ServiceProvider/CompletedServiceSP",
        type: "GET",
        data: {
            Id: id,
        },

        success: function (result) {
            //$("#tableDivID").append(' <partial name="~/Views/ServiceProvider/SPUpcomingServiceTable.cshtml" />');
            popUpFun(result);


        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}

