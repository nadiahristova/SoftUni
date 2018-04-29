<?php

error_reporting(E_ALL ^ E_NOTICE);
echo '<pre>';
include '../../FMK/App.php';

$app = \SoftUni\FMK\App::getInstance();
//$db = new \SoftUni\FMK\DB\SimpleDB();
//$a = $db->prepare('SELECT * from users')->execute()->fetchAllAssoc();
//var_dump($a);
$app->run();




