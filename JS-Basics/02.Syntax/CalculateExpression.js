function evalResult() {
	var equation = document.getElementById("equation-input").value;	
	if(equation.indexOf('(') !== -1){
		var numOfBrackets = equation.split('(').length - 1;		
		var firstBracketIndex = 0;
		do{
			var eqInBrackets = equation;
			firstBracketIndex = equation.indexOf('(');
			var lastBracketIndex = equation.indexOf(')');
			eqInBrackets = eqInBrackets.substring(firstBracketIndex, lastBracketIndex);
			console.log(eqInBrackets);
			while(eqInBrackets.indexOf('(') !== -1) {
				var currIndex = eqInBrackets.indexOf('(');
				firstBracketIndex += currIndex;
				eqInBrackets = eqInBrackets.substring(currIndex+1);
				console.log(eqInBrackets);
			}
			var valueOfSubEq = returnValue(eqInBrackets);
			console.log(valueOfSubEq);			
			equation = equation.replace( '(' + eqInBrackets + ')', valueOfSubEq);		
			console.log(equation);
			numOfBrackets-=1;
		} while(numOfBrackets);
	}
	var result = returnValue(equation);
	document.getElementById("equation-output").value = result;
}

function returnValue(strEquation) {
	if(!isNaN(strEquation)){
		return strEquation;
	}
	var numbers = returnNumbersOnly(strEquation);
	var operations = returnOperationsOnly(strEquation);
	if(numbers.length === operations.length){
		numbers.unshift(0);
	}
	while(operations.indexOf('/') !== -1) {
		var index = operations.indexOf('/');
		var division = numbers[index] /  numbers[index+1];
		numbers[index] = division;
		numbers.splice(index+1,1);
		operations.splice(index,1);
	}
	while(operations.indexOf('*') !== -1) {
		var index = operations.indexOf('*');
		var multiplication = numbers[index] * numbers[index+1];
		numbers[index] = multiplication;
		numbers.splice(index+1,1);
		operations.splice(index,1);
	}
	var value = numbers[0];	
	for (var i =0; i<numbers.length - 1; i++){
		switch(operations[i]) {			
			case '-': value -= parseFloat(numbers[i+1]); break;
			case '+': value += parseFloat(numbers[i+1]); break;
		}
	}
	return value;
}

function returnNumbersOnly(strEquation){
	var unfilteredNumbers = strEquation.split(/[*,/,+,\-,=]/);
	var numbers = unfilteredNumbers.filter(function(obj){return obj !== ''})
	return numbers;
}

function returnOperationsOnly(strEquation){
	var unfilteredOperations = strEquation.split(/[0-9,.]+/);
	var operations = unfilteredOperations.filter(function(obj){return obj !== ''})
	return operations;
}

//5-9+158/9*12596-1
//12-(1*(4-(2)))