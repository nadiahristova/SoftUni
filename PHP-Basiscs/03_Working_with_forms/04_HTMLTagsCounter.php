<form method="post" action="">
    Enter HTML Tags:
    <br />
    <input type="text" name="tag" />
    <input type="submit" value="Submit" />
</form>
<?php
session_start();
if(!isset($_SESSION['count'])){
    $_SESSION['count'] = 0;
}
//$tagsInHTML="!--...--, a, abbr, acronym, address, applet, area, article, aside, audio, b, base, basefont, bdi,
//bdo, big, blockquote, body, button, canvas, caption, center, cite, code, col, colgroup, datalist, dd, del, details, dfn, dialog, dir, div, dl, dt, em, embed, fieldset, figcaption, figure, font, footer, form, frame, frameset, h1, h6, h2, h3, h4, h5, head, header, hr, html, i, iframe, img, input, ins, kbd, keygen, label, legend, li, link, main, map, mark, menu, menuitem, meta, meter, nav, noframes, noscript, object, ol, optgroup, option, output, p, param, pre, progress, q, rp, rt, ruby, s, samp, script, section, select, small, source, span, strike, strong, style, sub, summary, sup, table, tbody, td, textarea, tfoot, th, thead, time, title, tr, track, tt, u, ul, var, video, wbr";
//$validTags=explode(', ',$tagsInHTML); -> variant 1
$tagsInText = file_get_contents('tags.txt','r'); //  --> variant 2
preg_match_all('/<(.*?)>/',$tagsInText,$tagsInHTML);
$validTags = array_unique($tagsInHTML[1]);
array_push($validTags,'h2','h3','h4','h5');
$validTag=false;
if(isset($_POST['tag'])){
    $tagInQuestion = $_POST['tag'];
    if(in_array($tagInQuestion,$validTags)){
        $validTag = true;
        $_SESSION['count'] += 1;
    }else {
        $validTag = false;
    }?>
    <h2>
        <?php echo $validTag ?  "Valid HTML Tag!" : "Invalid HTML Tag!"; ?> <br />
        Score: <?=$_SESSION['count'];?>
    </h2>
<?php }
?>