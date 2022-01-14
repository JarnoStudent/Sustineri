<?php

  function authCheck() {
    $currentFileName = basename($_SERVER['PHP_SELF'], ".php");

    if (isset($_COOKIE['logged_in'])) {
      if ($currentFileName == "login") {
        //header("Location: app.php");
      }
    } elseif (!isset($_COOKIE['logged_in'])) {
      if ($currentFileName != "login") {
        header("Location: login.php");
      }
    }
  }

  authCheck();

?>
