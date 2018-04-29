function extractObjects(array){
    var objOnly = [];
    for(var index in array){
        if (Object.prototype.toString.call(array[index]) === "[object Object]") {
            objOnly.push(array[index]);
        }
    }
    console.log(objOnly);
}

extractObjects(["Pesho", 4, 4.21, { name : 'Valyo', age : 16 }, 
    { type : 'fish', model : 'zlatna ribka' }, [1, 2, 3], "Gosho", { name : 'Penka', height: 1.65 }]);