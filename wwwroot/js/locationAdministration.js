
function ClearInputField(inputID) {
    document.getElementById(inputID).value = "";
};

//Submitting data from view
function submit(formID) {

    document.getelementById(formID).submit();
}

function returnToInputField() {
    let activeID = document.activeElement.id;
    document.getElementById("hiddenInputFieldID").value = activeID;
};

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




//Scrolling down to selected ID
function scrollToDiv(input, input2, id) {

    if (input != '' || input2 !='') {
        window.scrollTo(0, document.body.scrollHeight);
        //let ele = document.getElementById(input);
        //console.log(input)
        //window.scrollTo(ele.offsetLeft, ele.offsetTop);
    }

    if (id != '') {
        console.log(id)

        let result = parseFloat(id);
        if (result > 1) {
            result--;
        }
        else {
            result = 1;
        }
        let ele = document.getElementById(result);
        window.scrollTo(ele.offsetLeft, ele.offsetTop);
    }
}




//Function to slide up th feedback (included in the document.ready as well)
function FadeOutSlow() {
    let x = document.getElementsByClassName("fadeUp")
    for (var i = 0; i < 25; i++) {
        $(x[i]).slideUp('slow', function () {
        });

    }
}









// Function to make a popup alert if a storagelocation can not be deleted, due to occupation by devices
function alertFunction(tempData) {
    if (tempData == "occupied") {
        let message = "Lokationen kan ikke slettes da der findes enheder på denne lokation.\n Flyt venligst enhederne væk fra lokationen inden du forsøger at slette en lokation";
        alert(message);
    }
}



