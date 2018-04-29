<!DOCTYPE html>
<html>
<head>
    <title>Expenses</title>
</head>

<body>
<form action="" method="post">
    <label for="years">Enter number of years: </label>
    <input type="text" id="years" name="years" />
    <input type="submit" name="submit" value="Show costs"/><br /><br />
</form>
<?php
header('Content-Type: text/html; charset=utf8;');
$tableHeaders=array('Year', 'January', 'February', 'March', 'April',
    'May', 'June', 'July', 'August' , 'September', 'October', 'November', 'December', 'Total:');
$totalSum=0;
if(isset($_POST['submit'])){
    if(isset($_POST['years'])){
        $numOfYearsBack = $_POST['years'];
        $currYear=intval(date('Y'));

        ?>
        <table border="1">
            <tr>
                <?php for($th=0; $th<count($tableHeaders); $th++){
                    echo "<th>{$tableHeaders[$th]}</th>";
                }
                ?>
            </tr>
            <?php
            for($year=0; $year<$numOfYearsBack; $year++){
                $sum=0;
                echo "<tr><td>".($currYear-$year)."</td>";
                for($month=0; $month<12; $month++) {
                    $monthlyExp = rand(0, 999);
                    echo "<td>" . $monthlyExp . "</td>";
                    $sum+=$monthlyExp;
                    $totalSum +=$monthlyExp;
                }
                echo "<td>".$sum."</td></tr>";
            }?>
        </table>
        <h3>Total expenses for the period: <?=number_format($totalSum,0,'.', ' ')?></h3>
    <?php
    }
} ?>

</body>
</html>