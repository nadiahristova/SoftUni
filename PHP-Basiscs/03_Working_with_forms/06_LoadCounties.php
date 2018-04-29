<?php
$countiesOfTheWorld = [];
$countriesFromText = file_get_contents('countries.txt','r');
preg_match('/<AFRICA>(.*?)<AFRICA>/',$countriesFromText,$AfricaC);
preg_match('/<ASIA>(.*?)<ASIA>/',$countriesFromText,$AsiaC);
preg_match('/<EUROPE>(.*?)<EUROPE>/',$countriesFromText,$EuropeC);
preg_match('/<N\. AMERICA>(.*?)<N\. AMERICA>/',$countriesFromText,$NAmericaC);
preg_match('/<OCEANIA\/AUSTRALIA>(.*?)<OCEANIA\/AUSTRALIA>/',$countriesFromText,$AustraliaC);
preg_match('/<S\. AMERICA>(.*?)<S\. AMERICA>/',$countriesFromText,$SAmericaC);
preg_match('/<ANTARCTICA>(.*?)<ANTARCTICA>/',$countriesFromText,$AntarcticaC);

$AfricaC=explode(',',$AfricaC[1]);
$AsiaC=explode(',',$AsiaC[1]);
$EuropeC=explode(',',$EuropeC[1]);
$NAmericaC=explode(',',$NAmericaC[1]);
$AustraliaC=explode(',',$AustraliaC[1]);
$SAmericaC=explode(',',$SAmericaC[1]);
$AntarcticaC=explode(',',$AntarcticaC[1]);

$countiesOfTheWorld['AFRICA'] = $AsiaC;
$countiesOfTheWorld['ASIA'] = $AfricaC;
$countiesOfTheWorld['EUROPE'] = $EuropeC;
$countiesOfTheWorld['N. AMERICA'] = $NAmericaC;
$countiesOfTheWorld['AUSTRALIA'] = $AustraliaC;
$countiesOfTheWorld['S. AMERICA'] = $SAmericaC;
$countiesOfTheWorld['ANTARCTICA'] = $AntarcticaC;
?>