<html>
    <head>
        <title>SoftUni Seminar Generator</title>
        <link href="css/07_Style.css" rel="stylesheet" type="text/css" />
        <script>
            var mousePosX = 0;
            var mousePosY = 0;
            function displayDiv(el){
                document.addEventListener('mousemove', function(e) {
                    e = e || window.event;
                    mousePosX = e.clientX+15;
                    mousePosY = e.clientY+15;
                });
                el.style.left = mousePosX + "px";
                el.style.top = mousePosY + "px";
            }
        </script>
    </head>

    <body>
        <form>
            <textarea name="text"></textarea><br />
            <label for="sort">Sort By:</label>
            <select id="sort" name="sortCrit">
                <option value="name">Name</option>
                <option value="date">Date</option>
            </select>
            <label for="order">Order By:</label>
            <select id="order" name="order">
                <option value="desc">Descending</option>
                <option value="asc">Ascending</option>
            </select>
            <input type="submit" name="submit" value="Replace"/>
        </form>
        <?php
        if(isset($_GET['submit']) && !empty($_GET['text'])){
            $text = $_GET['text'];
            $segments = preg_split('/\n/', $text, -1, PREG_SPLIT_NO_EMPTY);
            $info = [];

            class Seminar {
                public $seminarName;
                public $lector;
                public $date;
                public $additionalInfo;

                function __construct($seminarName,$lector,$date,$additionalInfo){
                    $this->seminarName=$seminarName;
                    $this->lector=$lector;
                    $this->date=$date;
                    $this->additionalInfo=$additionalInfo;
                }
            }

            foreach($segments as $segment){
                preg_match('/^(.*?)-/',$segment,$seminar);
                $info[] = new Seminar($seminar[1],'','','');
            }

            for($i=0; $i<count($segments); $i++){
                preg_match('/-(.*?)-/',$segments[$i],$lector);
                $semi = $info[$i];
                $semi->lector = $lector[1];
                $info[$i] = $semi;
            }

            for($i=0; $i<count($segments); $i++){
                preg_match('/-([0-9]{2}-[0-9]{2}-[0-9]{4}\s+[0-9]{2}:[0-9]{2})/',$segments[$i],$date);
                $semi = $info[$i];
                $semi->date = $date[1];
                $info[$i] = $semi;
            }

            for($i=0; $i<count($segments); $i++){
                $addiInfo = str_replace($info[$i]->seminarName."-".$info[$i]->lector."-".$info[$i]->date." ", "",
                    $segments[$i]);
                $semi = $info[$i];
                $semi->additionalInfo = $addiInfo;
                $info[$i] = $semi;
            }

            if($_GET['sortCrit'] == 'name'){
               usort($info, function($name1, $name2){
                   return ($name1->seminarName == $name2->seminarName) ? 0 :  ($name1->seminarName >
                       $name2->seminarName) ? 1 : -1;
               });
            } else {
                usort($info, function($date1, $date2){
                    return strtotime($date1->date) - strtotime($date2->date);
                });
            }

            if($_GET['order'] == 'desc'){
                $info = array_reverse($info);
            }

        for($i=0; $i<count($info); $i++){
            echo '<div class="hidden" id="semi'.$i.'"><p><b>Lecturer: </b> '.$info[$i]->lector.'</p><p><b>Details:
            </b> '.$info[$i]->additionalInfo.'</p></div>';
        }
            ?>

            <table cellspacing="1">
                <tr>
                    <th>Seminars</th>
                    <th>Date</th>
                    <th>Participate</th>
                </tr>
            <?php for($i=0; $i<count($info); $i++): ?>
                <tr>
                    <td class="name"><a href="#" onmouseover="document.getElementById('semi<?=$i?>').style.display =
                            'block';"
                           onmousemove="displayDiv(document.getElementById('semi<?=$i?>'));"
                           onmouseout="document.getElementById('semi<?=$i?>').style.display = 'none';
                           "><?=$info[$i]->seminarName?></a></td>
                    <td><?=$info[$i]->date?></td>
                    <td><button type="button">SIGN UP</button></td>
                </tr>
            <?php endfor; ?>
        <?php }?>
    </body>
</html>