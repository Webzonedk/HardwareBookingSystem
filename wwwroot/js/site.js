// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

function SubmitValidation(input) {

    document.getElementById(input).submit();

}

//Submitting data from view
function submit(input) {

    document.getelementById(input).submit();

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
        document.getElementById("warning").innerText ='';
        console.info("delete");
        form.submit();
    }
}


