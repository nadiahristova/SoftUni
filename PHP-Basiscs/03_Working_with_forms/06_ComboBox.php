<?php
session_start();
include('06_LoadCounties.php');
?>
<!DOCTYPE html>
<html>
    <head>
        <title>Combo Box</title>
    </head>

    <body>
    <form action="" method="get" id="form">
        <select name="continents" id="selectBox" required onfocus onchange="document.getElementById('form').submit()" >
            <?=$_SESSION['selected']?>
            <?php if (!$_SESSION['selected'] || !isset($_GET['continents'])): ?>
            <option value="" disabled>- Select Continent -</option>
            <?php $keys=array_keys($countiesOfTheWorld);
            foreach($keys as $key):?>
            <option value="<?=$key?>"><?=$key?></option>
            <?php endforeach;
            $_SESSION['selected'] = true;
            else:
            $continent = $_GET['continents'];
            $_SESSION['selected']=false;
            ?>
            <option disabled><?= $continent ?></option>
            <?php endif; ?>
        </select>

        <select name="countries" id="country" onchange="document.getElementById('form').submit()">
            <option value="" selected disabled>- Select Country -</option>
            <?php
            if (!empty($_GET) && isset($_GET['continents'])) {
                $continent = $_GET['continents'];
                $_SESSION['continents'] = $continent;
                $countries = $countiesOfTheWorld[$continent];
                foreach ($countries as $country):?>
                    <option value='<?=$country?>'><?=htmlspecialchars($country)?></option>
            <?php endforeach;
            }
            ?>
        </select>
    </form>
    <?php
    if (isset($_GET['countries'])) {
        $continent = $_SESSION['continents'];
        $country = $_GET['countries'];
        echo "You selected the country  ".htmlspecialchars($country)."  from the continent  ".htmlspecialchars
            ($continent).".";
    }
    ?>
    </body>
</html>