<?php
    session_start();

    require_once("core/functions.php");

    require_once("core/device_check.php");

    if (isset($_POST["submit"])) {
        $url = 'https://127.0.0.1/sustineri_api/api/user/login_user.php';
        $data = array('JWT_Token' => $_COOKIE['jwt'], 'Email' => $_POST['Email'], 'Password' => $_POST['Password']);
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

        $cookie_value = $result_decode['ID'];
        //setcookie('logged_in', $cookie_value, time() + (31556926 * 30), "/");
        setcookie('logged_in', $cookie_value, time() +3600); // 1 hour

        header("Location: app.php");
    }
?>

<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body id="home">
        <!--<?php include "partials/background.php"; ?>-->

        <div class="login">
            <div class="overlay"></div>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="login-center">
                            <div class="login-box">
                                <div class="logo mb-4">
                                    <img src="assets/img/sustineri_logo_beeld_s.png">
                                </div>
                                <form action="<?= $_SERVER['PHP_SELF']; ?>" method="POST">
                                    <div class="form-group">
                                        <label for="Email">E-mailadres</label>
                                        <input class="form-control mb-2" type="email" name="Email" id="Email" placeholder="E-mailadres...">
                                    </div>

                                    <div class="form-group">
                                        <label for="Password">Wachtwoord</label>
                                        <div class="row px-3">
                                            <input class="form-control password mb-2" type="password" name="Password" id="Password" placeholder="Wachtwoord...">
                                            <div class="eye">
                                                <i class="far fa-eye" id="show-password"></i>
                                            </div>
                                        </div>
                                    </div>

                                    <div><span><?= $result_decode['Message']; ?></span></div>

                                    <input class="btn btn-primary" type="submit" name="submit" value="Inloggen">
                                    <a href="register.php" class="btn btn-primary">Registreren</a>
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