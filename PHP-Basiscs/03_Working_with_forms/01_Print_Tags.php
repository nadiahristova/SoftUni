<form action="" method="post">
    Enter Tags:<br />
    <br />
    <input type="text" name="tags" />
    <input type="submit" value="Submit"/>
</form>
<br />

<?php
if(isset($_POST['tags'])){
    $tags = $_POST['tags'];
    $tagArr = explode( ', ', $tags);
    for($tag = 0; $tag<sizeof($tagArr); $tag++){
        echo "$tag : ".htmlentities($tagArr[$tag])." <br />\n";
    }
}
?>