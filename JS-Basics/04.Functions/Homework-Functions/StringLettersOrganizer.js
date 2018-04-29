function sortLetters(string, boolean) {
    var strArr = string.split('');
    var lowerA, lowerB;
    if (boolean) {
        strArr = strArr.sort(function (a,b) {
            lowerA = a.toLowerCase();
            lowerB = b.toLowerCase();
            if (lowerA > lowerB) return 1;
            else if (lowerA < lowerB) return -1;
            else return 0;
        });
    } else {
        strArr = strArr.sort(function (a,b) { 
            lowerA = a.toLowerCase();
            lowerB = b.toLowerCase();
            return (lowerA > lowerB) ? -1 : ((lowerA < lowerB) ? 1 : 0);
        });
    }    ;
    string = strArr.join('');
    console.log(string);
}

sortLetters('HelloWorld', false);