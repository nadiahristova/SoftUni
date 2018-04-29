<!DOCTYPE html>
<html>
    <head>
        <title>A Table</title>
    </head>

    <body>
    <table border="1">
        <tr>
            <th>Number</th><th>Square</th>
        </tr>
        <?php
            header('Content-Type: text/html; charset=utf8;');
            for($num = 0; $num<=100; $num++):
            if($num%2 == 0):?>
            <tr>
                <td><?=$num?></td><td><?=round(pow($num,0.5),2)?></td>
            </tr>
        <?php endif;
        endfor;?>
    </table>
    </body>
</html>
