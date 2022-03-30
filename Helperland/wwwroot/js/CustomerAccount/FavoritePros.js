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
        url: "/CustomerService/MarkBlocked",
        type: "POST",
        data: {
            spId: Customerid,
        },
        success: function (result) {
            window.location.href = "/CustomerService/FavouriteProviders";

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}

function UnlockCustomerFun(Customerid) {
    $.ajax({
        url: "/CustomerService/MarkUnBlocked",
        type: "POST",
        data: {
            spId: Customerid,
        },
        success: function (result) {
            window.location.href = "/CustomerService/FavouriteProviders";

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}

function MakeFavourite(Customerid) {
    $.ajax({
        url: "/CustomerService/MarkFavourite",
        type: "POST",
        data: {
            spId: Customerid,
        },
        success: function (result) {
            window.location.href = "/CustomerService/FavouriteProviders";

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}

function MAkeUnfavourite(Customerid) {
    $.ajax({
        url: "/CustomerService/MarkUnfavourite",
        type: "POST",
        data: {
            spId: Customerid,
        },
        success: function (result) {
            window.location.href = "/CustomerService/FavouriteProviders";

        },
        error: function () {
            alert("error SPServiceHistoryStaus");
        },
    });
}










