
//this method runs when view has loaded
$(document).ready(function (e) {

    var video = document.getElementById("video");
    if (video != null) {
        video.style.display = "none";

    }

    //check if user in on a mobile device
    var mobile = window.matchMedia("only screen and (max-width: 760px)").matches;

    if (mobile) {
        document.getElementById("FileUpload").style.display = "none";
    }
    else {
        document.getElementById("CameraButton").style.display = "none";
    }


});


