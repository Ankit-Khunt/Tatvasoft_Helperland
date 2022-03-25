var totalServiceTime = 3;
var totalPayment = 0;
var effectivePrice = 0;
var onetimeAddress = 0;
var onetimeNewAddress = 0;
var fatch2ndtimeUpdate = 0;
var serviceHourlyRate = 10;
var serviceDiscount = 20;
var extraHoure = 0;
var selectedDate;
var DateTime;
var CleaningTime;
var FinalDateAndTime;



$(document).ready(function () {

    CleaningTime = $("#cleaningTimeOption1").text();
    
    
    $("#noBad").click(function () {
        var conceptName = $('#noBad').find(":selected").text();
        $("#noBadhtml").html(conceptName);
        var valueofBad = $(this).find(":selected").val();
        if (valueofBad >= 1) {
            setValue1(valueofBad);
        }
    });

    $("#noBath").click(function () {
        var conceptName = $(this).find(":selected").text();
        $("#noBathhtml").html(conceptName);
        var valueofBath = $(this).find(":selected").val();
        if (valueofBath >= 1) {
            setValueBath(valueofBath);
        }
    });
    $("#basicTime").click(function () {
        var conceptName = $(this).find(":selected").text();
        $("#basicPay").html(conceptName);
        var valueofBasicTime = $(this).find(":selected").val();
        if (valueofBasicTime >= 1) {
            setValue2(valueofBasicTime);
        }

    });
    $("#cleanigTime").click(function () {
         CleaningTime = $(this).find(":selected").text();
        $("#cleanigSpan").html(CleaningTime);
    });
    $(function () {


        $('#cleaningDate').datepicker({
            inline: true,
            firstDay: 1,
            changeMonth: false,
            changeYear: true,
            showOtherMonths: true,
            showMonthAfterYear: false,
            yearRange: "2015:2025",
            dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            dateFormat: "dd-mm-yy",

            onSelect: function (date) {
                selectedDate = $(this).val(),
                //var d = new Date(); // for now
                //var h = d.getHours();
                //h = (h < 10) ? ("0" + h) : h;

                //var m = d.getMinutes();
                //m = (m < 10) ? ("0" + m) : m;

                //var s = d.getSeconds();
                //s = (s < 10) ? ("0" + s) : s;

                //datetext = datetext + " " + h + ":" + m + ":" + s;
                    $("#dateOfService").html(selectedDate);


            }
        });
        //alert(date);
        //$("#cleaningDate").on("change", function () {
        //     selectedDate = $(this).val();

        //    $("#dateOfService").html(selectedDate);




        /*});*/
    });


    var d = new Date();
    var mm = d.getMonth() + 1;
    var date = d.getDate();
    if (mm < 10) {
        mm = "0" + mm;
    }
    if (date < 10) {
        date = "0" + date;
    }
    var strDate = date + "/" + mm + "/" + d.getFullYear();
    $("#dateOfService").html(strDate);
    $("#cleaningDate").val(strDate);
    selectedDate = strDate;


    $("#AddNewAddressBtnId").click(function () {
        $(this).css("display", "none");
        $("#AddressBoxId").css("display", "block");
        if (onetimeNewAddress == 0) {
            $.ajax({
                url: "/BookService/fatchUserAddressNewAddress",
                success: function (data) {
                    console.log(data);
                    // $('tbody').append(`<tr> <td>${data[i].addressLine1}</td> <td>${data[i].addressLine2}</td> <td>${data[i].mobile}</td>  </tr>`)
                    //if (data.length == 0) {

                    //}
                    //else {
                        var myarray = [];
                        for (var i = 0; i < data.length; i++) {
                            if (jQuery.inArray(data[i].city, myarray) == -1) {
                                $("#selectCityNewAddressId").append(`<option value="${i + 1}">${data[i].city}</option>`);
                                myarray.push(data[i].city);
                            }

                        }

                        $("#PostalCodeNewAddressId").val(data[0].postalCode);
                        $("#MobilenumNewAddressId").val(data[0].mobile);
                   /* }*/
                    

                },
                error: function (er) {
                    alert("not get");
                    console.log(er);
                }
            });
            onetimeNewAddress++;
        }

    });
    $("#AddressCancleId").click(function () {
        $("#AddressBoxId").css("display", "none");
        $("#AddNewAddressBtnId").css("display", "block");
    });

    

    //$('#chechDemo').click(function () {
    //    $("input[type='radio']:checked").each(function () {
    //        var idVal = $(this).attr("id"); //for getting id from this
    //        var AddressLine1Final = $("label[for='" + idVal + "'] #Addressline1GetSpan" + idVal + "").text();
    //        var AddressLine2Final = $("label[for='" + idVal + "'] #Addressline2GetSpan" + idVal + "").text();
    //        var postalCodeFinal = $("label[for='" + idVal + "'] #postalCodeGetSpan" + idVal + "").text();
    //        var cityFinal = $("label[for='" + idVal + "'] #cityGetSpan" + idVal + "").text();
    //        var PhonenumberFinal = $("label[for='" + idVal + "'] #PhonenumberGetSpan" + idVal + "").text();
          
    //    });
    //});

    $('#cardCheck').hide();
    $('#paymentCheckboxSpan').hide  ();
    //$('#cardNumberId').keyup(function () {
    //    validateUsername();
    //});

   

});

