<!DOCTYPE html>
//The output CV should be opened in new Tab. If all of the reqquirements are met and the program doesn't open a new tab
//u should disable the default option of ur browser to suppress pop-ups.
<html>
    <head>
        <title>Personal CV</title>
    </head>

    <body>
        <script>
            var nextPLId = 1;
            var nextLId = 1;
            function addProgrammLanguage(){
                nextPLId++;
                var pLDiv = document.createElement('div');
                pLDiv.setAttribute("id","numPL"+nextPLId);
                pLDiv.innerHTML="<input type='text' name='pLanName[]'/><select name='pLLevel[]'><option value='Beginner'>Beginner</option><option value='Intermediate'>Intermediate</option><option value='Advanced'>Advanced</option></select>"
                document.getElementById('programmLanguagesDiv').appendChild(pLDiv);
            }

            function addLanguage(){
                nextLId++;
                var lDiv = document.createElement('div');
                lDiv.setAttribute("id","numL"+nextLId);
                lDiv.innerHTML="<input type='text' name='lanName[]'/>" +
                "<select name='lComprehension[]'><option selected disabled >-Comprehension-</option><option " +
                "value='Basic'>Basic</option><option value='Good'>Good</option><option value='Excellent'>Excellent</option></select>"
                + "<select name='lReading[]'><option selected disabled >-Reading-</option><option " +
                "value='Basic'>Basic</option><option value='Good'>Good</option><option value='Excellent'>Excellent</option></select>"
                +"<select name='lWriting[]'><option selected disabled >-Writing-</option><option " +
                "value='Basic'>Basic</option><option value='Good'>Good</option><option value='Excellent'>Excellent</option></select>";
                document.getElementById('languagesDiv').appendChild(lDiv);
            }

            function removeLanguage(parent,which){
                var next = (which=="L") ? nextLId : nextPLId;
                console.log("num"+which+next);
                console.log(typeof ("num"+which+next));
                var inputDiv = document.getElementById("num"+which+next);
                document.getElementById(parent).removeChild(inputDiv);
                (which=="L") ? nextLId-- : nextPLId--;
            }
        </script>

        <form method="post" action="" name="f">
            <fieldset>
                <legend>
                    Personal Information
                </legend>
                <input type="text" name="fName" placeholder="First Name"/><span>*</span> <br />
                <input type="text" name="lName" placeholder="Last Name"/><span>*</span> <br />
                <input type="email" name="email" placeholder="Email"/><span>*</span> <br />
                <input type="text" name="pNum" placeholder="Phone Number"/><span>*</span> <br />
                <label for="fMale">Female</label>
                <input type="radio" name="gender" id="fMale" value="Female" checked/>
                <label for="male">Male</label>
                <input type="radio" name="gender" id="male" value="Male"/><br />
                <label for="bDate">Birth day:</label><br />
                <input type="date" name="dateOfBirth" id="bDate" /><br />
                <label for="nationality">Nationality:</label><br />
                <select name="nationality" id="nationality">
                    <option value="Bulgarian">Bulgarian</option>
                    <option value="English">English</option>
                    <option value="German">German</option>
                </select><br />
            </fieldset>

            <fieldset>
                <legend>
                    Last Work Position
                </legend>
                <label for="companyName">Company Name<span>*</span>:</label>
                <input type="text" name="companyName" id="companyName"/> <br />
                <label for="fromFC">From</label>
                <input type="date" name="fromFC" id="fromFC" /> <br />
                <label for="toFC">To</label>
                <input type="date" name="toFC" id="toFC" /> <br />
            </fieldset>

            <fieldset>
                <legend>
                    Computer Skills
                </legend>
                Programming Languages<br />
                <div id="programmLanguagesDiv"></div>
                <script>addProgrammLanguage()</script>
                <button type="button" onclick="removeLanguage('programmLanguagesDiv','PL')" >Remove Language</button>
                <button type="button" onclick="addProgrammLanguage()" >Add Language</button>
            </fieldset>

            <fieldset>
                <legend>
                    Other Skills
                </legend>
                Languages<span>*</span>:<br />
                <div id="languagesDiv"></div>
                <script>addLanguage()</script>
                <button type="button" onclick="removeLanguage('languagesDiv','L')">Remove Language</button>
                <button type="button" onclick="addLanguage()" >Add Language</button><br />
                Driver's License<br />
                <label for="dlB">B</label><input type="checkbox" name="driverLicense[]" value="B" id="dlB"/>
                <label for="dlA">A</label><input type="checkbox" name="driverLicense[]" value="A" id="dlA"/>
                <label for="dlC">C</label><input type="checkbox" name="driverLicense[]" value="C" id="dlC"/>
            </fieldset>

            <input type="submit" name="submit" value="Generate CV"/>
        </form>
    </body>
