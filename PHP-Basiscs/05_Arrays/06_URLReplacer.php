<form>
    <textarea name="text"></textarea><br />
    <input type="submit" name="submit" value="Replace"/>
</form>
<?php
if(isset($_GET['submit'])){
    $text = $_GET['text'];
    $text = str_replace("</a>", "[/URL]", $text);
    preg_match_all("/<a href=\"(.*?)\">/",$text,$matches);
    for($i = 0; $i<count($matches[1]); $i++){
        $text = str_replace('<a href="'.$matches[1][$i].'">', '[URL='.$matches[1][$i].']', $text);
    }
    echo $text;
}