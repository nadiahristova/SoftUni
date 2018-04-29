<!DOCTYPE html>
<html>
<head>
    <title>Student Sorting</title>
    <link href="css/07_Style.css" rel="stylesheet" type="text/css" />
</head>

<body>
<script>
    var nextId=0;
    function addStudent(){
        nextId++;
        var sDiv = document.createElement('div');
        var num = "div"+nextId;
        sDiv.setAttribute("id",num);
        sDiv.innerHTML="<input type='text' name='fName[]' id='fName'/>"+"<input type='text' name='lName[]' id='lName'/>"+
        "<input type='email' name='email[]' id='email'/>"+"<input type='number' name='score[]' id='score'/>"+
        "<button type='button' onclick="+"'removeStudent("+num+")" +
        "'"+">-</button>";
        document.getElementById('parent').appendChild(sDiv);
    }

    function removeStudent(str){
        document.getElementById('parent').removeChild(str);
    }
</script>
<form action="" method="post">
    <div id="header">
        <div id="inner">First Name:</div><div id="inner">Last Name:</div><div id="inner">Email:</div><div id="inner">Exam
            Score:</div>
    </div>
    <div id="parent"></div>
    <script>addStudent()</script>
    <button type="button" onclick="addStudent()">+</button>
    <label for="sortCriteria">Sort by:</label>
    <select id="sortCriteria" name="sortCriteria">
        <option value="fName">First name</option>
        <option value="lName">Last name</option>
        <option value="mail">Email</option>
        <option value="exam" selected>Exam score</option>
    </select>
    <label for="order">Order:</label>
    <select id="order" name="order">
        <option value="ascending">Ascending</option>
        <option value="descending" selected>Descending</option>
    </select>
    <input type="submit" name="submit" value="SUBMIT"/><br /><br />
</form><br /><br />
<?php
header('Content-Type: text/html; charset=utf8;');
class Student
{
    public $firstName;
    public $lastName;
    public $eamil;
    public $score;

    function __construct($fName, $lName, $email, $score){
        $this->firstName=$fName;
        $this->lastName=$lName;
        $this->email=$email;
        $this->score=$score;
    }
}

if(isset($_POST['submit'])){
    if(isset($_POST['fName']) && isset($_POST['fName']) && isset($_POST['email']) && isset($_POST['score'])){
        $students = [];
        $fName = $_POST['fName'];
        $lName = $_POST['lName'];
        $email = $_POST['email'];
        $score = $_POST['score'];
        $avg=0;

        for($i=0; $i<count($_POST['fName']); $i++){
            $students[]=new Student($fName[$i], $lName[$i], $email[$i], (float)$score[$i]);
            $avg+=(int)$score[$i];
        }

        switch($_POST['sortCriteria']){
            case 'fName': usort($students,function($st1, $st2){
                if ($st1->firstName == $st2->firstName) return 0;
                return $st1->firstName > $st2->firstName ? 1 : -1;
            }); break;
            case 'lName': usort($students,function($st1, $st2){
                if ($st1->lastName == $st2->lastName) return 0;
                return $st1->lastName > $st2->lastName ? 1 : -1;
            }); break;
            case 'mail': usort($students,function($st1, $st2){
                if ($st1->email == $st2->email) return 0;
                return $st1->email > $st2->email ? 1 : -1;
            }); break;
            case 'exam': usort($students,function($st1, $st2){
                return $st1->score - $st2->score;
            }); break;
        }

        if($_POST['order'] != 'ascending'){
            $students=array_reverse($students);
        }?>

        <table border="1" cellspacing="0">
            <thead>
                <tr>
                    <th>First name</th><th>Last name</th><th>Email</th><th>Exam score</th>
                </tr>
            </thead>
            <tbody>
                <?php
                foreach($students as $s):?>
                    <tr>
                        <td><?=$s->firstName?></td>
                        <td><?=$s->lastName?></td>
                        <td><?=$s->email?></td>
                        <td><?=$s->score?></td>
                    </tr>
                    <?php
                    endforeach;
                ?>
                <tr>
                    <td colspan="3"><b>Average score:<b></td>
                    <td><b><?=round($avg/count($students))?><b></td>
                </tr>
            </tbody>
        </table>
<?php
   }
}
?>

</body>
</html>
