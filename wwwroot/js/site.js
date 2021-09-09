// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

function SubmitValidation(input) {

    document.getElementById(input).submit();

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




//Fading function to fade out lines on list when a storagelocation is deleted
function fadeOut(input) {
 //console.log(input)
    $("#" + input).slideUp('fast', function () {
    });
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


