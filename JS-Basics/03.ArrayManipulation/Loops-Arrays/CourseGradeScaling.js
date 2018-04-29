function solution(str){
    var arr = JSON.parse(str);
    arr.forEach(function (obje) {
        obje.score *= 1.1;
        if (obje.score >= 100) {
            obje.score = Math.round(obje.score * 10) / 10;
            obje['hasPassed'] = true;
        }
    })    
    var filteredArr = arr.filter(function (x) { 
        return x.hasPassed;
    });
    filteredArr.sort(function (x,y) { 
        return x.name > y.name;
    });
    console.log(JSON.stringify(filteredArr));    
}

var obj = [{'name' : 'Пешо','score' : 91},
    {'name' : 'Лилия', 'score' : 290},
    {'name' : 'Алекс', 'score' : 343},
    {'name' : 'Габриела', 'score' : 400},
    {'name' : 'Жичка', 'score' : 70}]

var jsonStr = JSON.stringify(obj);

solution(jsonStr);