$(document).ready(function () {



    $("#SPHistoryVarId").addClass("active");

    callSPHistoryTable(0);
    $('#upcomingHistoryTable').DataTable({
        searching: false,
        info: true,
        ordering: true,
        responsive: true,
        buttons: ["excelHtml5"],
        dom: '<"float-left"B><"top">rt<"bottom"lip><"clear">',
        pageLength: 10,
        paging: true,
        pagingType: "full_numbers",
        language: {
            paginate: {
                first: "<img src='/images/firstPage.png' alt='first' />",
                previous: "<img src='/images/previous.png' alt='previous' />",
                next: '<img src="/images/previous.png" alt="next" style="transform: rotate(180deg)" />',
                last: "<img src='/images/firstPage.png' alt='first' style='transform: rotate(180deg)' />",
            },
        },
        "oLanguage": {
            "sInfo": "Total Records: _TOTAL_"
        },
        "order": [],
        
    });
    $(".buttons-excel").hide();
    //var entries = table.page.info().recordsTotal;
    //$("#table_id_length label").append(" Total Record: " + entries);

    $("select.StausOfTableClass").change(function () {
        var selectedValue = $(this).children("option:selected").val();

        callSPHistoryTable(selectedValue);
    });
    $("#export").on("click", function () {
        $(".buttons-excel").trigger("click");
    });

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
            /*$("#upcomingHistoryTable").html(result);*/

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}




