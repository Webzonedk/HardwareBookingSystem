// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

function SubmitValidation(input) {

    document.getElementById(input).submit();

}







//Fading function to fade out lines on list when a storagelocation is deleted
function fadeOut(input) {
 //console.log(input)
    $("#" + input).slideUp('fast', function () {
    });
}

function fadeOutSlow() {
    //console.log(input)
    let x = document.getElementsByClassName("fadeUp")
    for (var i = 0; i < 7; i++) {
    $(x[i]).slideUp('slow', function () {
    });

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

}