</html>
<?php

function validator0(){
    if (isset($_POST['lComprehension']) && isset($_POST['lReading']) && isset($_POST['lWriting'])){
        return true;
    } else {
        echo "Fill in the Language Lavels.<br />";
        return false;
    }
}

function validator1($info){
    $regex='/[a-z]{2,20}/i';
    if(gettype($info)=='string'){
        if(preg_match($regex,$info)){
            return true;
        } else{
            echo("Invalid Name/Language Input.<br />");
            return false;
        }
    } else{
        $count=count($info);
        $valid = true;
        foreach($info as $str){
            $valid = $valid && preg_match($regex,$str);
        }
        if($valid){
            return true;
        } else{
            echo("Invalid Name/Language Input.<br />");
            return false;
        }
    }
}

function validator2($info){
    $regex='/[a-z0-9]{2,20}/i';
    if(preg_match($regex,$info)){
        return true;
    } else{
        echo("Invalid Former Company Name.<br />");
        return false;
    }
}

function validator3($info){
    $regex='/^\+?[0-9\s\-]+$/';
    if(preg_match($regex,$info)){
        return true;
    } else{
        echo("Invalid Phone Number.<br />");
        return false;
    }
}

function validator4($info){
    $regex='/^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/';
    if(preg_match($regex,$info)){
        return true;
    } else{
        echo("Invalid E-mail Address.<br />");
        return false;
    }
}

if(isset($_POST['submit'])){
    $firstName=$_POST['fName'];
    $lName = $_POST['lName'];
    $lanName = $_POST['lanName'];
    $formerCompName =$_POST['companyName'];
    $phNumber = $_POST['pNum'];
    $email = $_POST['email'];
    $gender = $_POST['gender'];
    $dateOfBirth = $_POST['dateOfBirth'];
    $nationality = $_POST['nationality'];
    $fromFC=$_POST['fromFC'];
    $toFC = $_POST['toFC'];
    $pLLevel = $_POST['pLLevel'];
    $pLLevelHTTP = http_build_query(array('pLLevel' => $pLLevel));
    $pLanName =$_POST['pLanName'];
    $pLanNameHTTP = http_build_query(array('pLanName' => $pLanName));
    $lanName = $_POST['lanName'];
    $lanNameHTTP = http_build_query(array('lanName' => $lanName));
    $lComprehension = $_POST['lComprehension'];
    $lComprehensionHTTP = http_build_query(array('lComprehension' => $lComprehension));
    $lReading = $_POST['lReading'];
    $lReadingHTTP = http_build_query(array('lReading' => $lReading));
    $lWriting = $_POST['lWriting'];
    $lWritingHTTP = http_build_query(array('lWriting' => $lWriting));
    if(isset($_POST['driverLicense'])):
    $driverLicense = $_POST['driverLicense'];$driverLicenseHTTP = http_build_query(array('driverLicense' => $driverLicense));
    else:
    $driverLicenseHTTP = null;
    endif;


    $validInput = validator1($firstName) && validator1($lName) && validator1($lanName) && validator2($formerCompName) && validator3($phNumber)&& validator4($email);
    $validInput &= validator0();
    if($validInput){
        echo "<script
type=\"text/javascript\">".
            "window.open('http://localhost/softuni/PHPStorm/03_Homework/05_CV.php?fName=".$firstName."&lName=".$lName
            ."&email=".$email."&pNum=".$phNumber."&gender=".$gender."&dateOfBirth=".$dateOfBirth."&nationality=".$nationality."&companyName=".$formerCompName
            ."&fromFC=".$fromFC."&toFC=".$toFC."&".$pLLevelHTTP."&".$pLanNameHTTP."&".$lanNameHTTP."&".$lComprehensionHTTP.
            "&".$lReadingHTTP."&".$lWritingHTTP."&".$driverLicenseHTTP."')".
        "</script>";
        //$_POST['f']->action = '05_CV.php'; return true; - couldn't make it work. I'm open to suggestions how to
        //redirect the data submitted from given form to a certain php file.
    }else{
        echo "Please, refill the form.";
    };
}
?>