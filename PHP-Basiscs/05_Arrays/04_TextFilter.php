<form>
    <input type="text" name="text"/><br />
    <input type="text" name="banlist" /><br />
    <input type="submit" name="submit" value="Go" />
</form>
<?php
if(isset($_GET['submit'])){
    $text= $_GET['text'];
    $banlist = explode(', ', $_GET['banlist']);
    foreach($banlist as $word){
        $len = strlen($word);
        $censored=str_repeat('*', $len);
        $text = str_replace($word,$censored,$text);
    }
    echo "<p>".$text."</p>";
}