function validateUsername() {
    var result = true;
    let cardNumber = $('#cardNumberId').val();
    let cardMonth = $("#cardMonthID").val();
    let cardCVV = $("#cardCVVID").val();
    if (cardNumber.length == '' || cardMonth.length == '' || cardCVV.length == '') {
        $('#cardCheck').show();
       
        result= false;
    }
    else if (cardNumber.length < 16) 
     {
        $('#cardCheck').show();
        $('#cardCheck').html
            ("**length of card must be 16 Digit");
        
         result= false;
    }
    if (!$(".payment-checkbox").is(':checked')) {
        $('#paymentCheckboxSpan').show();
        $('#paymentCheckboxSpan').html
            ("**Required");

        result = false;
    }
    else {
        $('#paymentCheckboxSpan').hide  ();
    }

    if (result) {
        $('#cardCheck').hide();
      
    }

    return result;
   
       
    
}
var AddressLine1forReUse = $("#AddressLine1Id").val();
var AddressLine2forReUse = $("#AddressLine2Id").val();

function submitAddress() {
    //var data = $("#AddressForm").serialize();
   
    var data = {
        AddressLine1: $("#AddressLine1Id").val(),
        AddressLine2: $("#AddressLine2Id").val(),
        PostalCodeAddresss: $("#PostalCodeNewAddressId").val(),
        CityAddress: $("#selectCityNewAddressId").find(":selected").text(),
        MobileAddress: $("#MobilenumNewAddressId").val()
    }
    console.log(data);
   /* alert(data);*/
    $.ajax({
        type: 'POST',
        url: '/BookService/UserAddressGet',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
           /* alert('Successfully received Data ');*/
            console.log(result);
            onetimeAddress = 0;
            $("#AddressBoxId").css("display", "none");
            $("#AddNewAddressBtnId").css("display", "block");
            fatchUserAddress();
            //MakePayment();
        },
        error: function () {
           /* alert('Failed to receive the Data');*/
            console.log('Failed ');
        }
    });
}

