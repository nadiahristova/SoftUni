<form action="" method="post">
    <p id="output"></p>
    Enter Amount
    <input type="text" name="amount" /> <br />
    <input type="radio" name="currency" id="usd" value="$" />
    <label for="usd">USD</label>
    <input type="radio" name="currency" id="eur" value="€" />
    <label for="eur">EUR</label>
    <input type="radio" name="currency" id="bg" value="lv" checked/>
    <label for="bg" >BGN</label> <br />
    Compound Interest Amount
    <input type="text" name="interest"><br />
    <select name="totalMonths">
        <option value="6">6 Months</option>
        <option value="12">1 Year</option>
        <option value="24">2 Years</option>
        <option value="60">5 Years</option>
    </select>
    <input type="submit" value="Calculate" name="submit" />
</form>


<?php
if (isset($_POST['submit'])){
    if(isset($_POST['amount']) && isset($_POST['interest']) && is_numeric($_POST['amount']) && is_numeric($_POST['interest'])){
        $currency = $_POST['currency']."  ";
        $money = $_POST['amount'];
        $monthlyPer = round($_POST['interest']/12,2);
        $months = $_POST['totalMonths'];
        for($month = 0; $month<$months; $month++){
            $money *=((100+$monthlyPer)/100);
        }
        $money = round($money,2);
        ?>
        <script>
           document.getElementById('output').innerText = <?php echo json_encode($currency.$money)?>;
        </script>
   <?php } else {
        echo "Please fill in correctly the required information and try again.";
    }
}

?>