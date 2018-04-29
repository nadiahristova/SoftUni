for(var i=0; i<=2; i++){
	document.getElementById("0"+i).style.borderTop="none";
	document.getElementById("2"+i).style.borderBottom="none";
}

for(var i=0; i<=2; i++){
	document.getElementById(i+"0").style.borderLeft="none";
	document.getElementById(i+"2").style.borderRight="none";
}

var playerA = prompt("Enter a name for a player: ");
var playerB = prompt("And another one: ");
var randomNum = Math.floor((Math.random() * 10) + 1);

if(randomNum>5) {
	var playerNameHolder = playerB;
	playerB = playerA;
	playerA = playerNameHolder;
}

document.getElementById("info").innerHTML = "Begin the game.<br /> Please Click on an empty field.<br /><span class='player'>"+playerA + "</span> will be playing first(he/she'll be using X's)";

var nthPlayer=1;
var gameHasEnded=false;
var gameMatrix=[['*','*','*'], ['*','*','*'], ['*','*','*']];
var turns =9;

function Clicked(numb) {
	if(!gameHasEnded){ 
		var number = ("0" + numb).slice(-2);
		var row=parseInt(number.substring(0, 1));
		var column=parseInt(number.substring(1, 2));
		if(nthPlayer==1) {			
		if (gameMatrix[row][column] == '*'){
			document.getElementById(number).value = "x";	
			gameMatrix[row][column] = 'x';
			document.getElementById("info").innerHTML = "<span>O</span> <span class='player'>" + playerB +"</span>'s Turn";
			gameHasEnded=checkMatch(row,column);
			turns--;
		} else 	nthPlayer--;
	} else{				
		document.getElementById("info").innerHTML = "X <span class='player'>" + playerA +"</span>'s Turn";
		if (gameMatrix[row][column] == '*'){
			document.getElementById(number).value = "◎";
			document.getElementById(number).style.color = "#02CF07";
			gameMatrix[row][column] = 'o';
			gameHasEnded=checkMatch(row,column);
			turns--;
			nthPlayer=0;
		} else nthPlayer--;		
	}
		nthPlayer++;
	} 
	if(gameHasEnded) {
		var whoWon='';
		if (nthPlayer==1){
			whoWon =playerB; 
		} else whoWon =playerA;
		document.getElementById("info").innerHTML = "Game won <span class='player'>"+ whoWon+"</span>.";
	}	
	if(turns==0) {
		document.getElementById("info").innerHTML = "We don't have a winner.";
	}
}

function checkMatch(row,column) {
	var currVlalue = gameMatrix[row][column];
	var hasEndedByRows = true;
	var hasEndedByCols = true;
	var hasEndedDiagonally = true;
	for (i=0; i<=2;i++){
		hasEndedByRows = hasEndedByRows &&(currVlalue==gameMatrix[row][i]);
	}
	
	for (i=0; i<=2;i++){
		hasEndedByCols = hasEndedByCols &&( currVlalue==gameMatrix[i][column]);
	}
	
	if (row == column) {
		for(var i=0; i<=2; i++){
			hasEndedDiagonally = hasEndedDiagonally && (currVlalue==gameMatrix[i][i]);
		}	
	} else {
		for(var i=0; i<=2; i++){
			hasEndedDiagonally = hasEndedDiagonally && (currVlalue==gameMatrix[i][2-i]);
		}
	}
	
	var hasEnded = hasEndedByRows || hasEndedByCols || hasEndedDiagonally;
	return hasEnded;
}