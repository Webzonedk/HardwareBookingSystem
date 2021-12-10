
//this method runs when view has loaded
$(document).ready(function (e) {

    var video = document.getElementById("video");
    if (video != null) {
        video.style.display = "none";

    }

    //------------------------------------------------------------------
    // disabled feature that checks if user is on mobile or computer
    //------------------------------------------------------------------
    //check if user in on a mobile device
    //var mobile = window.matchMedia("only screen and (max-width: 5000px)").matches;
    document.getElementById("FileUpload").style.display = "none";
    //if (mobile) {
    //    document.getElementById("FileUpload").style.display = "none";
    //}
    //else {
    //    document.getElementById("CameraButton").style.display = "none";
    //}


});


