

//this method runs when view has loaded
$(document).ready(function (e) {

    var searchbar = document.getElementById("searchbar");
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
function SendValueToButton() {

    let element = document.getElementById(event.target.id);
    let splitted = element.id.split("_");
    let id = "basket_" +splitted[1]
    let button = document.getElementById(id);
    let modelID = button.value;

    let splittedvalues = button.value.split("-");
    if (splittedvalues.length > 0) {

        modelID = splittedvalues[0];

    }

    button.value = modelID + "-" + element.value;
   
}