$(document).ready(function () {
    callSPMyRatingTable();
   


    $("#SPMyRatingVBID").addClass("active");

    

    $("select.StausOfTableClass").change(function () {
        var selectedOP = $(this).children("option:selected").val();
        alert("You have selected the country - " + selectedOP);
        callSPMyRatingTable(selectedOP);
    });
    //if (status >= 0 && status != 10) {

    //    $.ajax({
    //        url: "/ServiceProvider/SPServiceHistoryStaus",
    //        type: "GET",
    //        data: {
    //            statusData: status,
    //        },
    //        success: function (result) {
    //            $("#upcomingHistoryTable").html(result);

    //        },
    //        error: function () {
    //            alert("error");
    //        },
    //    });
    //}

});





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
            alert("error ServiceRequestDetail");
        },
    });
}


function callSPMyRatingTable() {
    $.ajax({
        url: '/ServiceProvider/SPMyRatingTable',
        dataType: "html",
        method: "GET",
        
        success: function (result) {
            $(".tableDiv").html(result);
           
        },
        error: function () {
            alert("error callSPMyRatingTable");
        },
    });
}



function sorting() {

    if ($('#sortingBtnID').hasClass("dsc")) {
        $('#upcomingHistoryTable').DataTable().order([0, 'desc']).draw();
        $('#sortingBtnID').removeClass("dsc");
        $('#sortingBtnID').addClass("asc");
        $(".sortingClass").html('<i class="fa-solid fa-arrow-down-wide-short" ></i>')

    }
    else {
        $('#upcomingHistoryTable').DataTable().order([0, 'asc']).draw();
        $('#sortingBtnID').removeClass("asc");
        $('#sortingBtnID').addClass("dsc");
        $(".sortingClass").html('<i class="fa-solid fa-arrow-up-wide-short" ></i>')

    }




}
