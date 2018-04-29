Array.prototype.removeItem = function (value) {
    var constructor = this;
    var filteredArr = [];
    constructor.forEach(function (item) {
        if (item !== value) { 
            filteredArr.push(item);
        }
    });
    return filteredArr;
    //while (constructor.indexOf(value) !== -1) {
    //    var index = constructor.indexOf(value);
    //    constructor.splice(index, 1);
    //}
    //return constructor;
}

var givenArr = [1, 2, 1, 4, 1, 3, 4, 1, 111, 3, 2, 1, '1'];
console.log(givenArr.removeItem(1));