<link rel="stylesheet" href="css/07_Style.css" type="text/css">
<form action="07_FormData.php" method="get">
    <input type="text" name="name"/>
    <input type="text" name="age"/>
    <input type="radio" name="gender" value="female" id="female"/>
    <label for="female">Female</label>
    <input type="radio" name="gender" value="male" id="male" checked/>
    <label for="male">Male</label>
    <input type="submit" name="submit"/>
</form>
<?php if(isset($_GET['submit'])){?>
    <div>
    <br />
    <p>My name is <?=htmlentities($_GET['name'])?>. I am <?=htmlentities($_GET['age'])?> years old. I am <?=htmlentities($_GET['gender'])?>.</p>
    </div>
<?php } ?>