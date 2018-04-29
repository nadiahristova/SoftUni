<!DOCTYPE html>
<html>
<head>
    <title>Primes</title>
</head>

<body>
<form action="" method="post">
    <label for="start">Starting Index: </label>
    <input type="text" id="start" name="start" />
    <label for="end">End: </label>
    <input type="text" id="end" name="end" />
    <input type="submit" name="submit" value="Submit"/><br />
</form>
<?php
header('Content-Type: text/html; charset=utf8;');
if(isset($_POST['submit'])){
    if(isset($_POST['start']) && isset($_POST['end']) && is_numeric($_POST['start']) &&
        is_numeric($_POST['end']) &&  $_POST['start'] <= $_POST['end']){
        $start = (int)$_POST['start'];
        $end = (int)$_POST['end'];
        $numbers=[];

        function isPrime($num){
            $isPrime=true;
            if($num == 1 || $num == 2 || $num == 3){
                return $isPrime;
            }
            $maxNum=ceil(pow($num,0.5));
            for($n=2; $n<=$maxNum; $n++){
                if ($num%$n==0){
                    $isPrime = false;
                    break;
                }
            }
            return $isPrime;
        }

        for($curr = $start; $curr<=$end; $curr++){
            if(isPrime($curr)){
                $numbers[] = "<b>".(string)$curr."</b>";
            } else $numbers[] = (string)$curr;
        }

        echo implode(', ', $numbers);
    }
} ?>

</body>
</html>