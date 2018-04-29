<!DOCTYPE html>
<html>
<head>
    <title>Modify</title>
</head>

<body>
<form action="" method="post">
    <input type="text" name="str" />
    <input type="radio" id="palindome" name="radios" value="0" /><label for="palindome">Check Palindrome</label>
    <input type="radio" id="reverse" name="radios" value="1" /><label for="reverse">Reverse String</label>
    <input type="radio" id="split" name="radios" value="2" /><label for="split">Split</label>
    <input type="radio" id="hash" name="radios" value="3" /><label for="hash">Hash String</label>
    <input type="radio" id="shuffle" name="radios" value="4" /><label for="shuffle">Shuffle String</label>
    <input type="submit" name="submit" value="Submit"/><br />
</form>
<?php
header('Content-Type: text/html; charset=utf8;');

function isPalindrome($str){
    $revStr = strrev($str);
    if(strtolower($str) == strtolower($revStr)){
        return true;
    }else return false;
}

function splitStr($str){
    $joinedStr = explode(' ',$str); //we remove the empty spaces
    $joinedStr = implode('', $joinedStr);//we join the words into 1 string without space
    $arrChars = str_split($joinedStr);//we obtain an array of the separate letters of the joined string
    return implode(' ', $arrChars);//we join the letters with s space between them
}

function shuffleStr($str){
    $arrChars = str_split($str);
    shuffle($arrChars);
    $shuffledStr=implode('',$arrChars);
    return $shuffledStr;
}

if(isset($_POST['submit'])){
    if(isset($_POST['radios']) && isset($_POST['str'])){
        $case = $_POST['radios'];
        $str = $_POST['str'];
        switch($case){
            case "0": echo isPalindrome($str) ?  $str." is a palindrome!" :  $str." is not a palindrome!"; break;
            case '1': echo strrev($str); break;
            case '2': echo splitStr($str); break;
            case '3': echo crypt($str, strrev($str)); break;
            case '4': echo shuffleStr($str); break;
        }
    } else echo "Invalid entry. Please, fill in the input text field and choose an option.";
} ?>

</body>
</html>