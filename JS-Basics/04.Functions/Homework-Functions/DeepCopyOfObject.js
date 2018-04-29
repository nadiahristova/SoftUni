function clone(obj) {    
    if (obj == null || typeof (obj) != 'object') return obj; //if the parameter is not an object it doesn't need clonning    
    var clonedObj = obj.constructor();
      
    for (var key in obj) {
        if (obj.hasOwnProperty(key)) {
            clonedObj[key] = arguments.callee(obj[key]);
        }
    }
    return clonedObj;
};


function compareObjects(obj, objCopy) {
    return obj == objCopy;
}

var a = { name: 'Pesho', age: 21 };
var b = clone(a); 
console.log('a == b --> ', compareObjects(a, b)); 