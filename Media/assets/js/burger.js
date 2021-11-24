$(document).ready(function() {
    $(".burger").click(function() {
        if ($(this).hasClass("open")) {
            $(this).removeClass("open");
            $(".nav-mobile").removeClass("opened");

            $("body").removeClass("no-scroll");
        } else {
            $(this).addClass("open");
            $(".nav-mobile").addClass("opened");
        }
    });
});