var currentValue1 = 1;
function setValue1(valueNo) {
    // totalServiceTime=valueNo+3;
    if (valueNo == currentValue1) {

        $("#total-payment-calculate-span").html(totalPayment);

    }

    else {
        if (valueNo > currentValue1) {
            for (var i = currentValue1; i < valueNo; i++) {
                // totalPayment+=10;
                totalServiceTime += 1;
            }
            currentValue1 = valueNo;
        }
        else {
            for (var i = currentValue1; i > valueNo; i--) {
                // totalPayment-=10;
                totalServiceTime -= 1;
            }
            currentValue1 = valueNo;
        }
        totalPayment = (totalServiceTime * (serviceHourlyRate/2)) / .5;
        $("#total-service-time-span").html(totalServiceTime);
        $("#total-payment-calculate-span").html(totalPayment);
        effectivePrice = ((100 - serviceDiscount) * totalPayment) / 100;
        $("#effective-price-span").html(effectivePrice);

    }
}
var currentValueBath = 1;
function setValueBath(valueNo) {
    // totalServiceTime=valueNo+3;
    if (valueNo == currentValueBath) {

        $("#total-payment-calculate-span").html(totalPayment);

    }

    else {
        if (valueNo > currentValueBath) {
            for (var i = currentValueBath; i < valueNo; i++) {
                // totalPayment+=10;
                totalServiceTime += 1;
            }
            currentValueBath = valueNo;
        }
        else {
            for (var i = currentValueBath; i > valueNo; i--) {
                // totalPayment-=10;
                totalServiceTime -= 1;
            }
            currentValueBath = valueNo;
        }
        totalPayment = (totalServiceTime * (serviceHourlyRate/2)) / .5;
        $("#total-service-time-span").html(totalServiceTime);
        $("#total-payment-calculate-span").html(totalPayment);
        effectivePrice = ((100 - serviceDiscount) * totalPayment) / 100;
        $("#effective-price-span").html(effectivePrice);

    }
}
var currentValue2 = 1;


function setValue2(valueNo) {
    // totalServiceTime=valueNo+3;
    if (valueNo == currentValue2) {

        $("#total-payment-calculate-span").html(totalPayment);

    }
    // if(valueNo==1){
    //     // $("#total-payment-calculate-span").html(totalPayment);
    //     for(var i=currentValue;i>valueNo;i--){
    //         totalPayment-=10;
    //     }
    //     // $("#total-payment-calculate-span").html(totalPayment);
    // }
    else {



        if (valueNo > currentValue2) {
            for (var i = currentValue2; i < valueNo; i++) {
                // totalPayment+=10;
                totalServiceTime += 1;
            }
            currentValue2 = valueNo;
        }
        else {
            for (var i = currentValue2; i > valueNo; i--) {
                // totalPayment-=10;
                totalServiceTime -= 1;
            }
            currentValue2 = valueNo;
        }
        totalPayment = (totalServiceTime * (serviceHourlyRate/2)) / .5;
        $("#total-service-time-span").html(totalServiceTime);
        $("#total-payment-calculate-span").html(totalPayment);
        effectivePrice = ((100 - serviceDiscount) * totalPayment) / 100;
        $("#effective-price-span").html(effectivePrice);

    }





}


function SetupService() {
    $("#setup-content").addClass("form-step-active");
    $("#schedual-content").removeClass("form-step-active");
    $("#detail-content").removeClass("form-step-active");
    $("#payment-content").removeClass("form-step-active");

    $("#shedual-plan-tab").removeClass("active-tab");
    $("#your-detail-tab").removeClass("active-tab");
    $("#Make-payment-tab").removeClass("active-tab");

    $("#schedual-text").removeClass("service-text-active");
    $("#Your-detail-text").removeClass("service-text-active");
    $("#Make-payment-text").removeClass("service-text-active");

    $("#schedule-img-white").hide();
    $("#shedual-img-black").show();
    $("#Your-detail-img-white").hide();
    $("#Your-detail-img-black").show();
    $("#Make-payment-img-white").hide();
    $("#Make-payment-img-black").show();


}

function SchedualPlan() {

    totalPayment = (totalServiceTime * (serviceHourlyRate/2)) / .5;
    $("#total-payment-calculate-span").html(totalPayment);
    effectivePrice = ((100 - serviceDiscount) * totalPayment) / 100;
    $("#effective-price-span").html(effectivePrice);

    $("#basicPay").html("3 Hrs");
    $("#total-service-time-span").html("3   ");
    $("#noBadhtml").html("1 bad");
    $("#noBathhtml").html("1 bath");
    $("#setup-content").removeClass("form-step-active");
    $("#schedual-content").addClass("form-step-active");


    $("#shedual-plan-tab").addClass("active-tab");

    $("#schedual-text").addClass("service-text-active");


    $("#shedual-img-black").hide();
    $("#schedule-img-white").show();

    

}

