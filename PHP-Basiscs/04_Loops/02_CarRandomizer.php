<!DOCTYPE html>
<html>
<head>
    <title>Cars</title>
</head>

<body>
<form action="" method="post">
    <label for="cars">Enter cars: </label>
    <input type="text" id="cars" name="cars" />
    <input type="submit" name="submit" value="Show result"/><br /><br />
</form>
<?php
header('Content-Type: text/html; charset=utf8;');
$funnyColors=array('SARCOLINE', 'COQUELICOT', 'SMARAGDINE', 'MIKADO', 'GLAUCOUS',
    'WENGE', 'FULVOUS', 'XANADU', 'FALU' , 'EBURNEAN', 'AMARANTH');
if(isset($_POST['submit'])){
    if(isset($_POST['cars'])){
        $arrOFCars = explode(', ',$_POST['cars']);?>
        <table border="1">
            <tr>
                <th>Car</th><th>Color</th><th>Count</th>
            </tr>
        <?php
        for($car=0; $car<sizeof($arrOFCars); $car++){
            $color=array_rand($funnyColors,1);
            echo "<tr><td>".htmlentities($arrOFCars[$car])."</td><td>".
                $funnyColors[$color]."</td><td>".rand(1,5)."</td></tr>";
        }?>
        </table>
    <?php
    }
} ?>

</body>
</html>
