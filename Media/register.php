<?php

    require_once("core/device_check.php");

    if (isset($_POST["submit"])) {
        $url = 'https://127.0.0.1/sustineri_api/api/user/create_user.php';
        $data = array(
            'JWT_Token' => $_COOKIE['jwt'],
            'Firstname' => $_POST['Firstname'],
            'Insertion' => $_POST['Insertion'],
            'Lastname' => $_POST['Lastname'],
            'Email' => $_POST['Email'],
            'Password' => $_POST['Password'],
            'Password2' => $_POST['Password2']
        );
        $data_string = json_encode($data);

        // use key 'http' even if you send the request to https://...
        $options = array(
            'http' => array(
                'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
                'method'  => 'POST',
                'content' => $data_string

            ),
            'ssl'=>array(
                'verify_peer' => false,
                'verify_peer_name' => false
            )
        );
        $context  = stream_context_create($options);
        $result = file_get_contents($url, false, $context);
        if ($result === FALSE) {

        }

        $result_decode = json_decode($result, true);

        header("Location: login.php");
    }

?>

<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body>
    <!--<?php include "partials/background.php"; ?>-->

    <div class="register">
        <div class="overlay"></div>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="register-center">
                        <div class="register-box">
                            <div class="logo mb-4">
                                <img src="assets/img/sustineri_logo_beeld_s.png">
                            </div>
                            <form action="<?= $_SERVER['PHP_SELF']; ?>" method="POST">

                                <div class="form-group">
                                    <label for="Firstname">Voornaam*</label>
                                    <input class="form-control mb-2" type="text" name="Firstname" id="Firstname" placeholder="Voornaam..." required>
                                </div>

                                <div class="form-group">
                                    <label for="Insertion">Tussenvoegsels</label>
                                    <input class="form-control mb-2" type="text" name="Insertion" id="Insertion" placeholder="Tussenvoegsels...">
                                </div>

                                <div class="form-group">
                                    <label for="Lastname">Achternaam*</label>
                                    <input class="form-control mb-2" type="text" name="Lastname" id="Lastname" placeholder="Achternaam..." required>
                                </div>

                                <div class="form-group">
                                    <label for="Email">E-mail*</label>
                                    <input class="form-control mb-2" type="email" name="Email" id="Email" placeholder="E-mail..." required>
                                </div>

                                <div class="form-group">
                                    <label for="Password">Wachtwoord*</label>
                                    <div class="row px-3">
                                        <input class="form-control password mb-2" type="password" name="Password" id="Password" placeholder="Wachtwoord..." required>
                                        <div class="eye">
                                            <i class="far fa-eye" id="show-password"></i>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="Password2">Wachtwoord herhalen*</label>
                                    <div class="row px-3">
                                        <input class="form-control password mb-2" type="password" name="Password2" id="Password2" placeholder="Wachtwoord bevestigen..." required>
                                        <div class="eye">
                                            <i class="far fa-eye" id="show-password-check"></i>
                                        </div>
                                    </div>
                                </div>

                                <div><span><?= $result_decode['Message']; ?></span></div>

                                <input class="btn btn-primary" type="submit" name="submit" value="Registreren">
                                <a href="login.php" class="btn btn-primary">Annuleren</a>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <?php include "partials/default/scripts.php"; ?>
    </body>
</html>