<!DOCTYPE html>
<html>
<head>
    <title>Sum</title>
</head>

<body>
<form action="" method="post">
    <label for="str">Input string: </label>
    <input type="text" id="str" name="str" />
    <input type="submit" name="submit" value="Submit"/><br /><br />
</form>
<?php
header('Content-Type: text/html; charset=utf8;');

function returnSum($num){
    $sum=0;
    $length = strlen((string)$num);
    for($i=0; $i<$length; $i++){
        $sum+=$num%10;
        $num = floor($num/10);
    }
    return $sum;
}

if(isset($_POST['submit'])){
    if(isset($_POST['str'])){
        $arrOFStr = explode(', ', $_POST['str']);
        ?>
        <table border="1">
            <?php
            for($strIndex=0; $strIndex<sizeof($arrOFStr); $strIndex++){
                $num = $arrOFStr[$strIndex];
                echo "<tr><td>{$num}</td>";
                if(is_numeric($num) && !strpos($num, '.') && !strpos($num, ',')){
                    echo "<td>".returnSum($num)."</td></tr>";
                } else echo "<td>I cannot sum that</td></tr>";
            }?>
        </table>
    <?php
    }
} ?>

</body>
</html>