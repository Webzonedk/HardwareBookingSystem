$(document).ready(function () {

    

    //window.addEventListener('storage', function (e) {

    //    if (e.storageArea === sessionStorage) {
    //        console.log(e.sessionStorage.newValue);
            
    //    }

    //    if (e.storageArea === localStorage) {
    //        console.log(e.newValue);
    //    }

    //});

    window.addEventListener('storage', function (e) {
        let temp = e.localStorage;
        alert(temp);
        if (e.storageArea === localStorage) {
            alert('change');
        }
        // else, event is caused by an update to localStorage, ignore it
    });

    

});