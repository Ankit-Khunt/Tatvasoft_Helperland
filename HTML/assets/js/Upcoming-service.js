
        var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
        var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
          return new bootstrap.Popover(popoverTriggerEl)
        })


const hamburger=document.querySelector(".hamburger");
const navbarManu=document.querySelector(".navbar-nav");
const nav2=document.querySelector(".nav2");


hamburger.addEventListener("click",() =>{
    hamburger.classList.toggle("active");
    navbarManu.classList.toggle("active");
    nav2.classList.toggle("active");
})

