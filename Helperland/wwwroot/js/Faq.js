let rotation = 0;
function rotateimg1() {
  rotation += 90; // add 90 degrees, you can change this as you want
  if (rotation === 180) {
    rotation = 0;
  }
      document.querySelector("#img1").style.transform = `rotate(${rotation}deg)`;
      }


function rotateimg2() {
  rotation += 90; // add 90 degrees, you can change this as you want
  if (rotation === 180) {
    rotation = 0;
  }
      document.querySelector("#img2").style.transform = `rotate(${rotation}deg)`;
      }


function rotateimg3() {
  rotation += 90; // add 90 degrees, you can change this as you want
  if (rotation === 180) {
    rotation = 0;
}
    document.querySelector("#img3").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg4() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img4").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg5() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img5").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg6() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img6").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg7() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img7").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg8() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img8").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg9() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img9").style.transform = `rotate(${rotation}deg)`;
}function rotateimg10() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img10").style.transform = `rotate(${rotation}deg)`;
}function rotateimg11() {
    rotation += 90; // add 90 degrees, you can change this as you want
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img11").style.transform = `rotate(${rotation}deg)`;
}


function displayServiceProvider() {
    $(".customer-content-main-div").addClass("display-none");
    $(".serviceprovider-div").removeClass("display-none");
    $("#forcusID").css("color", "#525252");
    $("#forserId").css("color", "#ffff");
    $("#customerIdFaq").removeClass("customer-main");
    $("#serProviIdfaq").addClass("serviceProvider-main");
}
function displayCustomer() {
    $(".customer-content-main-div").removeClass("display-none");
    $("#forcusID").css("color", "#ffff");
    $("#forserId").css("color", "#525252");
    $(".serviceprovider-div").addClass("display-none");
    $("#customerIdFaq").addClass("customer-main");
    $("#serProviIdfaq").removeClass("serviceProvider-main");
}