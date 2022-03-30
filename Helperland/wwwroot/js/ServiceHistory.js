
var ontimeValueFinal = 0;
var friendlyFinal = 0;
var serviceFinal = 0;
var hourOut = true;

$(document).ready(function () {
   
    var table =$('#upcomingHistoryTable').DataTable({
        searching: false,
        info: false,
        responsive: true,
        buttons: ["excelHtml5"],
        stripeClasses: [],
        aLengthMenu: [
            [5, 10, 15, -1],
            [5, 10, 25, "All"],
        ],
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',
        pageLength: 10,
        paging: "true",
        pagingType: "full_numbers",
        language: {
            paginate: {
                first: "<img src='/images/firstPage.png' alt='first' />",
                previous: "<img src='/images/previous.png' alt='previous' />",
                next: '<img src="/images/previous.png" alt="next" style="transform: rotate(180deg)" />',
                last: "<img src='/images/firstPage.png' alt='first' style='transform: rotate(180deg)' />",
            },
        },
        
    });


    $(".buttons-excel").hide();
    var entries = table.page.info().recordsTotal;
    $("#table_id_length label").append(" Total Record: " + entries);
    $("#ServiceHistoryVarId").addClass("active");
    $("#export").on("click", function () {
        $(".buttons-excel").trigger("click");
    });
  
        


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
                    Friendly: friendlyFinal,
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
                        $("#EditRatingCloseBtn").click(function () {
                            location.reload();
                        });
                      
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
        loadRating();
        
    }
    else if (fromwhere == "starFriend") {
        friendlyFinal = value;
        loadRating();
    }
    else if (fromwhere == "starService") {
        serviceFinal = value;
        loadRating();
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

function loadRating() {
    count = 0;
    var ratingAvg = [ontimeValueFinal,friendlyFinal,serviceFinal];
    for (var i = 0; i < 3; i++) {
        if (ratingAvg[i] != 0) {
            count++;
        }
    }
   var ratingAvg2 = (ontimeValueFinal + friendlyFinal + serviceFinal) / count;
    
    $(".rateit-average").html('');
    for (var i = 1; i < 6; i++)
    {


        if (i <= ratingAvg2)
        {
            $(".rateit-average").append('<img src="/images/star-filled.png" class="star-filled me-1">');
        } 
        
        else
        {
            
            $(".rateit-average").append(`<img src="/images/star-unfilled.png" class="star-unfilled me-1">  `);
             
                   
        }
                        
                    
    }
    $("#averageRatingText").html(ratingAvg2.toFixed(2));

}

function ServiceDetailFun(serviceId) {

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
