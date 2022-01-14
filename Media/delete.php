<?php
    require_once("core/functions.php");

    if (isset($_POST["submit"])) {
        $url = 'https://127.0.0.1/sustineri_api/api/user/delete_user.php';
        $data = array(
            'JWT_Token' => $_COOKIE['jwt'],
            'User_ID' => $_COOKIE['logged_in'],
            'Email' => $_POST['Email'],
            'Password' => $_POST['Password'],
            'Delete_Confirmation' => $_POST['Delete_Confirmation']
        );
        $data_string = json_encode($data);

        //var_dump($data_string);

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

        if (isset($_SESSION['logged_in'])) {
            session_destroy();
            header("Location: app.php");
        } elseif (isset($_COOKIE['logged_in'])) {
            unset($_COOKIE['logged_in']);
            setcookie('logged_in', '', time() - 3600);
            header("Location: login.php");
        }
    }

?>
<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body>
        <?php include "partials_app/nav.php"; ?>

        <div class="delete-account">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-lg-6">
                        <h2 class="mb-3">Account verwijderen</h2>
                        <form action="<?= $_SERVER['PHP_SELF']; ?>" method="POST" id="form">

                            <h3 class="mt-4 mb-3">Inloggegevens</h3>

                            <div class="form-group">
                                <label for="Email">E-mailadres*</label>
                                <input class="form-control mb-2" type="email" name="Email" id="Email" placeholder="E-mailadres..." required>
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
                                <label for="Delete_Confirmation">Bevestig door DELETE te typen*</label>
                                <input class="form-control mb-2" type="text" name="Delete_Confirmation" id="Delete_Confirmation" placeholder="Typ DELETE ter bevestiging..." required>
                            </div>

                            <?php
                                if ($result_decode['Message']) {
                            ?>
                                <div class="mb-4"><span><?= $result_decode['Message']; ?></span></div>
                            <?php
                                }
                            ?>

                            <input class="btn btn-danger" type="submit" name="submit" value="Verwijderen">
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