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
        /*"columnDefs": [{ orderable: false, targets: 2 }],*/
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


    $("#BlockCustomerVarId").addClass("active");

    

});



function BlockCustomerFun(Customerid) {
    $.ajax({
        url: "/ServiceProvider/BlockCustomer",
        type: "POST",
        data: {
            customerId: Customerid,
        },
        success: function (result) {
            window.location.href = "/serviceprovider/BlockCustomer";

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}

function UnlockCustomerFun(Customerid) {
    $.ajax({
        url: "/ServiceProvider/unblockCustomer",
        type: "POST",
        data: {
            customerId: Customerid,
        },
        success: function (result) {
            window.location.href = "/serviceprovider/BlockCustomer";

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}










