<?php
    $current_date = date("d-m-Y");

    // Get current week dates
    $monday = strtotime("last monday");
    $monday = date('w', $monday)==date('w') ? $monday + 7 * 86400 : $monday;
    $sunday = strtotime(date("d-m-Y", $monday)." +6 days");
    $this_week_start = date("d-m-Y", $monday);
    $this_week_end = date("d-m-Y", $sunday);

    $mon = $this_week_start;
    $tue = date("d-m-Y", strtotime(date("d-m-Y", $monday)." + 1 day"));
    $wed = date("d-m-Y", strtotime(date("d-m-Y", $monday)." + 2 day"));
    $thu = date("d-m-Y", strtotime(date("d-m-Y", $monday)." + 3 day"));
    $fri = date("d-m-Y", strtotime(date("d-m-Y", $monday)." + 4 day"));
    $sat = date("d-m-Y", strtotime(date("d-m-Y", $monday)." + 5 day"));
    $sun = $this_week_end;

    // Water mon
    $url_get_water_mon = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_mon = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $mon
    );
    $data_string_get_water_mon = json_encode($data_get_water_mon);

    // use key 'http' even if you send the request to https://...
    $options_get_water_mon = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_mon

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_mon  = stream_context_create($options_get_water_mon);
    $result_get_water_mon = file_get_contents($url_get_water_mon, false, $context_get_water_mon);
    if ($result_get_water_mon === FALSE) {

    }

    $result_decode_get_water_mon = json_decode($result_get_water_mon, true);

    // Water tue
    $url_get_water_tue = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_tue = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $tue
    );
    $data_string_get_water_tue = json_encode($data_get_water_tue);

    // use key 'http' even if you send the request to https://...
    $options_get_water_tue = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_tue

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_tue  = stream_context_create($options_get_water_tue);
    $result_get_water_tue = file_get_contents($url_get_water_tue, false, $context_get_water_tue);
    if ($result_get_water_tue === FALSE) {

    }

    $result_decode_get_water_tue = json_decode($result_get_water_tue, true);

    // Water wed
    $url_get_water_wed = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_wed = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $wed
    );
    $data_string_get_water_wed = json_encode($data_get_water_wed);

    // use key 'http' even if you send the request to https://...
    $options_get_water_wed = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_wed

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_wed  = stream_context_create($options_get_water_wed);
    $result_get_water_wed = file_get_contents($url_get_water_wed, false, $context_get_water_wed);
    if ($result_get_water_wed === FALSE) {

    }

    $result_decode_get_water_wed = json_decode($result_get_water_wed, true);

    // Water thu
    $url_get_water_thu = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_thu = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $thu
    );
    $data_string_get_water_thu = json_encode($data_get_water_thu);

    // use key 'http' even if you send the request to https://...
    $options_get_water_thu = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_thu

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_thu  = stream_context_create($options_get_water_thu);
    $result_get_water_thu = file_get_contents($url_get_water_thu, false, $context_get_water_thu);
    if ($result_get_water_thu === FALSE) {

    }

    $result_decode_get_water_thu = json_decode($result_get_water_thu, true);

    // Water fri
    $url_get_water_fri = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_fri = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $fri
    );
    $data_string_get_water_fri = json_encode($data_get_water_fri);

    // use key 'http' even if you send the request to https://...
    $options_get_water_fri = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_fri

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_fri  = stream_context_create($options_get_water_fri);
    $result_get_water_fri = file_get_contents($url_get_water_fri, false, $context_get_water_fri);
    if ($result_get_water_fri === FALSE) {

    }

    $result_decode_get_water_fri = json_decode($result_get_water_fri, true);

    // Water sat
    $url_get_water_sat = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_sat = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $sat
    );
    $data_string_get_water_sat = json_encode($data_get_water_sat);

    // use key 'http' even if you send the request to https://...
    $options_get_water_sat = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_sat

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_sat  = stream_context_create($options_get_water_sat);
    $result_get_water_sat = file_get_contents($url_get_water_sat, false, $context_get_water_sat);
    if ($result_get_water_sat === FALSE) {

    }

    $result_decode_get_water_sat = json_decode($result_get_water_sat, true);

    // Water sun
    $url_get_water_sun = 'https://127.0.0.1/sustineri_api/api/measurement/get_week.php';
    $data_get_water_sun = array(
        'JWT_Token' => $_COOKIE['jwt'],
        'User_ID' => $_COOKIE['logged_in'],
        'Sensor_ID' => '1',
        'DateWeek' => $sun
    );
    $data_string_get_water_sun = json_encode($data_get_water_sun);

    // use key 'http' even if you send the request to https://...
    $options_get_water_sun = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string_get_water_sun

        ),
        'ssl'=>array(
            'verify_peer' => false,
            'verify_peer_name' => false
        )
    );
    $context_get_water_sun  = stream_context_create($options_get_water_sun);
    $result_get_water_sun = file_get_contents($url_get_water_sun, false, $context_get_water_sun);
    if ($result_get_water_sun === FALSE) {

    }

    $result_decode_get_water_sun = json_decode($result_get_water_sun, true);

    if ($mon == $current_date) {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>]
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                }
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    } elseif ($tue == $current_date) {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>, <?= $result_decode_get_water_tue['Value'] ?>]
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag', 'Dinsdag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                }
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    } elseif ($wed == $current_date) {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>, <?= $result_decode_get_water_tue['Value'] ?>, <?= $result_decode_get_water_wed['Value'] ?>]
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag', 'Dinsdag', 'Woensdag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                }
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    } elseif ($thu == $current_date) {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>, <?= $result_decode_get_water_tue['Value'] ?>, <?= $result_decode_get_water_wed['Value'] ?>, <?= $result_decode_get_water_thu['Value'] ?>]
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag', 'Dinsdag', 'Woensdag', 'Donderdag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                }
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    } elseif ($fri == $current_date) {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>, <?= $result_decode_get_water_tue['Value'] ?>, <?= $result_decode_get_water_wed['Value'] ?>, <?= $result_decode_get_water_thu['Value'] ?>, <?= $result_decode_get_water_fri['Value'] ?>]
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag', 'Dinsdag', 'Woensdag', 'Donderdag', 'Vrijdag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                }
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    } elseif ($sat == $current_date) {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>, <?= $result_decode_get_water_tue['Value'] ?>, <?= $result_decode_get_water_wed['Value'] ?>, <?= $result_decode_get_water_thu['Value'] ?>, <?= $result_decode_get_water_fri['Value'] ?>, <?= $result_decode_get_water_sat['Value'] ?>]
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag', 'Dinsdag', 'Woensdag', 'Donderdag', 'Vrijdag', 'Zaterdag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                }
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    } else {
?>

    <script>
        var options = {
            series: [{
                name: 'Water',
                color: '#068ec8',
                data: [<?= $result_decode_get_water_mon['Value'] ?>, <?= $result_decode_get_water_tue['Value'] ?>, <?= $result_decode_get_water_wed['Value'] ?>, <?= $result_decode_get_water_thu['Value'] ?>, <?= $result_decode_get_water_fri['Value'] ?>, <?= $result_decode_get_water_sat['Value'] ?>, <?= $result_decode_get_water_sun['Value'] ?>],
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '55%',
                    endingShape: 'rounded'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: ['Maandag', 'Dinsdag', 'Woensdag', 'Donderdag', 'Vrijdag', 'Zaterdag', 'Zondag'],
            },
            yaxis: {
                title: {
                    text: 'Liter'
                },
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                y: {
                    formatter: function (val) {
                        return val + " liter"
                    }
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    </script>

<?php
    }
?>