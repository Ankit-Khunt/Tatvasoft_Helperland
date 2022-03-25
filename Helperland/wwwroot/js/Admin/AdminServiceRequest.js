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

        $.ajax({
            url: "/Admin/SRTableAdmin",
            type: "GET",

            success: function (result) {
                $(".SRTable").html(result);
            },
            error: function () {
                alert("error");
            },
        });

        
    });

function onclickevent(id) {
    if ($(id).hasClass("active-status")) {
        $(id).removeClass("active-status");
        $(id).css("background-color", "white");

    }
    else {
        $(id).css("background-color", "#F4F4F4");
        $(id).addClass("active-status");
        $(id).addClass("active-status");

    }
    

}



$("#open").show();
$("#close").hide();


const navMenu = document.querySelector(".fullPage");
const fullPageHidden = document.querySelector(".fullPageHidden");
const navbarHamburger = document.querySelector("#open");


navbarHamburger.addEventListener("click", () => navMenu.classList.add("open"));
fullPageHidden.addEventListener("click", () => navMenu.classList.remove("open"));

document.addEventListener("wheel", () => navMenu.classList.remove("open"));


function OnSearch() {
    var check;
    $("#HasPetFormId").click(function () {
        if ($("#HasPetFormId").is(':checked')) {
            check = 1;
        }
        else {
            check = 0;
        }
    }); 
        
    

    var data = {
        serviceReqId: $("#selectFormId").val(),
        PostalCodeForm: $("#PostalCodeFormID").val(),
        EmailForm: $("#EmailFormId").val(),
        selectCustomerForm: $("#selectCustomerFormId").val(),
        selectSPForm: $("#selectSPFormId").val(),
        statusForm: $("#statusFormID").text(),
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