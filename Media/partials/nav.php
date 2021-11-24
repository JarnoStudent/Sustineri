<nav>
    <?php
        $url = basename($_SERVER["REQUEST_URI"]);

        $self = $_SERVER["PHP_SELF"];
    ?>
    <div class="nav">
        <div class="container">
            <div class="row">
                <div class="col-6 col-sm-5 col-md-3 col-lg-2">
                    <a id="logo_">
                        <img src="assets/img/sustineri_logo_s.png">
                    </a>
                </div>
                <div class="col-6 col-sm-7 col-md-9 col-lg-10 position-relative">
                    <ul class="d-none d-lg-block">
                        <li id="home_"> <!--<?php if ($url === "" || $url === "index.php") { ?>class="current"<?php } ?>-->
                            <div class="border-bottom-nav"></div>
                            <a>
                                <span>Home</span>
                            </a>
                        </li>
                        <li id="product_">
                            <div class="border-bottom-nav"></div>
                            <a>
                                <span>Product</span>
                            </a>
                        </li>
                        <li id="about_">
                            <div class="border-bottom-nav"></div>
                            <a>
                                <span>Over ons</span>
                            </a>
                        </li>
                        <li id="faq_">
                            <div class="border-bottom-nav"></div>
                            <a>
                                <span>FAQ</span>
                            </a>
                        </li>
                    </ul>
                    <div class="v-align">
                        <div class="burger burger-rotate float-right d-block d-lg-none">
                            <div class="burger-lines"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="nav-mobile d-block d-lg-none">
        <ul>
            <li id="home_m">
                <a>
                    <span>Home</span>
                </a>
            </li>
            <li id="product_m">
                <a>
                    <span>Product</span>
                </a>
            </li>
            <li id="about_m">
                <a>
                    <span>Over ons</span>
                </a>
            </li>
            <li id="faq_m">
                <a>
                    <span>FAQ</span>
                </a>
            </li>
        </ul>
    </div>
</nav>