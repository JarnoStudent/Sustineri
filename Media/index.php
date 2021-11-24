<!DOCTYPE html>
<html>
    <head>
        <?php include "partials/default/head.php"; ?>
    </head>
    <body id="home">
        <?php include "partials/background.php"; ?>
        <?php include "partials/nav.php"; ?>

        <?php include "partials/header.php"; ?>

        <div class="blocks">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-4 mb-5 mb-md-0">
                        <div class="box">
                            <div class="icon">
                                <i class="fas fa-shower"></i>
                            </div>
                            <span>Water</span>
                            <p class="mt-2 mb-0">
                                Wordt bewust van uw waterverbruik tijdens het douchen.
                            </p>
                        </div>
                    </div>
                    <div class="col-12 col-md-4 mb-5 mb-md-0">
                        <div class="box">
                            <div class="icon">
                                <i class="fas fa-thermometer-three-quarters"></i>
                            </div>
                            <span>Temperatuur</span>
                            <p class="mt-2 mb-0">
                                Heb inzicht in het gas dat u verbruikt op basis van de temperatuur.
                            </p>
                        </div>
                    </div>
                    <div class="col-12 col-md-4">
                        <div class="box">
                            <div class="icon">
                                <i class="fas fa-seedling"></i>
                            </div>
                            <span>Duurzaam</span>
                            <p class="mt-2 mb-0">
                                Help het klimaat en bespaar geld door het juiste inzicht.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="product" class="product py-5">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-4 mb-4 mb-md-0">
                        <img src="assets/img/shower.png" class="img-fluid">
                    </div>
                    <div class="col-12 col-md-8">
                        <h2>Sustineri douchekop</h2>
                        <p>
                            Met deze douchekop kunt u uw water- en gas-verbruik bijhouden in een overzichtelijke app.
                            <br>
                            Zo wordt u bewust van uw gebuik en kunt u het klimaat helpen door minder of kouder te douchen.
                            <br>
                            Ook heeft u overzicht in de kosten voor het douche, zo kunt u hier ook op besparen.
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <div id="about" class="about">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <div class="image">
                            <!--<img src="assets/img/header_bg.jpg">-->
                        </div>
                    </div>
                    <div class="col-12 col-md-6">
                        <div class="text">
                            <h2>Over ons</h2>
                            <p class="mb-0">
                                Wij zijn Projectgroep 21 bestaand uit:
                                <br>
                                <br>
                                Tijn van Rouwendaal,
                                <br>
                                Jarno Weemen,
                                <br>
                                Cleo van den Heuvel,
                                <br>
                                Sven van Rijen,
                                <br>
                                Jordy Soer en
                                <br>
                                Cas Heynen
                            </p>
                            <!--<a class="btn btn-primary" href="/about.php">
                                Lees meer
                            </a>-->
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="faq" class="faq py-5">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h2>FAQ</h2>
                        <p>
                            <strong>Kan dit in mijn bestaande douche worden ingebouwd?</strong>
                            <br>
                            De douche kan grotendeels blijven. Er is alleen een nieuwe douchekop nodig.
                        </p>
                        <p>
                            <strong>Moet ik mijn telefoon bij me hebben als ik douche?</strong>
                            <br>
                            Nee, dat is niet nodig. In de app is na de douchebeurt is de informatie nog steeds beschikbaar.
                        </p>
                        <p>
                            <strong>Hoe weet ik hoelang ik bezig ben?</strong>
                            <br>
                            Aan de kleur van het lampje is de zien hoelang je bezig bent, de kleur gaat van groen (goed) naar rood (te lang).
                        </p>
                        <p>
                            <strong>Kan ik ook zien hoeveel een douchebeurt kost?</strong>
                            <br>
                            Ja, in de app zijn ook de kosten zichtbaar.
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <?php include "partials/footer.php"; ?>
        <?php include "partials/default/scripts.php"; ?>
    </body>
</html>