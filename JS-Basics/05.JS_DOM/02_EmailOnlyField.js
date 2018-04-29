var mailPattern = /[a-zA-Z]+[0-9_a-zA-Z-]*@[a-z0-9.-]+.[a-z]{2,4}$/

function checkIfValidStr(){
	var input = document.getElementById('inpu').value;
	document.getElementById('checker').value = input;
	if(input.match(mailPattern)){
		document.getElementById('checker').style.backgroundColor="#90EE90";
	} else document.getElementById('checker').style.backgroundColor="red"
}

function clearValidator(){
	document.getElementById('checker').style.backgroundColor="white";
	document.getElementById('checker').value = "";
}