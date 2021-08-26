


//Submitting data from view
function submit(input) {

    document.getelementById(input).submit();

}

//Submitting data from view (Not used at the moment)
function submit2(input) {

    document.getelementById(input).submit();

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


//Fading function to fade out lines on list when a storagelocation is deleted
function fadeOut(input) {
    $("#" + input).slideUp();
}


//Function to check if inout fields is filled before submitting
//might be obsolete
function checkInputFields() {
    let a = document.getElementById("buildingDropDown").value;
    let b = document.getElementById("roomNumberDropDown").value;
    let c = document.getElementById("shelfNameDropDown").value;
    let d = document.getElementById("shelfLevelDropDown").value;
    let e = document.getElementById("shelfSpotDropDown").value;
    if (a == null || a == "", b == null || b == "", c == null || c == "", d == null || d == "", e == null || e == "") {
        alert("Lokationen blev IKKE oprettet! \n Alle felter skal være udfyldt.");
    }
}