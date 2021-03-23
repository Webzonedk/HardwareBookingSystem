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

//Used to sort the columns in the views

    function sorting() {
        document.getElementById('ChosenProvider').value = '1';
        document.getElementById('loginForm').submit();
    }
