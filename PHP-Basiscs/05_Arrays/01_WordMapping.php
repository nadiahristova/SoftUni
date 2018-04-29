<form>
    <input type="text" name="txt"/>
    <input type="submit" name="submit" value="Count words" />
</form>
<?php
if(isset($_GET['submit'])){
    $txt = $_GET['txt'];
    preg_match_all('/\w+/',$txt,$words);
    $wordsArr=array_map('strtolower',$words[0]);
    $countedWords=array_count_values($wordsArr);
    ?>
    <table border="1">
        <?php foreach($countedWords as $word => $encounters): ?>
        <tr>
            <td><?=$word?></td>
            <td><?=$encounters?></td>
        </tr>
        <?php endforeach; ?>
    </table>
<?php } ?>