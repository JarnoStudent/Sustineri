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
                        <h2>Gasverbruik deze week</h2>
                        <div id="chart"></div>
                    </div>
                </div>
            </div>
        </div>

        <?php include "partials/default/scripts.php"; ?>
        <?php include "partials_app/charts_gas.php"; ?>
    </body>
</html>