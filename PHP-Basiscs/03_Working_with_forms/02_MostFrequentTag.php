<form action="" method="post">
    Enter Tags: <br />
    <br />
    <input type="text" name="tags"/>
    <input type="submit" value="Submit">
</form>

<br />

<?php
if(isset($_POST['tags'])){
    $tags = explode(', ', $_POST['tags']);
    $output = [];
    foreach($tags as $tag){
        if(array_key_exists($tag, $output)){
            $output[$tag] += 1;
        } else {
            $output[$tag] = 1;
        }
    }
    arsort($output);
    reset($output);
    $first_key = key($output);
    foreach($output as $key=>$value){
        echo "$key : $value times <br />";
    }?>
    <br />
    Most frequent tag is : <?=$first_key?>
<?php
}
?>