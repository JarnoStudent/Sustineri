<?php
    require_once("core/autoload.php");

    $returnData = login($connection, $_POST);
?>

<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body id="home">
        <?php include "partials/background.php"; ?>

        <div class="login">
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
                                        <label for="userName">Gebruikersnaam</label>
                                        <input class="form-control mb-2" type="text" name="userName" id="userName" placeholder="Gebruikersnaam...">

                                        <div class="error-message-box">
                                            <?php
                                                if (isset($returnData['validateErrors']['userName']) && $returnData['validateErrors'] != ""){
                                            ?>
                                                <span><?= $returnData['validateErrors']['userName']; ?></span>
                                            <?php
                                                }
                                            ?>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label for="userPassword">Wachtwoord</label>
                                        <div class="row px-3">
                                            <input class="form-control password mb-2" type="password" name="userPassword" id="userPassword" placeholder="Wachtwoord...">
                                            <div class="eye">
                                                <i class="far fa-eye" id="show-password"></i>
                                            </div>
                                        </div>

                                        <div class="error-message-box">
                                            <?php
                                                if (isset($returnData['validateErrors']['userPassword']) && $returnData['validateErrors'] != ""){
                                            ?>
                                                <span><?= $returnData['validateErrors']['userPassword']; ?></span>
                                            <?php
                                                }
                                            ?>
                                        </div>

                                    </div>

                                    <?php
                                        if (isset($returnData['response']['message']) && $returnData['response'] != ""){
                                    ?>
                                        <p class="not-matching mb-1"><?= $returnData['response']['message']; ?></p>
                                    <?php
                                        }
                                    ?>

                                    <input class="btn btn-primary" type="submit" value="Inloggen">
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