$(document).ready(function () {
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
        "columnDefs": [{ orderable: false, targets: 2 }],
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


    $("#SPHistoryVarId").addClass("active");

    callSPHistoryTable(0);

    $("select.StausOfTableClass").change(function () {
        var selectedCountry = $(this).children("option:selected").val();
        alert("You have selected the country - " + selectedCountry);
        callSPHistoryTable(selectedCountry);
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


function callSPHistoryTable(id) {
    $.ajax({
        url: "/ServiceProvider/SPServiceHistoryStaus",
        type: "GET",
        data: {
            Id: id,
        },
        success: function (result) {
            $("#upcomingHistoryTable").html(result);

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}