function removeSchedualPlan() {
    if ($("#shedual-plan-tab").hasClass("active-tab")) {
        $("#schedual-content").addClass("form-step-active");
        $("#detail-content").removeClass("form-step-active");
        $("#payment-content").removeClass("form-step-active");

    }


    // $("#detail-content").removeClass("form-step-active");
    // $("#payment-content").removeClass("form-step-active");  


    $("#your-detail-tab").removeClass("active-tab");
    $("#Make-payment-tab").removeClass("active-tab");

    $("#Your-detail-text").removeClass("service-text-active");
    $("#Make-payment-text").removeClass("service-text-active");

    $("#Your-detail-img-white").hide();
    $("#Your-detail-img-black").show();
    $("#Make-payment-img-white").hide();
    $("#Make-payment-img-black").show();


}

function YourDetail() {
    
    $("#schedual-content").removeClass("form-step-active");
    $("#detail-content").addClass("form-step-active");

    $("#your-detail-tab").addClass("active-tab");


    $("#Your-detail-text").addClass("service-text-active");


    $("#Your-detail-img-black").hide();
    $("#Your-detail-img-white").show();
    fatchUserAddress();
    
    

}

function fatchUserAddress(){
    if (onetimeAddress == 0 ) {
        $.ajax({
            url: "/BookService/fatchUserAddress",
            success: function (data) {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    //$('tbody').append(`<tr> <td>${data[i].addressLine1}</td> <td>${data[i].addressLine2}</td> <td>${data[i].mobile}</td>  </tr>`)
                    
                    if (fatch2ndtimeUpdate ==i) {
                        $("#AddressRadioBoxId").append
                            (`
                             <div class="AddressRadioBox d-flex">
                                <!-- <span class="checkmark"></span> -->
                                <input type="radio" id="radBtn${i}" name="radio">


                                <label class="radioAddress" for="radBtn${i}">
                                  <div class="ms-3">
                                    <p class="AddressRadioBox-p ps-3"><span class="AddressRadioBoxSpan">Address:</span> <span id="Addressline1GetSpanradBtn${i}">${data[i].addressLine1}</span><span class="ps-2" id="Addressline2GetSpanradBtn${i}">${data[i].addressLine2}</span><span class="ps-2" id="postalCodeGetSpanradBtn${i}">${data[i].postalCode}</span><span class="ps-2" id="cityGetSpanradBtn${i}">${data[i].city}</span></p>
                                    <p class="AddressRadioBox-p ps-3"><span class="AddressRadioBoxSpan">Phone Number:</span> <span id="PhonenumberGetSpanradBtn${i}">${data[i].mobile}</span></p>
                                  </div>

                                </label>


                             </div>
                         `);
                        
                        fatch2ndtimeUpdate++;
                    }
                    
                }

            },
            error: function (er) {
                console.log(er);
            }
        });
        onetimeAddress++;
    }
}
function checkDemofun() {
    $("input[type='radio']:checked").each(function () {
        var idVal = $(this).attr("id");
        alert($("label[for='" + idVal + "']").text());
    });
}


function removeYourDetail() {
    if ($("#your-detail-tab").hasClass("active-tab")) {
        $("#payment-content").removeClass("form-step-active");
        $("#detail-content").addClass("form-step-active");
    }


    $("#Make-payment-tab").removeClass("active-tab");

    $("#Make-payment-text").removeClass("service-text-active");

    $("#Make-payment-img-white").hide();
    $("#Make-payment-img-black").show();

}



function MakePayment() {
    if ($("input[type='radio']:checked").val() == null) {
        $("#addressAlertId").css("display", "block");
      
    }
    else {
        $("#detail-content").removeClass("form-step-active");
        $("#payment-content").addClass("form-step-active");
        $("#Make-payment-tab").addClass("active-tab");

        $("#Make-payment-text").addClass("service-text-active");


        $("#Make-payment-img-white").show();
        $("#Make-payment-img-black").hide();
        $("#addressAlertId").css("display", "none");

    }
        

 }





//select

// extra icons


