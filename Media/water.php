<?php
    require_once("core/functions.php");
?>
<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body>
        <?php include "partials_app/nav.php"; ?>

        <div class="detail-charts">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h2>Waterverbruik deze week</h2>
                        <div id="chart"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="limit">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <form action="<?= $_SERVER['PHP_SELF']; ?>" method="POST">
                            <div class="form-group">
                                <label for="limit">Water limiet per dag</label>
                                <input class="form-control mb-2" type="number" name="limit" id="limit" placeholder="Aantal liters...">
                                <input class="btn btn-primary" type="submit" value="Opslaan">
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>

        <?php include "partials/default/scripts.php"; ?>
        <?php include "partials_app/charts_water.php"; ?>
    </body>
</html>