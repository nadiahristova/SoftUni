var switcher = false;
var clock;

function tick(){
	var time = new Date();
	document.getElementById("clock").innerHTML = ("0" + time.getHours()).slice(-2) + ":" + ("0" + time.getMinutes()).slice(-2) + ":" + ("0" + time.getSeconds()).slice(-2);
}

function startClock(){
	document.getElementById("on-off").value ="Stop me!";
	clock = setInterval("tick()",1000);
}

function stopClock(){
	clearInterval(clock);
	document.getElementById("on-off").value ="Play clock";	
}

function swi(){	
	switcher = !switcher;	
	if (!switcher){		
		startClock();
	} else {
		stopClock();
	}	
}