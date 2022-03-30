$(document).ready(function () {
    //$("#ServiceSchedualVerId").css("background-color","red");
    var events = [];
    $.ajax({
        type: "GET",
        url: "/serviceProvider/ServiceSchedualCalander",
        success: function (data) {
            $.each(data.data, function (i, v) {
                events.push({
                    title: v.title,
                    start: v.start,
                    end: v.End != null ? v.End : null,
                    color: v.color,
                    id: v.id,
                    allDay: true,
                });
            })

            GenerateCalender(events);
        },
        error: function (error) {
            alert('failed');
        }
    });

});

    function GenerateCalender(events) {
        $("#calender").fullCalendar("destroy");
        $("#calender").fullCalendar({
            defaultDate: new Date(),
            timeFormat: "h(:mm)a",
            header: {
                left: "prev,next title",
                right: "my text",
            },
            eventLimit: true,
            eventColor: "#378006",
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                loadSPModal(calEvent.id);
            },
        });
    }
function loadSPModal(id) {
    $(".fc-content").addClass("Active-ServiceDetail-Dilog");
    $.ajax({
        url: "/serviceprovider/servicerequestdetail",
        type: "GET",
        data: {
            id: id,
            UPService:true,
        },
        success: function (result) {
            $("#spModal").html(result);
            $("#spModal").modal("show");
            
        },
        error: function () {
            alert("error");
        },
    });
}



function CancleClickFun(id) {
    $.ajax({
        url: "/ServiceProvider/CancleServiceSP",
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


function popUpFun(result) {
    $.ajax({
        url: "/ServiceProvider/PopUpFun",
        type: "GET",
        data: {
            Message: result.message,
            ImgSrc: result.imgSrc,
        },
        success: function (result) {

            if ($(".fc-content").hasClass("Active-ServiceDetail-Dilog")) {
                $('#spModal').modal('toggle');
                $(".fc-content").removeClass("Active-ServiceDetail-Dilog");
            }

            $("#serviceProviderModelPopUp").html(result);
            $("#serviceProviderModelPopUp").modal("show");

            $(".SPDetailModalCloseId").click(function () {
                location.reload();
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