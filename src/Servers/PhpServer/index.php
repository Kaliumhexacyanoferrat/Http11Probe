<?php
if ($_SERVER['REQUEST_URI'] === '/echo') {
    header('Content-Type: text/plain');
    foreach (getallheaders() as $name => $value) {
        echo "$name: $value\n";
    }
    exit;
}

header('Content-Type: text/plain');
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    echo file_get_contents('php://input');
} else {
    echo 'OK';
}
