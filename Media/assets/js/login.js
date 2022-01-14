$(document).ready(function(){
  $("#show-password").click(function(){
    if ($(this).hasClass("fa-eye")) {
      $(this).removeClass("fa-eye");
      $(this).addClass("fa-eye-slash");
      $("#Password").attr("type", "text");
    } else {
      $(this).removeClass("fa-eye-slash");
      $(this).addClass("fa-eye");
      $("#Password").attr("type", "password");
    }
  });
  $("#show-password-check").click(function(){
    if ($(this).hasClass("fa-eye")) {
      $(this).removeClass("fa-eye");
      $(this).addClass("fa-eye-slash");
      $("#Password2").attr("type", "text");
    } else {
      $(this).removeClass("fa-eye-slash");
      $(this).addClass("fa-eye");
      $("#Password2").attr("type", "password");
    }
  });
  $("#show-new-password").click(function(){
    if ($(this).hasClass("fa-eye")) {
      $(this).removeClass("fa-eye");
      $(this).addClass("fa-eye-slash");
      $("#New_Password").attr("type", "text");
    } else {
      $(this).removeClass("fa-eye-slash");
      $(this).addClass("fa-eye");
      $("#New_Password").attr("type", "password");
    }
  });
  $("#show-new-password-check").click(function(){
    if ($(this).hasClass("fa-eye")) {
      $(this).removeClass("fa-eye");
      $(this).addClass("fa-eye-slash");
      $("#New_Password2").attr("type", "text");
    } else {
      $(this).removeClass("fa-eye-slash");
      $(this).addClass("fa-eye");
      $("#New_Password2").attr("type", "password");
    }
  });
});
