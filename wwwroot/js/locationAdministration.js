
//Submitting data from view
function submit(input) {

    document.getelementById(input).submit();
}


////Submitting data from view
//$(document).ready(function (e) {

//    console.log("test test test")
//    let scrollToDiv = document.getElementById("insertData");
//    scrollToDiv.scrollTop = scrollToDiv.scrollHeight;
//});



//$(document).ready(function () {

//window.scrollTo(0, document.body.scrollHeight);
//});








//Scrolling down to selected ID
function scrollToDiv(input, id) {
    console.log("test test test " + input);
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


//function scroll(divID) {
//    let ele = document.getElementById(divID + 1);
//    console.log(ele)
//    window.scrollTo(ele.offsetLeft, ele.offsetTop);
//}




//Submitting data from view (Not used at the moment)
function submit2(input) {


    let building = document.getElementById("buldingInput").value;//getting value
    let roomNr = document.getElementById("romNrInput").value;//getting value
    let chosenRoom = document.getElementById("chosenRoom");//inserting value

    if (building != "" && roomNr != "") {
        chosenRoom.value = building + "." + roomNr;
    }
    else {
        chosenRoom.value = "";
    }
    document.getelementById(input).submit();
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