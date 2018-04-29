function calcCylinderVol(arr) {
    var radius = arr[0];
    var height = arr[1];
    var volume = (Math.PI * Math.pow(radius, 2) * height).toFixed(3);
    console.log(volume);
}

calcCylinderVol([12, 3]);