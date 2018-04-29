<html>
<head>
    <title>Colored text</title>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }
        aside{
            margin:0 auto;
            margin-bottom: 20px;
            background: -moz-linear-gradient(bottom,  #1e5799 0%, #2989d8 50%, #207cca 51%, #7db9e8 100%); /* FF3.6+ */
            background: -webkit-linear-gradient(bottom,  #1e5799 0%,#2989d8 50%,#207cca 51%,#7db9e8 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(bottom,  #1e5799 0%,#2989d8 50%,#207cca 51%,#7db9e8 100%); /* Opera 11.10+ */
            border-radius: 10px;
            width:160px;
            padding:10px;
        }
        aside > header{
            font-weight: bold;
            font-size: 1.4em;
        }

        ul {
            margin-left:10px;
        }

        ul > li{
            list-style-type: circle;
            list-style-position: inside;
        }
        input {
            margin-top: 5px;
            display:inline-block;
        }
        input[type="submit"]{
            padding:3px;
            border-radius: 5px;
            float:right;
        }
        form{
            margin:0 auto;
            width:15%;
            margin-bottom: 20px;
            overflow:hidden;
        }
    </style>
</head>

<body>
<form>
    <label>Categories: </label><input type="text" name="categories"/><br />
    Tags: <input type="text" name="tags"/><br />
    Months: <input type="text" name="months"/><br />
    <input type="submit" name="submit" value="Generate" />
</form>
<?php
if(isset($_GET['submit'])) {
    $categories = explode(', ', $_GET['categories']);
    $tags = explode(', ', $_GET['tags']);
    $months = explode(', ', $_GET['months']);
    $aside = [$categories,$tags,$months];
    $titles=['Categories', 'Tags', 'Months'];
    for($index=0; $index<count($titles); $index++):?>
<aside>
    <header><?=$titles[$index]?></header>
    <hr />
    <br />
    <ul>
        <?php foreach($aside[$index] as $list){
            echo "<li><a href='#'>$list</a></li>";
        }?>
    </ul>
</aside>
<?php endfor;
}?>
</body>
</html>
