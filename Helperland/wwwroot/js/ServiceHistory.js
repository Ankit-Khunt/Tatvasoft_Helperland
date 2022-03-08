

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


    

    $("#ServiceHistoryVarId").addClass("active");

  
        


});

//int ontimeValueFinal = 0;
//int friendlyFinal = 0;
//int serviceFinal = 0;

function onRelod() {
    location.reload();
}

$(".rate-sp").click(function () {
    $.ajax({
        url: "/CustomerService/EditRating",
        type: "GET",
        data: {
            id: $(this).attr("data-id"),
        },
        success: function (result) {
            
            $("#ratingModal").html(result);
            $("#ratingModal").modal("show");
            var ontimeValue;
            RateitJs();
            rateclick(ratenum);
            
            
        },
        error: function () {
            alert("error");
        },
    });
});

$("#export").on("click", function () {
    $(".buttons-excel").trigger("click");
});


function RateitJs(starid,id) {
    /* $(this).mouseover(function () {*/
    if (id == "1") {
        $(starid + "-1").attr('src', '/images/star-filled.png');
        $(starid + "-2").attr('src', '/images/star-unfilled.png');
        $(starid + "-3").attr("src", "/images/star-unfilled.png");
        $(starid + "-4").attr("src", "/images/star-unfilled.png");
        $(starid + "-5").attr("src", "/images/star-unfilled.png");
    }


    /*});*/
    //$(starid + "-2").mouseover(function () {
    else if (id == "2") {
        $(starid + "-1").attr('src', '/images/star-filled.png');
        $(starid + "-2").attr('src', '/images/star-filled.png');
        $(starid + "-3").attr("src", "/images/star-unfilled.png");
        $(starid + "-4").attr("src", "/images/star-unfilled.png");
        $(starid + "-5").attr("src", "/images/star-unfilled.png");
    }


    //});
    //$(starid + "-3").mouseover(function () {
    else if (id == "3") {
        $(starid + "-1").attr('src', '/images/star-filled.png');
        $(starid + "-2").attr('src', '/images/star-filled.png');
        $(starid + "-3").attr("src", "/images/star-filled.png");
        $(starid + "-4").attr("src", "/images/star-unfilled.png");
        $(starid + "-5").attr("src", "/images/star-unfilled.png");
    }


    //});
    //$(starid + "-4").mouseover(function () {
    else if (id == "4") {
        $(starid + "-1").attr('src', '/images/star-filled.png');
        $(starid + "-2").attr('src', '/images/star-filled.png');
        $(starid + "-3").attr("src", "/images/star-filled.png");
        $(starid + "-4").attr("src", "/images/star-filled.png");
        $(starid + "-5").attr("src", "/images/star-unfilled.png");
    }
       

    //});
    //$(starid + "-5").mouseover(function () {
    else {
        $(starid + "-1").attr('src', '/images/star-filled.png');
        $(starid + "-2").attr('src', '/images/star-filled.png');
        $(starid + "-3").attr("src", "/images/star-filled.png");
        $(starid + "-4").attr("src", "/images/star-filled.png");
        $(starid + "-5").attr("src", "/images/star-filled.png");
        }

      

    //});
    
    $(".rateStar").mouseout(function () {
        $(".rateStar").attr("src", "/images/star-unfilled.png");
    });
    
}

function rateclick(ratenum,fromwhere) {
    var ontimeValue;
    if (ratenum == "star-1") {
        ontimeValue = 1;
    }
    else if (ratenum == "star-2") {
        ontimeValue = 2;
    }
    else if (ratenum == "star-3") {
        ontimeValue = 3;
    }
    else if (ratenum == "star-4") {
        ontimeValue = 4;
    }
    else if (ratenum == "star-5") {
        ontimeValue = 5;
    }

    if (fromwhere == "starOnTime") {
        alert(ontimeValue);
    }
    else if (fromwhere == "starFriend") {

    }
    else if (fromwhere == "starService") {

    }
   
    return ontimeValue;

}









