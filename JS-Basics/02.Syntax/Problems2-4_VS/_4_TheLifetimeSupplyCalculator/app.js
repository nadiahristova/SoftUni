function calcSupply(age, maxAge, food, foodPerDay) {
    var amounghtOfFood = (maxAge - age) * 365 * foodPerDay;
    console.log(amounghtOfFood + "kg of " + food + "would be enough until I am " + maxAge + " years old.");
}

calcSupply(16, 102, 'nuts', 1.1);