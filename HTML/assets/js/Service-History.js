
const navMenu = document.querySelector(".fullPage");
const fullPageHidden = document.querySelector(".fullPageHidden");
const navbarHamburger = document.querySelector(".navSm .hamburger");


navbarHamburger.addEventListener("click", () => navMenu.classList.add("open"));
fullPageHidden.addEventListener("click", () => navMenu.classList.remove("open"));

document.addEventListener("wheel", () => navMenu.classList.remove("open"));







