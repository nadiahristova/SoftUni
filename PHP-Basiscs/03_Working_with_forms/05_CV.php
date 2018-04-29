<!--"window.open('http://localhost/softuni/PHPStorm/03_Homework/05_CV.php?fName=".$firstName."&lName=".$lName-->
<!--."&email=".$email."&pNum=".$phNumber."&gender=".$gender."&dateOfBirth=".$dateOfBirth."&nationality=".$nationality."&companyName=".$formerCompName-->
<!--."&fromFC=".$fromFC."&toFC=".$toFC."&pLLevel=".$pLLevelHTTP."&pLanName=".$pLanNameHTTP."&lanName=".$lanName."&lComprehension="-->
<!--.$lComprehension ."&lReading=".$lReading."&lWriting=".$lWriting."&driverLicense=".$driverLicense."')".-->

<!DOCTYPE html>
<html>
    <head>
        <title>Personal CV</title>
    </head>

    <body>
        <h1>CV</h1>

        <table border="1">
            <tr>
                <th colspan="2">Personal Information</th>
            </tr>
            <tr>
                <td>First Name</td><td><?= $_GET['fName'] ?></td>
            </tr>
            <tr>
                <td>Last Name</td><td><?= $_GET['lName'] ?></td>
            </tr>
            <tr>
                <td>Email</td><td><?= $_GET['email'] ?></td>
            </tr>
            <tr>
                <td>Gender</td><td><?=$_GET['gender'] ?></td>
            </tr>
            <?php if($_GET['dateOfBirth']): ?>
            <tr>
                <td>Birth Date</td><td><?= $_GET['dateOfBirth'] ?></td>
            </tr>
            <?php endif; ?>
            <tr>
                <td>Nationality</td><td><?= $_GET['nationality'] ?></td>
            </tr>
        </table>
        <br />

        <table border="1">
            <tr>
                <th colspan="2">Last Work Position</th>
            </tr>
            <tr>
                <td>Company Name</td><td><?= $_GET['companyName'] ?></td>
            </tr>
            <?php if($_GET['fromFC']): ?>
                <tr>
                    <td>Birth Date</td><td><?= $_GET['fromFC'] ?></td>
                </tr>
            <?php endif; ?>
            <?php if($_GET['toFC']): ?>
                <tr>
                    <td>Birth Date</td><td><?= $_GET['toFC'] ?></td>
                </tr>
            <?php endif; ?>
        </table><br />

        <?php if(isset($_GET['pLanName'])):?>
        <table border="1">
            <tr>
                <th colspan="2">Computer Skills</th>
            </tr>
            <tr>
                <td>Programming Languages</td>
                <td>
                    <table border="1">
                        <tr>
                            <th>Language</th><th>Skill Level</th>
                        </tr>
                        <?php for($i=0;$i<count($_GET['pLanName']);$i++):?>
                            <tr>
                                <td><?=$_GET['pLanName'][$i]?></td><td><?=$_GET['pLLevel'][$i]?></td>
                            </tr>
                        <?php endfor; ?>
                    </table>
                </td>
            </tr>
        </table><br />
        <?php endif; ?>

        <table border="1">
            <tr>
                <th colspan="2">Other Skills</th>
            </tr>
            <tr>
                <td>Languages</td>
                <td>
                    <table border="1">
                        <tr>
                            <th>Language</th><th>Skill Level</th><th>Reading</th><th>Writing</th>
                        </tr>
                        <?php for($i=0;$i<count($_GET['lanName']);$i++):?>
                            <tr>
                                <td><?=$_GET['lanName'][$i]?></td>
                                <td><?=$_GET['lComprehension'][$i]?></td>
                                <td><?=$_GET['lReading'][$i]?></td>
                                <td><?=$_GET['lWriting'][$i]?></td>
                            </tr>
                        <?php endfor; ?>
                    </table>
                </td>
            </tr>
            <?php if(isset($_GET['driverLicense'])): ?>
                <tr>
                    <td>Driver's License</td>
                    <td colspan="4"><?=join(", ", $_GET['driverLicense'])?></td>
                </tr>
            <?php endif; ?>
        </table>
        <?php

        ?>
    </body>
</html>