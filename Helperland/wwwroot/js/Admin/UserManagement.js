$(document).ready(function () {
    
    $("#userManId").css({ "color": "#646464", "font-weight": "bold" });
    $("#toDateFormId").change(function () {
        if ($("#fromDateFormId").val() != null && $("#toDateFormId").val() != null) {
            if ($("#fromDateFormId").val() > $("#toDateFormId").val()) {
                alert("your To Date " + $("#toDateFormId").val() + " is Smaller then your From Date " + $("#fromDateFormId").val());
                $("#toDateFormId").val('');
            }
        }
    });
    loadTable();

}); 

function loadTable() {
    $.ajax({
        url: "/Admin/UserManagementTable",
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


function OnSearch( clear) {
    if (clear == "clear") {
        $("input,#UserRoleSelectID").val("");
        
    }
    
        var data = {
            UserName: $("#UserNameId").val(),
            PostalCodeForm: $("#PostalCodeFormID").val(),
            phoneNumber: $("#phoneNumber").val(),
            EmailForm: $("#EmailFormId").val(),
            UserRoleSelect: $("#UserRoleSelectID").val(),
            fromDateForm: $("#fromDateFormId").val(),
            toDateFormId: $("#toDateFormId").val(),
        }
    
    $.ajax({
        url: "/Admin/UserFilter",
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

function ActiveUser(id) {
    $.ajax({
        url: "/Admin/ActiveUser",
        type: "GET",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: {
            Id:id,
        },
        success: function (result) {
            OnSearch();
            console.log("Success");
        },
        error: function () {
            alert("error");
        },
    });
}

function InactiveUser(id) {
    $.ajax({
        url: "/Admin/InactiveUser",
        type: "POST",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: {
            Id:id,
        },
        success: function (result) {
            OnSearch();
            console.log("Success");
        },
        error: function () {
            alert("error");
        },
    });
}

function DeleteUsers(id) {
    $.ajax({
        url: "/Admin/DeleteUsers",
        type: "POST",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: {
            Id: id,
        },
        success: function (result) {
            OnSearch();
            console.log("Success");
        },
        error: function () {
            alert("error");
        },
    });
}
