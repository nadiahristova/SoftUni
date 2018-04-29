<html>
<head>
    <title>Colored text</title>
    <style>
        .red{
            color: red;
        }
        .blue{
            color: blue;
        }
    </style>
</head>

<body>
<form>
    <input type="text" name="txt"/>
    <input type="submit" name="submit" value="Color" />
</form>
<?php
if(isset($_GET['submit'])) {
    $text = $_GET['txt'];
    $text = str_replace(' ', '', $text);
    $letters = str_split($text);
    foreach ($letters as $letter) {
        if (ord($letter) % 2 == 0) {
            $letter = "<span class='red'>" . $letter . "</span>";
        } else  $letter = "<span class='blue'>" . $letter . "</span>";
        $coloredLetters[] = $letter;
    }
    $text = implode(' ', $coloredLetters);
    echo $text;
}?>
</body>
</html>