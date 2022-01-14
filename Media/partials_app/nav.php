<?php
    $url = basename($_SERVER["REQUEST_URI"]);

    $self = $_SERVER["PHP_SELF"];
?>

<div id="nav-app" class="nav-app">
    <div class="container">
        <div class="row">
             <div class="col-12">
                 <ul>
                     <li <?php if ($url === "" || $url === "app.php") { ?>class="current"<?php } ?>>
                         <a href="app.php">
                             <span><i class="fas fa-home"></i></span>
                         </a>
                     </li>
                     <li <?php if ($url === "water.php") { ?>class="current"<?php } ?>>
                         <a href="water.php">
                             <span><i class="fas fa-tint"></i></span>
                         </a>
                     </li>
                     <li <?php if ($url === "gas.php") { ?>class="current"<?php } ?>>
                         <a href="gas.php">
                             <span><i class="fas fa-burn"></i></span>
                         </a>
                     </li>
                     <li class="account <?php if ($url === "account.php") { ?>current<?php } ?>">
                         <a href="account.php">
                             <span><i class="fas fa-user"></i></span>
                         </a>
                     </li>
                     <li class="account">
                         <a href="logout.php">
                             <span><i class="fas fa-sign-out-alt"></i></span>
                         </a>
                     </li>
                 </ul>
             </div>
        </div>
    </div>
</div>

<script>
    var prevScrollpos = window.pageYOffset;
    window.onscroll = function() {
        var currentScrollPos = window.pageYOffset;
        if (prevScrollpos < currentScrollPos) {
            document.getElementById("nav-app").style.bottom = "0";
        } else {
            document.getElementById("nav-app").style.bottom = "-5rem";
        }
        prevScrollpos = currentScrollPos;
    }
</script>