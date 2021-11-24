$(document).ready(function(){
  $("#show-password").click(function(){
    if ($(this).hasClass("fa-eye")) {
      $(this).removeClass("fa-eye");
      $(this).addClass("fa-eye-slash");
      $("#userPassword").attr("type", "text");
    } else {
      $(this).removeClass("fa-eye-slash");
      $(this).addClass("fa-eye");
      $("#userPassword").attr("type", "password");
    }
  });
});
