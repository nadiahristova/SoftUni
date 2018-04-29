<?php
$month = intval(date('n'));
$year = intval(date('Y'));
$days = cal_days_in_month(CAL_GREGORIAN,$month, $year);
$foundFirstSun = false;
for($d=1 ; $d<=$days; $d++){
    if(!$foundFirstSun){
        $dayOfWeek = date('D', mktime(0,0,0,$month,$d,$year));
        if($dayOfWeek == 'Sun'){
            $foundFirstSun=true;
            $d--;
        }
    }else {
        echo date('jS F, Y', mktime(0,0,0,$month,$d,$year))."\n";
        $d+=6;
    }
}
?>