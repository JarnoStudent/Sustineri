<?php
    if (isset($_COOKIE['logged_in'])) {
        unset($_COOKIE['logged_in']);
        setcookie('logged_in', '', time() - 3600);
        header("Location: login.php");
    }
?>