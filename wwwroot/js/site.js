﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

function SubmitValidation(input) {

    document.getElementById(input).submit();

}

//Submitting data from view
function submit(input) {

    document.getelementById(input).submit();

}

function showRoom() {
    var building = document.getElementById("buldinginput");
    var roomNr = document.getElementById("romNrInput");
    var chosenRoom = document.getElementById("chosenRoom");

    if (building.value != "" && roomNr.value != "") {
        chosenRoom.value = building.value + "." + roomNr.value
    }
}

function CheckInputField() {

    var field = document.getElementById("deleteNote").value;
    if (field == "") {

        /* var AlertBox = confirm("For at slette enheden, skal årsag udfyldes");*/
        document.getElementById("warning").innerText = "For at slette enheden, skal årsag udfyldes";
        //if (AlertBox == true || AlertBox == false) {
        //}
        return false;
    }
    else {

        document.getElementById("editForm").action = "DeleteDevice";
        document.getElementById("warning").innerText = '';
        console.info("delete");
        form.submit();
    }
}

//call submit at scanDevice view
function ReturnToEdit(val) {
    //var location = $("#content").val();
    //console.log(location);
    //var id = 0;

    //var output = location + '-' + id;
    //console.log(output);

    //submit();
    console.log("test test test" + val);
}

function alertFunction(tempData) {
    if (tempData == "occupied") {
        let message = "Lokationen kan ikke slettes da der findes enheder på denne lokation.\n Flyt venligst enhederne væk fra lokationen inden du forsøger at slette en lokation";
        alert(message);
    }
}

function fadeOut(input) {
    $("#" + input).slideUp();  
}

function checkInputFields() {
    let a = document.getElementById("buildingDropDown").value;
    let b = document.getElementById("roomNumberDropDown").value;
    let c = document.getElementById("shelfNameDropDown").value;
    let d = document.getElementById("shelfLevelDropDown").value;
    let e = document.getElementById("shelfSpotDropDown").value;
    if (a == null || a == "", b == null || b == "", c == null || c == "", d == null || d == "", e == null || e == "", ) {
        alert("Lokationen blev IKKE oprettet! \n Alle felter skal være udfyldt.");
    }
}