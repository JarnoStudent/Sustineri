<?php

  function connectDB() {
    try {
      $connection = new PDO('mysql:host=' . DB_HOSTNAME . ';dbname=' . DB_NAME , DB_USERNAME, DB_PASSWORD);

      return $connection;
    } catch (PDOException $e) {
      echo "ERROR: " . $e->getMessage() . "<br>";
      die();
    }
  }

  $connection = connectDB();

?>
