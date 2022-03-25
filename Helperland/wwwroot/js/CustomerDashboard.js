$(document).ready(function () {
    $('#upcomingHistoryTable').DataTable({
        "dom": '<"top"i>rt<"bottom"flp><"clear">',
        "columnDefs": [
            { "orderable": false, "targets": 4 }
        ],
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
        "columnDefs": [{ orderable: false, targets: 4 }],
        "columnDefs": [
            { "orderable": false, "targets": 4 }
        ],
        "oLanguage": {
            "sInfo": "Total Records: _TOTAL_"
        },
        "dom": '<"top">rt<"bottom"lip><"clear">',
        responsive: true,
        "order": []
    });


    $("#DashboardVerId").addClass("active");

    


});


function onRelod() {
    location.reload();
}
function ServiceDetailFun( serviceId) {
       
        $.ajax({
            url: '/CustomerService/ServiceDetailPartialView',
            type: 'GET',
            data: {
                id: serviceId,
            },
            success: function (result) {
                $("#customerModal").html(result);
                $("#customerModal").modal("show");
                //alert('Successfully ServiceDEtail received Data ');
               

            },
            error: function () {
                alert('Failed to receive the Data of serviceRquest');
                console.log('Failed ');
            }
        });
}

$(".reshadual-btn").click(function () {
   
    loadRescheduleDialog($(this).attr("id"));

});
$(".CancleBtnTable").click(function () {
    loadCancleDilog($(this).attr("id"));
});

function loadRescheduleDialog(serviceId) {
   
    $.ajax({
        url: "/CustomerService/RescheduleService",
        type: "GET",
        data: {
            id: serviceId,
        },
        success: function (result) {
            $("#customerModal").html(result);
            $("#customerModal").modal("show");
          
           
        },
        error: function () {
            alert("error");
        },
    });
}

function closeReschedual() {
    $("#reschedualCloseBtn").click(function () {
        onRelod();
    });
}

function loadCancleDilog(serviceId) {
    $.ajax({
        url: "/CustomerService/cancelservicerequest",
        type: "GET",
        data: {
            id: serviceId,
        },
        success: function (result) {
            $("#customerModal").html(result);
            $("#customerModal").modal("show");
            $("#cancelRequestBtn").attr("disabled", true);
            $("#cancelRequestBtn").css("background-color", "#6DA9B5");

            $(".cancel-request textarea").on("keyup", function () {
                var textarea_value = $(".cancel-request textarea").val();
                if (textarea_value != "") {
                    $("#cancelRequestBtn").attr("disabled", false);
                    $("#cancelRequestBtn").css("background-color", "#1D7A8C");
                } else {
                    $("#cancelRequestBtn").attr("disabled", true);
                }
            });
            $("#cancelRequestBtn").click(function () {
                cancelRequestPost(serviceId);
            });
        },
        error: function () {
            alert("error");
        },
    });
}

function cancelRequestPost(serviceId) {
    $.ajax({
        url: "/CustomerService/CancelServiceRequest",
        type: "POST",
        data: {
            id: serviceId,
            comment: $(".cancel-request textarea").val(),
        },
        success: function (result) {
            $("#customerModal").html(result);
            $("#customerModal").modal("show");
            onRelod();
        },
        error: function () {
            alert("error");
        },
    });
}