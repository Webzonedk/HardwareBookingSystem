
//Submitting data from view
function submit(formID) {

    document.getelementById(formID).submit();
}

function returnToInputField() {
    let activeID = document.activeElement.id;
    document.getElementById("hiddenInputFieldID").value = activeID;
}

//this method runs when view has loaded
$(document).ready(function (e) {

    let hiddenInput = document.getElementById("hiddenInputFieldID");
    let searchBar = document.getElementById(hiddenInput.value)
    if (searchBar != null) {

        let length = searchBar.value.length;

        if (searchBar.setSelectionRange) {
            searchBar.focus();
            searchBar.setSelectionRange(length, length);
        }
        else if (searchbar.createTextRange) {
            let t = searchbar.createTextRange();
            t.collapse(true);
            t.moveEnd('character', length);
            t.moveStart('character', length);
            t.select();
        }

    }


    //--------------------------------------------------------------
    //--------------------------------------------------------------
    //Function to set a timer to wait before sliding up feedback and run the function called FadeOutSlow
    setTimeout(FadeOutSlow, 2000);
    //--------------------------------------------------------------
    //--------------------------------------------------------------





});


//functions to delay sumit while typing in the input field
let timer;
function SetTimeout() {

    let doneTypingInterval = 1000; // wait 1 second

    clearTimeout(timer);
    timer = setTimeout(doneTyping, doneTypingInterval);

}


//clear timer on key down
function ClearTimeout() {

    clearTimeout(timer);
}

function doneTyping() {

    document.getElementById("listStorageLocations").submit();

}



//Function to slide up th feedback (included in the document.ready as well)
function FadeOutSlow() {
    let x = document.getElementsByClassName("fadeUp")
    for (var i = 0; i < 7; i++) {
        $(x[i]).slideUp('slow', function () {
        });

    }
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



