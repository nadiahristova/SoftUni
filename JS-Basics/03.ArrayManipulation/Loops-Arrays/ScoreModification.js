function solution(arr){
    arr = arr.filter(function (x) {
        return x >= 0 && x <= 400;
    });
    arr = arr.map(function (number) {
        return Number((number * 0.8).toFixed(1));
    });
    arr.sort(function (x, y) {
        return x > y;
    });
    console.log(arr);
}

solution([200, 120, 23, 67, 350, 420, 170, 212, 401, 615, -1]);