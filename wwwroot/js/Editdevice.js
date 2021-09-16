

//function to submit form based on id 
function SubmitValidation(input) {

    document.getElementById(input).submit();

}


//function to delete device if commentary field has been filled
function CheckInputField() {

    var field = document.getElementById("deleteNote").value;
  /*  var form = document.getElementById("editform");*/
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
        document.getElementById("editForm").submit();
    }
}


