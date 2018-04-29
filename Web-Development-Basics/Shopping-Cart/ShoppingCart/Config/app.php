<?php

$conf['default_controller'] = 'Index';
$conf['default_method'] = 'index2';
$conf['namespaces']['Controllers'] = '../Controllers';

$cnf['session']['auto_start'] = true;
$cnf['session']['type'] = 'native';
$cnf['session']['name'] = 'app_session';
$cnf['session']['lifetime'] = 60 * 60 * 15;
$cnf['session']['path'] = '/';
$cnf['session']['domain'] = '';
$cnf['session']['secure'] = false;

return $conf;