function extraservice(borderid, gyayid, blueid) {
    if ($(blueid).hasClass("active-icon-blue")) {
        $(blueid).removeClass("active-icon-blue");
        $(gyayid).addClass("active-icon-gray");
        $(borderid).css("border-color", "grey");
    }
    else {
        $(blueid).addClass("active-icon-blue");
        $(gyayid).removeClass("active-icon-gray");
        $(borderid).css("border-color", "#1D7A8C");
    }

    if ($(borderid).hasClass("icon-not-has-value")) {
        if (borderid == "#cabinate-icon") {

            $(borderid + "-mins").html("inside cabinate (extra) ");
            totalServiceTime += .5;
            extraHoure += .5;
            $("#total-service-time-span").html(totalServiceTime);
            $(borderid + "-extra-mins").html("30min");
        }

        if (borderid == "#frige-icon") {
            $(borderid + "-mins").html("inside fridge (extra)");
            totalServiceTime += .5;
            extraHoure += .5;
            $("#total-service-time-span").html(totalServiceTime);
            $(borderid + "-extra-mins").html("30min");
        }
        if (borderid == "#oven-icon") {
            $(borderid + "-mins").html("inside oven (extra)");
            totalServiceTime += .5;
            extraHoure += .5;
            $("#total-service-time-span").html(totalServiceTime);
            $(borderid + "-extra-mins").html("30min");
        }
        if (borderid == "#laundry-icon") {
            $(borderid + "-mins").html("inside laundry (extra)");
            totalServiceTime += .5;
            extraHoure += .5;
            $("#total-service-time-span").html(totalServiceTime);
            $(borderid + "-extra-mins").html("30min");
        }
        if (borderid == "#interior-icon") {
            $(borderid + "-mins").html("inside interior (extra)");
            totalServiceTime += .5;
            extraHoure += .5;
            $("#total-service-time-span").html(totalServiceTime);
            $(borderid + "-extra-mins").html("30min");
        }

        $(borderid).addClass("icon-has-value");
        $(borderid).removeClass("icon-not-has-value");
        totalPayment = (totalServiceTime * (serviceHourlyRate/2)) / .5;
        $("#total-payment-calculate-span").html(totalPayment);
        effectivePrice = ((100 - serviceDiscount) * totalPayment) / 100;
        $("#effective-price-span").html(effectivePrice);
    }
    else {
        $(borderid + "-mins").html("");
        $(borderid + "-extra-mins").html("");
        totalServiceTime -= .5;
        extraHoure -= .5;
        $("#total-service-time-span").html(totalServiceTime);
        totalPayment = (totalServiceTime * (serviceHourlyRate/2)) / .5;
        $("#total-payment-calculate-span").html(totalPayment);
        effectivePrice = ((100 - serviceDiscount) * totalPayment) / 100;
        $("#effective-price-span").html(effectivePrice);

        $(borderid).addClass("icon-not-has-value");

    }

}



//rotate

function rotate(arrow) {
    if ($(arrow).hasClass("rotate-down")) {
        $(arrow).removeClass("rotate-down");
        $(arrow).addClass("rotate-up");
    }
    else {
        $(arrow).removeClass("rotate-up");
        $(arrow).addClass("rotate-down");
    }
}


    //billing

//for form error

function hasValue(elem) {
    $(elem).blur(function () {
        if ($(elem).filter(function () { return $(this).val(); }).length < 1) {
            $(elem + "Span").addClass("error");
            $(elem + "Span").css("display", "block");
        }
        else {
            $(elem + "Span").removeClass("error");
            $(elem + "Span").css("display","none");
        }
    });
}


//----------------------payment-------------------js-------------------

function sendPaymentDetail() {
    var validate = validateUsername();
    if (!validate) {

    } else {
        var data = {

            CardNumber: $("#cardNumberId").val()
        }
        console.log(data);
        /* alert(data);*/
        $.ajax({
            type: 'POST',
            url: '/BookService/CheckCardDetail',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
            data: data,

            success: function (result) {
                // alert('Successfully payment received Data ');
                ServiceRequestSuccessful();



                console.log(result);

            },
            error: function () {
                //alert('Failed to receive payment the Data');

                serviceAlert(1);
                console.log('Failed ');
            }
        });
    }
   
}



