$("#go-up").hide();
$("#okbtn > a").click(function () {
    $("#okbtn").fadeOut(500);
});
$(window).scroll(function () {
    var sticky = $("#header"),
        scroll = $(window).scrollTop();

    if (scroll > 64) {
        sticky.addClass("fixed");
        $(".navbar-brand img").height(54).width(73);
        $(".navbar").css("background", "rgba(82, 82, 82, 0.8)");
        $(".bl").css("background", "#29626d");
        ;
    }
    else {
        sticky.removeClass("fixed");
        $(".navbar-brand img").height(102).width(138);
        $(".navbar").css("background", "transparent");
        $(".bl").css("background", "transparent");
    }
});
let fbtn = document.getElementById("btn-back-to-top");

window.onscroll = function () {
    scrollFunction();
};
function scrollFunction() {
    if (
        document.body.scrollTop > 20 || document.documentElement.scrollTop > 20
    ) {
        fbtn.style.display = "block";
    }
    else {
        fbtn.style.display = "none";
    }
}

// onclick we reach to top
fbtn.addEventListener("click", backToTop);

function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}