$(document).ready(function () {

    $("#headerManId").click(function () {
        if ($(".header-dropdown-div").hasClass("display-block")) {
            $(".header-dropdown-div").removeClass("display-block");
        } else {
            $(".header-dropdown-div").addClass("display-block");
        }

    });
});


/*const { ready } = require("jquery");*/

const navMenu = document.querySelector(".fullPage");
const fullPageHidden = document.querySelector(".fullPageHidden");
const navbarHamburger = document.querySelector(".navSm .hamburger");


navbarHamburger.addEventListener("click", () => navMenu.classList.add("open"));
fullPageHidden.addEventListener("click", () => navMenu.classList.remove("open"));

document.addEventListener("wheel", () => navMenu.classList.remove("open"));







