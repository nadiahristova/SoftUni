function findYoungestPerson(array) {
    var minAge = Number.MAX_VALUE;
    var name = '';
    array.forEach(function (persone) {
        if (persone.hasSmartphone && persone.age < minAge) {
            minAge = persone.age;
            name = persone.firstname + ' ' + persone.lastname;
        }
    });
    console.log('The youngest person is ' + name);
}

var people = [
    { firstname : 'George', lastname: 'Kolev', age: 32, hasSmartphone: false }, 
    { firstname : 'Vasil', lastname: 'Kovachev', age: 40, hasSmartphone: true },
    { firstname : 'Bay', lastname: 'Ivan', age: 81, hasSmartphone: true },
    { firstname : 'Baba', lastname: 'Ginka', age: 40, hasSmartphone: false }]

findYoungestPerson(people);