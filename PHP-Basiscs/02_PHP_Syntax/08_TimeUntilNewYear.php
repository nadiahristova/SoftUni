<?php
$year = intval(date('Y'));
$now = getdate();
$newYear = getdate(date(mktime(23, 59, 59, 12, 31, $year)));
$diffSecs = $newYear['0'] - $now['0'];
$secs = number_format($diffSecs,0,'.',' ');
$mins = number_format($diffSecs/60,0,'.',' ');
$hours = number_format($diffSecs/60/60,0,'.',' ');
$days = $newYear['yday'] - $now['yday'];
$diffSecs = $diffSecs - $days*(24*60*60);
$h = floor($diffSecs/(60*60));
$diffSecs = $diffSecs - $h*(60*60);
$m = floor($diffSecs/(60));
$s = $diffSecs - $m*(60);

echo "Hours until new year : ".$hours."\n";
echo "Minutes until new year : ".$mins."\n";
echo "Seconds until new year : ".$secs."\n";
echo "Days:Hours:Minutes:Seconds ".$days.':'.$h.':'.$m.':'.$s."\n";
?>