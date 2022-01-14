<?php
    require_once("core/functions.php");

    if (isset($_POST["submit"])) {
        $url = 'https://127.0.0.1/sustineri_api/api/user/update_password.php';
        $data = array(
            'JWT_Token' => $_COOKIE['jwt'],
            'User_ID' => $_COOKIE['logged_in'],
            'New_Password' => $_POST['New_Password'],
            'New_Password2' => $_POST['New_Password2'],
            'Email' => $_POST['Email'],
            'Password' => $_POST['Password']
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
    }

?>
<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body>
        <?php include "partials_app/nav.php"; ?>

        <div class="new-password">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-lg-6">
                        <h2 class="mb-3">Wachtwoord</h2>
                        <form action="<?= $_SERVER['PHP_SELF']; ?>" method="POST">

                            <div class="form-group">
                                <label for="New_Password">Nieuw wachtwoord*</label>
                                <div class="row px-3">
                                    <input class="form-control password mb-2" type="password" name="New_Password" id="New_Password" placeholder="Nieuw wachtwoord..." required>
                                    <div class="eye">
                                        <i class="far fa-eye" id="show-new-password"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="New_Password2">Nieuw wachtwoord herhalen*</label>
                                <div class="row px-3">
                                    <input class="form-control password mb-2" type="password" name="New_Password2" id="New_Password2" placeholder="Nieuw wachtwoord..." required>
                                    <div class="eye">
                                        <i class="far fa-eye" id="show-new-password-check"></i>
                                    </div>
                                </div>
                            </div>

                            <h2 class="mt-5 mb-3">Inloggegevens</h2>

                            <div class="form-group">
                                <label for="Email">E-mailadres*</label>
                                <input class="form-control mb-2" type="email" name="Email" id="Email" placeholder="E-mailadres..." required>
                            </div>

                            <div class="form-group">
                                <label for="Password">Huidig wachtwoord*</label>
                                <div class="row px-3">
                                    <input class="form-control password mb-2" type="password" name="Password" id="Password" placeholder="Huidig wachtwoord..." required>
                                    <div class="eye">
                                        <i class="far fa-eye" id="show-password"></i>
                                    </div>
                                </div>
                            </div>

                            <?php
                                if ($result_decode['Message']) {
                            ?>
                                <div class="mb-4"><span><?= $result_decode['Message']; ?></span></div>
                            <?php
                                }
                            ?>

                            <input class="btn btn-primary" type="submit" name="submit" value="Opslaan">
                            <a href="account.php" class="btn btn-primary">Annuleren</a>
                        </form>
                    </div>
                    <div class="col-12 col-lg-6 d-none d-lg-block">
                        <img src="assets/img/sustineri_logo_beeld_s.png">
                    </div>
                </div>
            </div>
        </div>

        <?php include "partials/default/scripts.php"; ?>
    </body>
</html>