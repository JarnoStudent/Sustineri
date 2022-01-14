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

        <div class="charts">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h2>Water- en gas- verbruik deze week</h2>
                        <div id="chart"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="global-data">
            <div class="container">
                <h2 class="mb-4">Gemiddeld per douchebeurt</h2>
                <div class="row">
                    <div class="col-12 col-md-4 col-data mb-4 mb-md-0">
                        <div class="box">
                            <div class="icon">
                                <i class="fas fa-tint"></i>
                            </div>
                            <span>73 <span class="value">liter</span></span>
                        </div>
                    </div>
                    <div class="col-12 col-md-4 col-data mb-4 mb-md-0">
                        <div class="box">
                            <div class="icon">
                                <i class="fas fa-thermometer-three-quarters"></i>
                            </div>
                            <span>36 <span class="value">ºC</span></span>
                        </div>
                    </div>
                    <div class="col-12 col-md-4 col-data mb-4 mb-md-0">
                        <div class="box">
                            <div class="icon">
                                <i class="fas fa-burn"></i>
                            </div>
                            <span>0,4 <span class="value">m³</span></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="reward">
            <div class="container">
                <h2 class="mb-3">Wat heeft dit voor effect?</h2>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <div class="align">
                            <img src="assets/img/polar_bear.png" class="mb-3">
                            <span>U heeft 1 ijsbeer gered</span>
                        </div>
                    </div>
                    <div class="col-12 col-md-6">
                        <img src="assets/img/tree.png" class="tree mb-3">
                        <span>U heeft 3 bomen gered</span>
                    </div>
                </div>
            </div>
        </div>

        <div class="website">
            <div class="overlay"></div>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <a href="index.php">Ga naar website</a>
                    </div>
                </div>
            </div>
        </div>

        <?php include "partials/default/scripts.php"; ?>
        <?php include "partials_app/charts_home.php"; ?>
    </body>
</html>