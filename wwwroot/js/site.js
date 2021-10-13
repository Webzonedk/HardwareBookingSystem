// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.




function SubmitValidation(input) {

    document.getElementById(input).submit();

}







//Fading function to fade out lines on list when a storagelocation is deleted
function fadeOut(input) {
    $("#" + input).slideUp('fast', function () {
    });
}





////$('[data-toggle="tooltip"]').tooltip()