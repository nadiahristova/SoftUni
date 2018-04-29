function solution() {
    function Person(firstName, lastName, age) {
        this.age = age;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    var people = [
        new Person("Scott", "Guthrie", 38),
        new Person("Scott", "Johns", 36),
        new Person("Scott", "Hanselman", 39),
        new Person("Jesse", "Liberty", 57),
        new Person("Jon", "Skeet", 38)
    ];
    
    groupBy(people, 'lastName');

    function groupBy(peopleArr, criteria) {      
        var obj = {};
        peopleArr.forEach(function (x) {
            if (obj["Group " + x[criteria].toString()] === undefined) {
                obj["Group " + x[criteria].toString()] = [];
            } 
            obj["Group " + x[criteria].toString()].push(x.firstName + ' ' + x.lastName + '(age' + x.age + ')');
        });
        console.log(obj);
    }
}

solution();