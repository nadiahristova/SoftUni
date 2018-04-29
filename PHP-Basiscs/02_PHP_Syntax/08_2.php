<?php
$day = 1;
$weeks = 0;
$currWeek = 1;
$year = 2015;
$month = 1;
//$weekDays = [[], [], [], [], [], [], []];
$NameOFMonths = ['Януари', 'Февруари', 'Март', 'Април', 'Май', 'Юни', 'Юли', 'Август', 'Септември',
    'Октомври', 'Ноември', 'Декември'];
$enterMonth = true;

function determineWeeks(){
    global $month;
    global $year;
    global $weeks;
    $currDate = mktime(0,0,0,$month,1,$year);
    $date = date('w', $currDate);
    if ($date==0) {
        $date = 7;
    }
    $daysOfMonth = date('t', $currDate);
    $daysOfMonth = $daysOfMonth-(8-$date)-1;
    $weeks = 1+floor($daysOfMonth/7);
    $weeks = ($daysOfMonth%7) != 0 ? $weeks+1 : $weeks;
}

function returnCell($dayOfWeek){
    global $day;
    global $month;
    global $year;
    static $daysOfMonth;
    $daysOfMonth = date('t', mktime(0,0,0,$month,1,$year));
    $currDate = mktime(0,0,0,$month,$day,$year);
    $date = date('w', $currDate);
    if($date==0) {
        $date=7;
    }


    if(($day == 1)&&($date>$dayOfWeek)){
        $pastMonth = ($month - 1 == 0) ? 12 : ($month - 1);
        $pastYear = ($month - 1 == 0) ? ($year-1) : $year;
        $pastDay = date('t', mktime(0,0,0,$pastMonth,$day,$pastYear)) - ($date - $dayOfWeek)+1;
        return ($dayOfWeek==6 || $dayOfWeek == 7)? "<td class='out red'>{$pastDay}</td>" : "<td class='out'>{$pastDay}</td>";
    } elseif( ($daysOfMonth + 1 == $day) ){
        $nextDay = 1 + ($dayOfWeek - $date);
        return ($dayOfWeek==6 || $dayOfWeek == 7)? "<td class='out red'>{$nextDay}</td>" : "<td class='out'>{$nextDay}</td>";
    } else {
        $d = $day;
        $day++;
        return ($dayOfWeek==6 || $dayOfWeek == 7) ? "<td class='red'>{$d}</td>" : "<td >{$d}</td>";
    }
}
?>


<!DOCTYPE html>
<html>
    <head>
        <title>Календар за <?=$year?></title>
        <link href="css/08_Style.css" rel="stylesheet" type="text/css">
    </head>

    <body>
        <?php
            for($mon = 0; $mon <12; $mon++){
            determineWeeks();
        ?>

        <div class="monthDiv">
            <header class="monthHeader"><?=$NameOFMonths[$mon]?></header>
            <hr />
            <table>
                <tr>
                    <th>По</th>
                    <th>Вт</th>
                    <th>Ср</th>
                    <th>Чт</th>
                    <th>Пе</th>
                    <th class="red">Сб</th>
                    <th class="red">Не</th>
                </tr>

                <?php
                for($week = 0; $week < $weeks; $week++ ){
                echo ('<tr>');
                for($d = 1; $d<8; $d++){
                        echo(returnCell($d));
                    }
                }
                echo ('</tr>');
                $day = 1;
                $month++;
                }
                ?>

            </table>
        </div>

    </body>
</html>