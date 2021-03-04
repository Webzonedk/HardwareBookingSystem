// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

function SubmitValidation() {


    document.getElementById("EditForm").submit();


    if (document.forms['EditForm'].locations.value == "") {
        alert("emtpty")
        return false;
    }
    else {
        alert("imput")

        document.getElementById("EditForm").submit();

        this.document.Submit();
    }
}