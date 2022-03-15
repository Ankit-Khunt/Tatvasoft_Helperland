
var ontimeValueFinal = 0;
var friendlyFinal = 0;
var serviceFinal = 0;
var hourOut = true;

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
            rateclick();
            $("#ratingSubmit").attr("data-id", result.serviceRequestId);
            $(".submitRating").click(function () {
               
                var data = {
                    ServiceRequestId: $(this).attr("id"),
                    OnTimeArrival: ontimeValueFinal,
                    Friendly: ontimeValueFinal,
                    QualityOfService: serviceFinal,
                    Ratings: (ontimeValueFinal + ontimeValueFinal + serviceFinal) / 3,
                    Comments: $("#feedback").val(),
                };
                console.log(data);
                $.ajax({

                    type: "POST",
                    url: "/CustomerService/EditRating",
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    data: data,
                    success: function (result) {
                        alert("success");
                      
                    },
                    error: function () {
                        alert("Failed to receive the Data");
                        console.log("Failed ");
                    },
                });
            });
            
        },
        error: function () {
            alert("error");
        },
    });
});

$("#export").on("click", function () {
    $(".buttons-excel").trigger("click");
});


function RateitJs(starid, id) {
    hoveris = true;
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


    hourOut = true;

   /* mouseout(starid, hourOut);*/
      
   
    //});
    
   
    
}

function rateclick(ratenum,fromwhere) {
    var ontimeValue;
    if (ratenum == "star-1") {
        value = 1;
    }
    else if (ratenum == "star-2") {
        value = 2;
    }
    else if (ratenum == "star-3") {
        value = 3;
    }
    else if (ratenum == "star-4") {
        value = 4;
    }
    else if (ratenum == "star-5") {
        value = 5;
    }

    if (fromwhere == "starOnTime") {
        ontimeValueFinal = value;
        alert(ontimeValueFinal);
    }
    else if (fromwhere == "starFriend") {
        friendlyFinal = value;
        alert(friendlyFinal);
    }
    else if (fromwhere == "starService") {
        serviceFinal = value;
        alert(serviceFinal);
    }
    hourOut = false;
   /* mouseout(fromwhere, hourOut);*/
    return ontimeValue;

}



function onmouseOutFun(starname) {
    if (hourOut == true) {
       
        $(starname).attr("src", "/images/star-unfilled.png");
 
    }
}

