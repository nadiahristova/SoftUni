<?php
$firstNumber = 2;
$secondNumber = 3;
//$sum = round($firstNumber + $secondNumber,2); - does not work for Integer numbers
//$sum = number_format($firstNumber + $secondNumber,2,'.','');
$sum = sprintf('%.2f',$firstNumber + $secondNumber);
?>
<p><?=$firstNumber?> + <?=$secondNumber?> = <?=$sum?></p>
