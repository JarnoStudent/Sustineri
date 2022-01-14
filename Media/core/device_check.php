<?php
    $url = 'https://127.0.0.1/sustineri_api/api/devices/check_website.php';
    $data = array('Device_Pass' => 'token');
    $data_string = json_encode($data);

    // use key 'http' even if you send the request to https://...
    $options = array(
        'http' => array(
            'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
            'method'  => 'POST',
            'content' => $data_string
            /*'verify_peer' => false,
            'verify_peer_name' => false*/
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

    //$_COOKIE
    setcookie('jwt', $result_decode['jwt'], time() +3600);
?>
