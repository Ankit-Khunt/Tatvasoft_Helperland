$("#open").show();
$("#close").hide();

$("#open").click(function () {
  openNavbar();
});

$("#close").click(function () {
  closeNavbar();
});
function closeNavbar() {
  $(".navigation").animate(
    {
      width: "0px",
    }
  );
  $(".navigation").hide();
  $("#open").show();
  $("#close").hide();
}
function openNavbar() {
  $(".navigation").animate(
    {
      width: "100%",
    }
  );
  $(".navigation").show();
  $("#open").hide();
  $("#close").show();
}
