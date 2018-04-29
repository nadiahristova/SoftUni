<?php
$conf['default']['connection_uri'] = 'mysql:host=localhost;dbname=tododb';
$conf['default']['username'] = 'root';
$conf['default']['pass'] = '';
$conf['default']['pdo_options'][PDO::MYSQL_ATTR_INIT_COMMAND] = 'SET NAMES UTF8';
$ocnf['default']['pdo_options'][PDO::ATTR_ERRMODE] = PDO::ERRMODE_EXCEPTION;

$conf['session']['connection_url'] = 'mysql:host=localhost;dbname=session';
$conf['session']['username'] = 'root';
$conf['session']['pass'] = '';

return $conf;