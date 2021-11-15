

//this method runs when view has loaded
$(document).ready(function (e) {

    var searchbar = document.getElementById("searchBar");
    var length = searchbar.value.length;


    if (searchbar.setSelectionRange) {
        searchbar.focus();
        searchbar.setSelectionRange(length, length);
    }
    else if (searchbar.createTextRange) {
        var t = searchbar.createTextRange();
        t.collapse(true);
        t.moveEnd('character', length);
        t.moveStart('character', length);
        t.select();
    }

});

//functions to delay sumit while typing in the input field

var timer;
function SetTimeout() {

    var doneTypingInterval = 1000; // wait 1 second

    clearTimeout(timer);
    timer = setTimeout(doneTyping, doneTypingInterval);

}

//clear timer on key down
function ClearTimeout() {

    clearTimeout(timer);
}

function doneTyping() {

    document.getElementById("searchform").submit();

}



//sends quantity to button before submit
function SendValueToButton(inputTitle) {

  
    let element = document.getElementById(event.target.id);
    let splitted = element.id.split("_");
    let id = "basket_" + splitted[1];
    let stock = document.getElementById("stock_" + splitted[1]);
    let button = document.getElementById(id);
    let modelID = button.value;

   

    if (stock == null) {
        stock = '0';
    }
    else {
        stock = stock.innerText;
    }



    let splittedvalues = button.value.split("-");
    if (splittedvalues.length > 0) {

        modelID = splittedvalues[0];

    }


    button.value = modelID + ";" + stock + ";" + element.value;
    console.log("button value:" + button.value);

}

//sends location to different form
function SendLocationToForm() {
    let selectedLocation = document.getElementById(event.target.id);
    let locationInput = document.getElementById("location");

    locationInput.value = selectedLocation.value;

    console.log(locationInput.value);
}

//sends new quantity to input to form for booking search
function changeQuantity() {


    //get elements from view 
    let fromInput = document.getElementById(event.target.id);
    let splittedValues = fromInput.id.split('_');

    let stock = document.getElementById("stock_" + splittedValues[1])
    let toInput = document.getElementById("searchOutput_" + splittedValues[1]);

    //send input data to hidden input
    toInput.value = fromInput.value;
    


    //calculate basket count
    let array = document.getElementsByName("input");
    let basket = document.getElementById("basket");
    let hiddenbasket = document.getElementById("basketHidden");
    let basketCount = 0;
    for (var i = 0; i < array.length; i++) {

        let val = parseInt(array[i].value);
        basketCount = basketCount + val;
    }

    hiddenbasket.value = basketCount;
    basket.textContent = "enheder i kurven" + basketCount;
}