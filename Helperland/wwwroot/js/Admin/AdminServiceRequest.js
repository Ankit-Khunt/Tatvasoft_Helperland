$(document).ready(function () {
   
       

    loadTable();    

    $("#serviceReqID").css({ "color": "#646464", "font-weight": "bold" });

    $("#toDateFormId").change(function () {
        if ($("#fromDateFormId").val() != null && $("#toDateFormId").val() != null) {
            if ($("#fromDateFormId").val() > $("#toDateFormId").val()) {
                alert("your To Date " + $("#toDateFormId").val() + " is Smaller then your From Date " + $("#fromDateFormId").val());
                $("#toDateFormId").val('');
            }
        }
    });
    
        
        

        
    });


function loadTable() {
    $.ajax({
        url: "/Admin/SRTableAdmin",
        type: "GET",

        success: function (result) {
            $(".SRTable").html(result);
            $(".navbardropdown").click(function () {
                if ($(this).hasClass("active-status")) {

                    $(this).removeClass("active-status");


                }
                else {
                    $(this).addClass("active-status");
                }
            });
        },
        error: function () {
            alert("error");
        },
    });
}



$("#open").show();
$("#close").hide();


const navMenu = document.querySelector(".fullPage");
const fullPageHidden = document.querySelector(".fullPageHidden");
const navbarHamburger = document.querySelector("#open");


navbarHamburger.addEventListener("click", () => navMenu.classList.add("open"));
fullPageHidden.addEventListener("click", () => navMenu.classList.remove("open"));

document.addEventListener("wheel", () => navMenu.classList.remove("open"));


function OnSearch(clear) {
    var check;
    if (clear == "clear") {
        $("input,#statusFormID").val("");

    }

    if (clear !="clear")
        if ($("#HasPetFormId").is(':checked')) {
            check = 1;
        }
        else {
            check = 0;
        }
    
        
    

    var data = {
        serviceReqId: $("#selectFormId").val(),
        PostalCodeForm: $("#PostalCodeFormID").val(),
        EmailForm: $("#EmailFormId").val(),
        selectCustomerForm: $("#selectCustomerFormId").val(),
        selectSPForm: $("#selectSPFormId").val(),
        statusForm: $("#statusFormID").val(),
        HasPetForm: check,
        fromDateForm: $("#fromDateFormId").val(),
        toDateFormId: $("#toDateFormId").val(),
    }
    
    $.ajax({
        url: "/Admin/filterApply",
        type: "GET",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
            $(".SRTable").html(result);
        },
        error: function () {
            alert("error");
        },
    });

}

function onEditLinkClick(id) {
    $.ajax({
        url: "/admin/EditServiceRequest",
        type: "GET",
        data: {
            id: id,
        },
        success: function (result) {
            $("#adminModal").html(result);
            $("#adminModal").modal("show");
        },
        error: function () {
            alert("error");
        },
    });
}
$(document).on("click", ".serviceID", function () {
    
        $.ajax({
            url: "/admin/servicedetails",
            type: "GET",
            data: {
                id: $(this).text(),
            },
            success: function (result) {
                $("#adminModal").html(result);
                $("#adminModal").modal("show");
            },
            error: function () {
                alert("error");
            },
        });
    
});

function onCancelLinkClick(id) {
    $.ajax({
        url: "/admin/cancelservicerequest",
        type: "GET",
        success: function (result) {
            $("#adminModal").html(result);
            $("#adminModal").modal("show");
            $("#cancelRequestBtn").attr("disabled", true);
            cancelDialogEvents();
            $("#cancelRequestBtn").click(function () {
                cancelRequestPost(id);
            });
        },
        error: function () {
            alert("error");
        },
    });
}

function cancelDialogEvents() {
    $("#cancelRequestBtn").css("background-color", "#6DA9B5");
    $(".cancel-request textarea").on("keyup", function () {
        var textarea_value = $(".cancel-request textarea").val();
        if (textarea_value != "") {
            $("#cancelRequestBtn").attr("disabled", false);
            $("#cancelRequestBtn").css("background-color", "#1D7A8C");
        } else {
            $("#cancelRequestBtn").attr("disabled", true);
            $("#cancelRequestBtn").css("background-color", "#6DA9B5");
        }
    });
}

function cancelRequestPost(serviceId) {
    $.ajax({
        url: "/admin/cancelservicerequest",
        type: "POST",
        data: {
            id: serviceId,
            comment: $(".cancel-request textarea").val(),
        },
        success: function (result) {
            $("#adminModal").html(result);
            $("#adminModal").modal("show");
            $("#adminCancleClose").click(function () {
                OnSearch();
            });
        },
        error: function () {
            alert("error");
        },
    });
}
