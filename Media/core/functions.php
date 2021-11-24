<?php

  /* Login */

  //session_start();

  function login($connection, $postData) {
    $validateErrors = array();
    $response = array();

    if ($postData){
      if (empty($postData['userName']) && $postData['userName'] == ""){
        $validateErrors['userName'] = "Vul a.u.b. een gebruikersnaam in.";
      }

      if (empty($postData['userPassword']) && $postData['userPassword'] == ""){
        $validateErrors['userPassword'] = "Vul a.u.b. een wachtwoord in.";
      }
    } else {
      $validateErrors['post-state'] = false;
    }

    if (!$validateErrors){
      //$queryData = array($postData['userName'], md5($postData['userPassword']));
      $queryData = array($postData['userName'], $postData['userPassword']);

      $STH = $connection->prepare("SELECT * FROM userdata WHERE userName = ? AND userPassword = ?");

      $STH->execute($queryData);

      $responseData = $STH->fetch(PDO::FETCH_ASSOC);

      if ($responseData){
        //$_SESSION['admin-user'] = $responseData;

        $response['message'] = "U bent ingelogd!";

        //header("Location: " . $_SERVER['PHP_SELF']);
        header("Location: /sustineri/app.php");
      } else {
        $response['message'] = "De ingevoerde gebruikersnaam en/of wachtwoord is onjuist.";
      }
    }

    return array("validateErrors" => $validateErrors, "response" => $response);
  }

  /*function authCheck() {
    $currentFileName = basename($_SERVER['PHP_SELF'], ".php");

    if ($_SESSION && $_SESSION['admin-user']){
      if ($currentFileName == "login"){
        header("Location: index.php");
      }
    } else {
      if ($currentFileName != "login"){
        header("Location: login.php");
      }
    }
  }

  authCheck();*/

  /*function logout() {
    if ($_SESSION && $_SESSION['admin-user']){
      unset($_SESSION['admin-user']);

      header("Location: " . $_SERVER['PHP_SELF']);
    }
  }*/

  /* End login */

?>
