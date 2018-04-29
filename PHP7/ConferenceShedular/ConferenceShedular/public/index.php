<?php
error_reporting(E_ALL ^ E_NOTICE);

require '../../FMK/App.php';

$app = \PFCS\FMK\App::getInstance();
$app->run();