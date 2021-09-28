
//Submitting data from view
function submit(formID) {
    let activeID = document.activeElement.id;
    document.getElementById("hiddenInputFieldID").value = activeID;
    console.log(activeID);
    document.getelementById(formID).submit();
}

 
//this method runs when view has loaded
$(document).ready(function (e) {

    var hiddenInput = document.getElementById("hiddenInputFieldID");
    let searchBar = document.getElementById(hiddenInput.value)
    var length = searchBar.value.length;


    if (searchBar.setSelectionRange) {
        searchBar.focus();
        searchBar.setSelectionRange(length, length);
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






//Scrolling down to selected ID
function scrollToDiv(input, id) {

    if (input != "") {
        window.scrollTo(0, document.body.scrollHeight);
    }

    if (id != "") {
        let result = parseFloat(id);
        result++;
        let ele = document.getElementById(result);
        console.log(result)
        window.scrollTo(ele.offsetLeft, ele.offsetTop);

    }


}






//Function to show specified room in the input field in the massdestruction area
function showRoom() {
    let building = document.getElementById("buldingInput").value;//getting value
    let roomNr = document.getElementById("romNrInput").value;//getting value
    let chosenRoom = document.getElementById("chosenRoom");//inserting value

    if (building != "" && roomNr != "") {
        chosenRoom.value = building + "." + roomNr;
    }
    else {
        chosenRoom.value = "";
    }
}




// Function to make a popup alert if a storagelocation can not be deleted, due to occupation by devices
function alertFunction(tempData) {
    if (tempData == "occupied") {
        let message = "Lokationen kan ikke slettes da der findes enheder på denne lokation.\n Flyt venligst enhederne væk fra lokationen inden du forsøger at slette en lokation";
        alert(message);
    }
}



