<?php
$value = (object)[2,34];
if (is_numeric($value)):?>
    <p><?= var_dump($value);?></p>
<?php else: ?>
    <p><?=gettype($value)?></p>
<?php endif; ?>