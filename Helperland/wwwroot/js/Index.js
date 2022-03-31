$(document).ready(function () {
    
$("#go-up").hide();
$("#okbtn > a").click(function () {
    $("#okbtn").fadeOut(500);
});
    $("#IndexCollapsBtn").click(function () {
        $("#IndexCollapsBtn").addClass("CollapsClick");
    });

    $("#IndexLoginBtn").click(function () {
        if ($("#IndexCollapsBtn").hasClass("CollapsClick")) {
            $("#IndexCollapsBtn").trigger('click');
        }
    });

    $(window).scroll(function () {
        var sticky = $("#header1"),
            scroll = $(window).scrollTop();

        if (scroll > 100) {
            sticky.addClass("fixed");
            $(".navbar-brand img").height(54).width(73);
            $(".navbar").css("background", "rgba(82, 82, 82, 0.8)");
            $(".bl").css("background", "#29626d");
            
            

        }
        else {
            sticky.removeClass("fixed");
            $(".navbar-brand img").height(130).width(175);
            $(".navbar").css("background", "transparent");
            $(".bl").css("background", "transparent");
            
        }
    });
    let fbtn = document.getElementById("btn-back-to-top");

    //window.onscroll = function () {
    //    scrollFunction();
    //};
    //function scrollFunction() {
    //    if (
    //        document.body.scrollTop > 20 || document.documentElement.scrollTop > 20
    //    ) {
    //        fbtn.style.display = "block";
    //    }
    //    else {
    //        fbtn.style.display = "none";
    //    }
    //}

    $("#headerManId").click(function () {
        if ($(".header-dropdown-div").hasClass("display-block")) {
            $(".header-dropdown-div").removeClass("display-block");
        } else {
            $(".header-dropdown-div").addClass("display-block");
        }
            
    });

    //onclick we reach to top
    fbtn.addEventListener("click", backToTop);

    const navMenu = document.querySelector(".fullPage");
    const fullPageHidden = document.querySelector(".fullPageHidden");
    const navbarHamburger = document.querySelector(".navSm .hamburger");


    navbarHamburger.addEventListener("click", () => navMenu.classList.add("open"));
    fullPageHidden.addEventListener("click", () => navMenu.classList.remove("open"));

    document.addEventListener("wheel", () => navMenu.classList.remove("open"));
});


function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}





function SendForgotPsddword() {
    //var data = $("#AddressForm").serialize();

    var data = {
        Email: $("#ForgotPasswordEmailId").val()
       
    }
    console.log(data);
  
    $.ajax({
        type: 'POST',
        url: '/Account/ForgotPassword',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
            $(".alertDiv").append("<div class='alert alert-success alert-dismissible fade show' role='alert'>we have sent Password reset link on your Email..<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>");
            console.log(result);
        },
        error: function () {
           
            $("#forgotWarnig").html("Email Not Valid");
            console.log('Failed ');
        }
    });
}



function loadLogOutModal() {
    $.ajax({
        url: "/Home/LogOutModal",
        type: "GET",
       
        success: function (result) {
            $("#LogOutId").html(result);
            $("#LogOutId").modal("show");

        },
        error: function () {
            alert("error");
        },
    });
}