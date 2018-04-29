"use strict"

function solution(arr) {
    var numArr = arr.filter( function (x) {
       return !isNaN(x);
    });
    numArr.sort(function (x, y) {
        return x < y;
    });
    var maxOcc = 0;
    var mostFrequentNum;
    for (var i = 0; i < numArr.length-1; i++) {
        var occ = 1;
        var num = numArr[i];
        while (numArr[i] === numArr[i+1]) {
            i += 1;
            occ += 1;
        }
        if (maxOcc<occ) {
            maxOcc = occ;
            mostFrequentNum = num;
        }
    }
    console.log("Min number:" + numArr[numArr.length - 1]);
    console.log("Max number:" + numArr[0]);
    console.log("Most frequent number:" + mostFrequentNum);
    console.log(numArr);
}

solution(["Pesho", 2, "Gosho", 12, "true", 9, 2,undefined, 0, "Penka", { bunniesCount : 10}, [10, 20, 30, 40]]);