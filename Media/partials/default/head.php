<meta charset="UTF-8">
<meta name="language" content="dutch">
<meta name="author" content="Projectgroep 21">
<meta name="description" content="Sustineri">
<meta name="keywords" content="Water, Duurzaam, Besparen, Douchen">
<meta name="copyright" content="Sustineri">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta name="theme-color" content="#068ec8">
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2&family=Secular+One?family=Prata&family=Roboto&display=swap" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="css/style.css">
<link rel="shortcut icon" type="image/x-icon" href="assets/img/favicon.png">
<?php
    $url = basename($_SERVER["REQUEST_URI"]);

    $self = $_SERVER["PHP_SELF"];
?>
<title>Sustineri <?php if ($url === "" || $url === "index.php") { ?>| Home<?php } elseif ($url === "login.php") { ?>| Login <?php } elseif ($url === "register.php") { ?>| Registreren <?php } ?></title>