function ServiceRequestSuccessful() {
    var checked;
    FinalDateAndTime = selectedDate + " " + CleaningTime;
    if ($("#hasPatID").is(":checked")) {
        checked = true;
    }
    
    var data = {
        
        ServiceHourlyRate:serviceHourlyRate,    
        ServiceHours:totalServiceTime,
        ExtraHours:extraHoure,
        SubTotal:totalPayment,
        Discount:serviceDiscount,
        TotalCost:effectivePrice,
        Comments:$("#commentsID").val(),
        HasPets: checked,
        ServiceStartDate: FinalDateAndTime
        
    }
    console.log(data);
    $.ajax({
        type: 'POST',
            url: '/BookService/ServiceRequestSuccessful',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data:data,

            success : function(result) {
               // alert('Successfully serviceRquest received Data ');
                serviceAddressSend();
                if (extraHoure > 0) {
                    ExtraServiceSend();
                }
                console.log(result);

        },
        error : function(){
            //alert('Failed to receive the Data of serviceRquest');
            $("#alertServiceModelMessageID").html("ServiceAddress has not been Succcessful Submitted");
            serviceAlert(0);
            console.log('Failed ');
        }
    });
}

function serviceAddressSend() {
    var idVal, AddressLine1Final, AddressLine2Final, postalCodeFinal, cityFinal, PhonenumberFinal;
    $("input[type='radio']:checked").each(function () {
         idVal = $(this).attr("id"); //for getting id from this
         AddressLine1Final = $("label[for='" + idVal + "'] #Addressline1GetSpan" + idVal + "").text();
         AddressLine2Final = $("label[for='" + idVal + "'] #Addressline2GetSpan" + idVal + "").text();
         postalCodeFinal = $("label[for='" + idVal + "'] #postalCodeGetSpan" + idVal + "").text();
         cityFinal = $("label[for='" + idVal + "'] #cityGetSpan" + idVal + "").text();
         PhonenumberFinal = $("label[for='" + idVal + "'] #PhonenumberGetSpan" + idVal + "").text();
        /*alert(AddressLine2Final);*/
    });
    var data = {
        AddressLine1: AddressLine1Final,
        AddressLine2: AddressLine2Final,
        CityName: cityFinal,
        ZipcodeValue: postalCodeFinal,
        MobileNum: PhonenumberFinal
    }
    console.log(data);
    $.ajax
        ({
            type: 'POST',
            url: '/BookService/serviceAddressSend',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,

            success: function (result) {
               // alert('Successfully ServiceRequestAddress send ');
                $("#alertServiceModelMessageID").html("Booking has been successfuly Submitted");
                serviceAlert(3);
                console.log(result);

            },
            error: function () {
                //alert('Failed to receive serviceAddressAddress the Data');
                $("#alertServiceModelMessageID").html("Booking has not been successfuly Submitted");
                serviceAlert(3);
                console.log('Failed ');
            }

        });
}


function serviceAlert(message) {
    if (message == 3)
    {
        $.ajax({
            url: "/BookService/AlertServiceReqID",
            success: function (data) {
                console.log(data);
                $("#alertServiceReqID").html(data[0].serviceRequestId);
            },
            error: function (er) {
                console.log(er);
            }
        });
    }

    if (message == 1) {
        $("#alertServiceModelMessageID").html("Paymeant Request has not been Succcessful Submitted");
    }
    $("#alertServiceReqID").html("Not Yet Create");
        $('#ServiceReqAlert').modal('show');
   
}

function ExtraServiceSend() {
    var data = {

        ExtraHours: extraHoure
    }
    console.log(data);
    /* alert(data);*/
    $.ajax({
        type: 'POST',
        url: '/BookService/ExtraServiceSend',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,

        success: function (result) {
           /* alert('Successfully extrareq received Data ');*/
            console.log(result);

        },
        error: function () {
          /*  alert('Failed to receive extrareq ');*/
            console.log('Failed ');
        }
    });

}