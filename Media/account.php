<?php
    require_once("core/functions.php");

    $url_get = 'https://127.0.0.1/sustineri_api/api/user/get_user.php';
    $data_get = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in']
    );
    $data_string_get = json_encode($data_get);

    // use key 'http' even if you send the request to https://...
    $options_get = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get  = stream_context_create($options_get);
    $result_get = file_get_contents($url_get, false, $context_get);
    if ($result_get === FALSE) {

    }

    $result_decode_get = json_decode($result_get, true);

    //echo $result_decode['ID'];

    if (isset($_POST["submit"])) {
        $url = 'https://127.0.0.1/sustineri_api/api/user/update_user.php';
        $data = array(
            'JWT_Token' => $_COOKIE['jwt'],
            'User_ID' => $_COOKIE['logged_in'],
            'Firstname' => $_POST['Firstname'],
            'Insertion' => $_POST['Insertion'],
            'Lastname' => $_POST['Lastname'],
            'New_Email' => $_POST['New_Email'],
            'Email' => $_POST['Email'],
            'Password' => $_POST['Password']
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
    }

?>
<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body>
        <?php include "partials_app/nav.php"; ?>

        <div class="account-details">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-lg-6">
                        <h2 class="mb-3">Accountgegevens</h2>
                        <form action="<?= $_SERVER['PHP_SELF']; ?>" method="POST">

                            <div class="form-group">
                                <label for="Firstname">Voornaam*</label>
                                <input class="form-control mb-2" type="text" name="Firstname" id="Firstname" placeholder="Voornaam..." value="<?= $result_decode_get['Firstname'] ?>" required>
                            </div>

                            <div class="form-group">
                                <label for="Insertion">Tussenvoegsels</label>
                                <input class="form-control mb-2" type="text" name="Insertion" id="Insertion" placeholder="Tussenvoegsels..." value="<?= $result_decode_get['Insertion'] ?>">
                            </div>

                            <div class="form-group">
                                <label for="Lastname">Achternaam*</label>
                                <input class="form-control mb-2" type="text" name="Lastname" id="Lastname" placeholder="Achternaam..." value="<?= $result_decode_get['Lastname'] ?>" required>
                            </div>

                            <div class="form-group">
                                <label for="New_Email">Nieuw e-mailadres*</label>
                                <input class="form-control mb-2" type="email" name="New_Email" id="New_Email" placeholder="Nieuw e-mailadres..." value="<?= $result_decode_get['Email'] ?>" required>
                            </div>

                            <h2 class="mt-5 mb-3">Inloggegevens</h2>

                            <div class="form-group">
                                <label for="Email">Huidig e-mailadres*</label>
                                <input class="form-control mb-2" type="email" name="Email" id="Email" placeholder="Huidig e-mailadres..." required>
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

                            <?php
                                if ($result_decode['Message']) {
                            ?>
                                <div class="mb-4"><span><?= $result_decode['Message']; ?></span></div>
                            <?php
                                }
                            ?>

                            <input class="btn btn-primary" type="submit" name="submit" value="Opslaan">
                            <a href="app.php" class="btn btn-primary">Annuleren</a>
                            <a href="password.php" class="btn btn-primary">Wijzig wachtwoord</a>
                            <a href="delete.php" class="btn btn-danger">Account verwijderen</a>
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