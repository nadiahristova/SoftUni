<form>
    Text: <input type="text" name="text"/><br />
    Banlist: <input type="text" name="banlist" /><br />
    <input type="submit" name="submit" value="Go" />
</form>
<?php
if (isset($_GET['submit'])){
$text = $_GET['text'];
preg_match_all('/(.+?)[.,?!]/', $text, $text);
$blanks= explode(', ',$_GET['banlist']);
$output = [];
$print = false;
for($i=0; $i<count($text[1]); $i++){
    $words = preg_split('/\s+/',$text[1][$i],-1,PREG_SPLIT_NO_EMPTY);
    foreach ($blanks as $blank){
        if(in_array($blank,$words)){
            if(in_array($text[0][$i],$output) == false){
                $output[] = $text[0][$i];
                $print = true;
            } else $print = false;
            break;
        }  $print = false;
    }
    if($print){
        echo $text[0][$i]."<br />";
    }
}
}