<?php
$config['default_controller'] = 'Home';
$config['default_method'] = 'Index';
//$config['namespaces']['PFCS\ConferenceShedular'] = 'C:\Users\peter\Dropbox\1.SoftUni\Petar\Level 3\WDB\Exs\PersonalProject\shoppingCart';
$config['defaultControllerNamespace'] = 'PFCS\ConferenceShedular\Controllers';

//$config['session']['autostart'] = true;
//$config['session']['type'] = 'native';
//$config['session']['name'] = '_sess';
//$config['session']['lifetime'] = 3600;
//$config['session']['path'] = '/';
//$config['session']['domain'] = '';
//$config['session']['secure'] = false;
//$config['session']['dbConnection'] = 'default';
//$config['session']['dbTable'] = 'sessions';

$config['displayExceptions'] = true;
$config['namespaces']['PFCS\ConferenceShedular'] = '../../conferenceShedular';

return $config;