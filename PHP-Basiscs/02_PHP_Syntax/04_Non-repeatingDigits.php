<?php $n = 247;
$n=(int)$n;
$output =[];
for($i=102; $i<=$n && $i<=987; $i++){
    $strI = (string)$i;
    $length = strlen($strI);
    $numArr=[];
    for($j=0; $j<$length; $j++){
        $char = $strI{$j};
        array_push($numArr, $char);
    }
    $numArr=array_unique($numArr);

    if(count($numArr)==$length){
        array_push($output, $strI);
    }
}
if(count($output)==0){
    echo('no');
} else {
    echo(join(', ',$output));
}
?>